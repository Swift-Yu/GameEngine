using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    enum NetCmdType
    {
        NetCmd_Null = 0,
        NetCmd_ConnectSuccess,
        NetCmd_ConnectError,
        NetCmd_Recv,
        NetCmd_Close,
        NetCmd_Error,
    }

    class NetCommand
    {
        public NetCmdType dwType;  // 命令类型
        public int dwLen;          // 数据长度
        public NetWorkError error; // 网络错误
        public PackageIn msg;      // 数据buff

        public NetCommand()
        {
            this.dwType = NetCmdType.NetCmd_Null;
            this.dwLen = 0;
            this.error = NetWorkError.NetWorkError_null;
            msg = null;
        }
        public NetCommand(NetCommand cmd)
        {
            this.dwType = cmd.dwType;
            this.dwLen = cmd.dwLen;
            if (this.dwLen > 0)
            {
                this.msg = cmd.msg;
            }
        }
    }

    // 网络消息管理器
    class NetManager
    {
        private static NetManager _inst = null;

        public static NetManager Instance()
        {
            if (_inst == null)
            {
                _inst = new NetManager();
            }

            return _inst;
        }

        private TSwitchList<NetCommand> m_NetCommandList = new TSwitchList<NetCommand>();   // 网络命令队列
        private INetLinkSink m_NetLinkSink = null;
        private INetLinkMonitor m_NetLinkMonitor = null;

        public INetLinkSink NetLinkSink
        {
            set { m_NetLinkSink = value; }
        }
        public INetLinkMonitor NetLinkMonitor
        {
            set { m_NetLinkMonitor = value; }
        }


        public void PushConnectSuccess()
        {
            NetCommand cmd = new NetCommand();
            cmd.dwType = NetCmdType.NetCmd_ConnectSuccess;
            cmd.error = NetWorkError.NetWorkError_ConnectSuccess;
            m_NetCommandList.Push(cmd);
        }
        public void PushConnectError(NetWorkError error)
        {
            NetCommand cmd = new NetCommand();
            cmd.dwType = NetCmdType.NetCmd_ConnectError;
            cmd.error = error;
            m_NetCommandList.Push(cmd);
        }

        public void PushClose()
        {
            NetCommand cmd = new NetCommand();
            cmd.dwType = NetCmdType.NetCmd_Close;
            m_NetCommandList.Push(cmd);
        }

        public void PushRecv(PackageIn msg)
        {
            NetCommand cmd = new NetCommand();
            cmd.dwType = NetCmdType.NetCmd_Recv;
            cmd.msg = msg;
            cmd.dwLen = (int)msg.Length;
            //cmd.buff.Write(buff, 0,nLen);
            m_NetCommandList.Push(cmd);
        }

        public void OnConnectSuccess()
        {
            if( m_NetLinkSink != null )
            {
                m_NetLinkSink.OnConnectError(NetWorkError.NetWorkError_ConnectSuccess);
            }
        }

        public void OnConnectError(NetWorkError error)
        {
            if (m_NetLinkSink != null)
            {
                if (error == NetWorkError.NetWorkError_DisConnect)
                {
                    m_NetLinkSink.OnDisConnect();
                }
                else
                {
                    m_NetLinkSink.OnConnectError(error);
                }
            }
        }

        public void OnClose()
        {
            if (m_NetLinkSink != null)
            {
                m_NetLinkSink.OnClose();
            }
        }

        public void OnRecv(PackageIn msg)
        {
            if (m_NetLinkSink != null)
            {
                m_NetLinkSink.OnReceive(msg);
            }

            if (m_NetLinkMonitor != null)
            {
                m_NetLinkMonitor.OnReceive(msg);
            }
        }

        public void OnEvent()
        {
            m_NetCommandList.Switch();
            if( m_NetCommandList.m_OutList != null )
            {
                while(m_NetCommandList.m_OutList.Count>0)
                {
                    NetCommand cmd = m_NetCommandList.m_OutList[0];
                    try
                    {
                        switch (cmd.dwType)
                        {
                            case NetCmdType.NetCmd_ConnectSuccess:
                                {
                                    OnConnectSuccess();
                                    break;
                                }
                            case NetCmdType.NetCmd_ConnectError:
                                {
                                    OnConnectError(cmd.error);
                                    break;
                                }
                            case NetCmdType.NetCmd_Close:
                                {
                                    OnClose();
                                    break;
                                }
                            case NetCmdType.NetCmd_Recv:
                                {
                                    OnRecv(cmd.msg);
                                    break;
                                }
                        }
                    }
                    catch( SystemException e )
                    {
                        if(cmd.dwType == NetCmdType.NetCmd_Recv)
                        {
                            Utility.Log.Error("网络消息处理异常{0}", e.ToString());
                        }
                    }
                    finally
                    {
                        m_NetCommandList.m_OutList.RemoveAt(0);
                    }
                }
            }
        }
    }
}
