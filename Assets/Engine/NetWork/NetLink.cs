﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Engine
{
    // 网络连接
    class NetLink : INetLink
    {
        private SCOKET_CONNECT_STATE socketstate = SCOKET_CONNECT_STATE.UNCONNECTED;

        private TcpClient m_tcpClient;
        private ReceiverThread m_receiver;
        private SenderThread m_sender;

        // 网络连接回调
        private INetLinkSink m_NetLinkSink = null;
        // 网络监测回调
        private INetLinkMonitor m_NetLinkMonitor = null;

        private int m_nLinkID = 0;
        private string curIP = "";
        private int curPort;

        // 掉线数据包缓存
        private List<Engine.PackageOut> DiaoxpackOut = null;

        // 收到的数据包
        private Queue<Engine.PackageIn> packs = new Queue<Engine.PackageIn>();

        private float m_nLastTestTime = 0;

        public NetLink(int nLinkID, INetLinkSink linkSink, INetLinkMonitor monitor = null)
        {
            m_NetLinkSink = linkSink;
            m_NetLinkMonitor = monitor;
            m_nLinkID = nLinkID;

            NetManager.Instance().NetLinkSink = linkSink;
            NetManager.Instance().NetLinkMonitor = monitor;
        }

        public int GetLinkID()
        {
            return m_nLinkID;
        }

        // 获取国连接状态
        public SCOKET_CONNECT_STATE GetState() { return socketstate; }

        // 网络是否处于连接状态
        public bool Connected
        {
            get
            {
                if (m_tcpClient == null)
                {
                    return false;
                }

                if (m_tcpClient.Client.Poll(1000, SelectMode.SelectRead) && (m_tcpClient.Client.Available == 0))
                {
                    return false;
                }

                return socketstate == SCOKET_CONNECT_STATE.CONNECTED;
            }
        }

        private void OnConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndConnect(ar);
            }
            catch (SocketException ex)
            {
                Utility.Log.Error("连接服务器{0}:{1}失败:{2}", curIP, curPort, ex.ToString());
                NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_ConnectFailed);
            }
            finally
            {
                if (m_tcpClient.Connected)
                {
                    //  设置属性
                    m_tcpClient.NoDelay = true;
                    m_tcpClient.ReceiveBufferSize = 1024 * 1024;// *2;
                    m_tcpClient.ReceiveTimeout = 10000;
                    m_tcpClient.SendBufferSize = 1024 * 1024;// *2;
                    m_tcpClient.SendTimeout = 10000;
                    //Utility.Log.Trace("m_tcpClient.ReceiveBufferSize :  " + m_tcpClient.ReceiveBufferSize.ToString());
                    //Utility.Log.Trace("m_tcpClient.SendBufferSize :  " + m_tcpClient.SendBufferSize.ToString());

                    if (DiaoxpackOut != null)
                    {
                        DiaoxpackOut.Clear();
                    }

                    if (socketstate == SCOKET_CONNECT_STATE.CONNECTE_STOP)
                    {
                        if (m_sender != null)
                        {
                            m_sender.PopPackage(out DiaoxpackOut);
                            Utility.Log.Trace("断线重连``````````````弹出包->count=" + DiaoxpackOut.Count);
                        }

                        // 重新连接回调
                        if (m_NetLinkSink != null)
                        {
                            m_NetLinkSink.OnReConnected();
                        }
                    }

                    // 启动收包， 发包线程
                    StartSendThread();
                    StartReceiveThread();
                    socketstate = SCOKET_CONNECT_STATE.CONNECTED;

                    //等到收到seed 才能发送消息
                    //NetManager.Instance().PushConnectSuccess();
                }
                else
                {
                    socketstate = SCOKET_CONNECT_STATE.UNCONNECTED;
                    NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_UnConnect);
                }
            }
        }

        public void Connect(string strServerIP, int port)
        {
            //SocketStateManage.Instance.init();
            //if (IsConnected())
            //{
            Disconnect();
            //}
            m_tcpClient = new TcpClient();
            if (strServerIP.Length < 0)
            {
                return;
            }
            Utility.Log.Trace("Connecting " + strServerIP + ":" + port.ToString());
            curIP = strServerIP;
            curPort = port;

            m_tcpClient.BeginConnect(curIP,curPort,new System.AsyncCallback(OnConnectCallback),m_tcpClient.Client);

            //try
            //{
            //    m_tcpClient.Connect(strServerIP, port);
            //}
            //catch (SocketException ex)
            //{
            //    //ex.NativeErrorCode.Equals(10035);
            //    // 连接远程服务器失败
            //    //if( m_NetLinkSink != null )
            //    //{
            //    //    m_NetLinkSink.OnConnectError(NetWorkError.NetWorkError_ConnectError);
            //    //}

            //    NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_ConnectFailed);
            //}
            //finally
            //{
            //    if (m_tcpClient.Connected)
            //    {
            //        //  设置属性
            //        m_tcpClient.NoDelay = true;
            //        m_tcpClient.ReceiveBufferSize = 1024 * 1024;// *2;
            //        m_tcpClient.ReceiveTimeout = 10000;
            //        m_tcpClient.SendBufferSize = 1024 * 1024;// *2;
            //        m_tcpClient.SendTimeout = 10000;
            //        //Utility.Log.Trace("m_tcpClient.ReceiveBufferSize :  " + m_tcpClient.ReceiveBufferSize.ToString());
            //        //Utility.Log.Trace("m_tcpClient.SendBufferSize :  " + m_tcpClient.SendBufferSize.ToString());

            //        if (DiaoxpackOut != null)
            //        {
            //            DiaoxpackOut.Clear();
            //        }

            //        if (socketstate == SCOKET_CONNECT_STATE.CONNECTE_STOP || socketstate == SCOKET_CONNECT_STATE.UNCONNECTED)
            //        {
            //            if (m_sender != null)
            //            {
            //                m_sender.PopPackage(out DiaoxpackOut);
            //                Utility.Log.Trace("断线重连``````````````弹出包->count=" + DiaoxpackOut.Count);
            //            }
                        
            //            // 重新连接回调
            //            if( m_NetLinkSink != null )
            //            {
            //                m_NetLinkSink.OnReConnected();
            //            }
            //        }

            //        // 启动收包， 发包线程
            //        StartSendThread();
            //        StartReceiveThread();
            //        socketstate = SCOKET_CONNECT_STATE.CONNECTED;

            //        NetManager.Instance().PushConnectSuccess();
            //    }
            //    else
            //    {
            //        socketstate = SCOKET_CONNECT_STATE.UNCONNECTED;
            //        NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_UnConnect);
            //    }
            //}
        }

        // 断开连接
        public void Disconnect()
        {
            //          hasLoginCheckConnected = false;
            if (null != m_tcpClient && m_tcpClient.Connected)
            {
                m_tcpClient.GetStream().Close();
                m_tcpClient.Close();
                m_tcpClient = null;
                this.socketstate = SCOKET_CONNECT_STATE.UNCONNECTED;
                NetManager.Instance().PushClose();
            }
            SetTerminateFlag();
        }

        // 发送消息
        public void SendMsg(Engine.PackageOut pkg)
        {
            //Utility.Log.Error("MsgCode:{0}", pkg.getCode());
            if (this.Connected && socketstate == SCOKET_CONNECT_STATE.CONNECTED)
            {
                if (m_sender != null)
                {
                    m_sender.Send(pkg);

                    if( m_NetLinkMonitor != null )
                    {
                        m_NetLinkMonitor.OnSend(pkg);
                    }
                }
            }
            else
            {
                Utility.Log.Trace("已经断开连接，发包错误，包进入缓存");
                if (m_sender != null)
                {
                    m_sender.enqueErrorPack(pkg);
                }

                //if( m_NetLinkSink != null )
                //{
                //    m_NetLinkSink.OnConnectError(NetWorkError.NetWorkError_ConnectError);
                //}
                //NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_ConnectError);
            }
        }

        public void Destroy()
        {
            Disconnect();
        }

        public bool IsConnected()
        {
            if (null == m_tcpClient)
            {
                return false;
            }

            return Connected;
        }

        public void Run()
        {
            if (null == m_tcpClient)
            {
                return;
            }

            float nCurTime = Time.realtimeSinceStartup;
            if (nCurTime - m_nLastTestTime > 5.0f)
            {
                testConnect();
                m_nLastTestTime = nCurTime;
            }

            NetManager.Instance().OnEvent();
        }

        //重连后发送掉线前的包;
        public void ReSendPackOut()
        {
            if (socketstate == SCOKET_CONNECT_STATE.CONNECTED && DiaoxpackOut != null)
            {
                Utility.Log.Trace("断线重连``````````````发1包->count=" + DiaoxpackOut.Count);
                for (int i = 0; i < DiaoxpackOut.Count; ++i)
                {
                    int code = DiaoxpackOut[i].getCode();
                    Utility.Log.Trace("  断线重连``````````````发包" + code);
                    SendMsg(DiaoxpackOut[i]);
                }
                m_sender.PopPackage(out DiaoxpackOut);
                Utility.Log.Trace("断线重连``````````````发2包->count=" + DiaoxpackOut.Count);
                for (int i = 0; i < DiaoxpackOut.Count; ++i)
                {
                    int code = DiaoxpackOut[i].getCode();
                    SendMsg(DiaoxpackOut[i]);
                }

                DiaoxpackOut.Clear();
            }
        }

        private void testConnect()
        {
            if (socketstate == SCOKET_CONNECT_STATE.CONNECTED && m_tcpClient != null && (m_tcpClient.Client != null))
            {
                // 另外说明：tcpc.Connected同tcpc.Client.Connected；
                // tcpc.Client.Connected只能表示Socket上次操作(send,recieve,connect等)时是否能正确连接到资源,
                // 不能用来表示Socket的实时连接状态。
                //m_tcpClient.Connected

                //((m_tcpClient.Client.Poll(1000, SelectMode.SelectRead) && (m_tcpClient.Client.Available == 0)) || !m_tcpClient.Client.Connected)

                if ((!m_tcpClient.Client.Connected || (m_tcpClient.Client.Poll(1000, SelectMode.SelectRead) && m_tcpClient.Client.Available == 0)))
                {
                    //try
                    //{
                    //    //m_tcpClient.Client.Send(null,)
                    //}
                    //catch (System.Exception ex)
                    //{
                        // 断开连接
                        Disconnect();
                        socketstate = SCOKET_CONNECT_STATE.CONNECTE_STOP;
                        NetManager.Instance().PushConnectError(NetWorkError.NetWorkError_DisConnect);

                        //Utility.Log.Error("断开连接{0}",ex.ToString());
                    //}
                }
            }

        }

        protected void SetTerminateFlag()
        {
            if (m_sender != null)
            {
                m_sender.SetTerminateFlag();
            }

            if (m_receiver != null)
            {
                m_receiver.SetTerminateFlag();
            }
        }

        protected void WaitTermination()
        {
            if (m_sender != null)
                m_sender.WaitTermination();
            if (m_receiver != null)
                m_receiver.WaitTermination();
        }

        protected void StartReceiveThread()
        {
            if (m_receiver != null)
            {
                m_receiver = null;
            }
            m_receiver = new ReceiverThread(m_tcpClient.GetStream());
            m_receiver.Run();
        }

        protected void StartSendThread()
        {
            SenderThread sendThread = new SenderThread(m_tcpClient, m_tcpClient.GetStream());
            if (m_sender != null)
            {
                m_sender = null;
            }

            m_sender = sendThread;
            m_sender.Run();
        }

        //private void CopyToPackage(ref Queue<Engine.PackageIn> queue)
        //{
        //    lock (m_receiver.m_Mutex)
        //    {
        //        int count = m_receiver.m_packetQueue.Count;
        //        for (int i = 0; i < count; ++i)
        //        {
        //            queue.Enqueue(m_receiver.m_packetQueue.Dequeue());
        //        }
        //        m_receiver.m_packetQueue.Clear();
        //    }
        //}
    }
}
