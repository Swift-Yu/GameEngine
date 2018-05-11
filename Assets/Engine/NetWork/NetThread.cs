using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

// 网络线程
namespace Engine
{
    public abstract class BasicThread
    {
        private Thread m_thread;
        private bool m_terminateFlag;
        private System.Object m_terminateFlagMutex;

        public BasicThread()
        {
            m_thread = new Thread(ThreadProc);
            m_terminateFlag = false;
            m_terminateFlagMutex = new System.Object();
        }

        public void Run()
        {
            m_thread.Start(this);
        }

        protected static void ThreadProc(object obj)
        {
            BasicThread me = (BasicThread)obj;
            me.Main();
        }

        protected abstract void Main();

        public void WaitTermination()
        {
            m_thread.Join();
        }

        public void SetTerminateFlag()
        {
            lock (m_terminateFlagMutex)
            {
                m_terminateFlag = true;
            }
        }

        public bool IsTerminateFlagSet()
        {
            lock (m_terminateFlagMutex)
            {
                return m_terminateFlag;
            }
        }
    }

    abstract class NetThread : BasicThread
    {
        private NetworkStream m_netStream;

        public NetThread(NetworkStream stream)
            : base()
        {
            m_netStream = stream;
        }

        protected NetworkStream NetStream
        {
            get
            {
                return m_netStream;
            }
        }
    }

    class ReceiverThread : NetThread
    {
        const int SEED_SIZE = 4;
        const int MSG_HEAD_LENGTH = 4;

        const uint MaxPacketSize = 1024 * 512;
        //缓冲区;
        private byte[] _readBuffer;
        //缓冲区  读取 下标;
        private int _readOffset;
        //缓冲区  写入 下标;
        private int _writeOffset;


        public ReceiverThread(NetworkStream stream)
            : base(stream)
        {
            _readBuffer = new byte[2 * MaxPacketSize];
            _writeOffset = 0;
            _readOffset = 0;
        }



        protected override void Main()
        {
            //           Utility.Log.Trace("ReceiverThread.Main : Begin");

            while (!IsTerminateFlagSet())
            {
                ReadFromStream();
                //if (_writeOffset > 1)
                {

                    if (Engine.NetWork.Instance().Seed == 0 && _writeOffset == SEED_SIZE)
                    {
                        ReadSeed();
                    }
                    else if (_writeOffset >= Engine.PackageIn.HEADER_SIZE)
                    {
                        readPackage();
                    }
                }
            }

            //           Utility.Log.Trace("ReceiverThread.Main : End");
        }

        protected void ReadSeed()
        {
            int dataLeft = _writeOffset - _readOffset;
            int len = 4;
            //包足够长;
            if (dataLeft >= len )
            {
                byte[] buf = new byte[len];
                Array.Copy(_readBuffer, _readOffset, buf, 0, len);
                _readOffset += len;
                dataLeft = _writeOffset - _readOffset;
                
                string strSeed = System.Text.UTF8Encoding.UTF8.GetString(buf);
                //Utility.Log.Info("Net Seed " + strSeed);
                Engine.NetWork.Instance().Seed = Convert.ToInt32(strSeed, 16);
                NetManager.Instance().PushConnectSuccess();
               // Utility.Log.Info("Net Seed " + Engine.NetWork.Instance().Seed);
            }

            if (dataLeft > 0)
            {
                for (int i = _readOffset, j = 0; i < _readOffset + dataLeft; i++, j++)
                {
                    _readBuffer[j] = _readBuffer[i];
                }
            }
            _readOffset = 0;
            _writeOffset = dataLeft;

        }

        int total = 0;
        protected void ReadFromStream()
        {
            if (NetStream.DataAvailable)
            {
                int offset = NetStream.Read(_readBuffer, _writeOffset, _readBuffer.Length - _writeOffset);
                _writeOffset += offset;
                total += offset;
            }
            else
            {
                Thread.Sleep(16);
            }
        }

        protected void readPackage()
        {
            int dataLeft = _writeOffset - _readOffset;
            do
            {
                int len = -1;
                if (_readOffset + PackageIn.HEADER_SIZE <= _writeOffset)
                {
                    byte[] buf = new byte[PackageIn.HEADER_SIZE];
                    Array.Copy(_readBuffer, _readOffset, buf, 0, PackageIn.HEADER_SIZE);

                    string strSeed = System.Text.UTF8Encoding.UTF8.GetString(buf);
                    len = Convert.ToInt32(strSeed, 16);
                    _readOffset += PackageIn.HEADER_SIZE;
                }

                if (len == -1 || len == 0)
                {
                    return;
                }

                dataLeft = _writeOffset - _readOffset;
                //包足够长;
                if (dataLeft >= len && len != 0)
                {

                    byte[] buf = new byte[len];
                    Array.Copy(_readBuffer, _readOffset, buf, 0, len);
                    Engine.PackageIn package = new Engine.PackageIn(buf);
                    NetManager.Instance().PushRecv(package);
                    _readOffset += len;
                    dataLeft = _writeOffset - _readOffset;
                }
                else
                {
                    _readOffset -= PackageIn.HEADER_SIZE;
                    dataLeft = _writeOffset - _readOffset;
                    break;
                }
            }while (dataLeft >= Engine.PackageIn.HEADER_SIZE);
            
            // 数据往前移
            if (dataLeft > 0)
            {
                for (int i = _readOffset, j = 0; i < _readOffset + dataLeft; i++, j++)
                {
                    _readBuffer[j] = _readBuffer[i];
                }
            }
            _readOffset = 0;
            _writeOffset = dataLeft;

        }
    }

    class SenderThread : NetThread
    {
        private TcpClient m_tcpClient;
        private TSwitchList<PackageOut> m_packetList = new TSwitchList<PackageOut>();
        // 线程事件
        private AutoResetEvent m_SendEvent = new AutoResetEvent(false);

        // 发送异常缓存队列
        private Queue<Engine.PackageOut> packout = new Queue<Engine.PackageOut>();
        private Engine.PackageOut packoutCmd = null;
        public SenderThread(TcpClient client, NetworkStream stream)
            : base(stream)
        {
            m_tcpClient = client;
        }

        public void Send(Engine.PackageOut p)
        {
            m_packetList.Push(p);
            m_SendEvent.Set();
        }

        public void enqueErrorPack(Engine.PackageOut pkg)
        {
            packout.Enqueue(pkg);
        }

        public void PopPackage(out List<Engine.PackageOut> lstPackage)
        {
            lstPackage = new List<Engine.PackageOut>();
            Utility.Log.Trace("PopPackage start->lstPackage count=" + lstPackage.Count);
            if (packoutCmd != null)
            {
                lstPackage.Add(packoutCmd);
            }
            if (packout != null)
            {
                while (packout.Count > 0)
                {
                    Engine.PackageOut pkg = packout.Dequeue();
                    if (pkg != packoutCmd)
                    {
                        lstPackage.Add(pkg);
                    }
                }
            }

            //for (int i = 0; i < m_packetList.m_InList.Count; ++i )
            //{
            //    //Engine.PackageOut pkg = m_packetQueue.Dequeue();
            //    if (m_packetList.m_InList[i] != packoutCmd)
            //    {
            //        lstPackage.Add(m_packetList.m_InList[i]);
            //    }
            //}
            //Utility.Log.Trace("PopPackage end ->lstPackage count=" + lstPackage.Count);
        }

        protected override void Main()
        {
            while (!IsTerminateFlagSet() && m_SendEvent != null)
            {
                m_SendEvent.WaitOne();
                m_packetList.Switch();

                while (m_packetList.m_OutList.Count > 0)
                {
                    Engine.PackageOut pkg = m_packetList.m_OutList[0];
                    //pkg.pack();
                    packoutCmd = pkg;
                    m_packetList.m_OutList.RemoveAt(0);

                    if (m_tcpClient != null && m_tcpClient.Connected)
                    {
                        try
                        {
                            // encode
                            byte[] buff = pkg.GetBuffer();

//                             if (NetWork.DEENCODE)
//                             {
//                                 // 加密编码
//                                 //Engine.Encrypt.Encode(ref buff);
//                             }

                            NetStream.Write(buff, 0, (int)pkg.Length);
                            NetStream.Flush();

                            if (m_tcpClient == null || !m_tcpClient.Connected)
                            {
                                enqueErrorPack(pkg);
                            }
                        }
                        catch (IOException e)
                        {
                            Utility.Log.Error("发包{0}异常: {1}", pkg.getCode(), e.ToString());
                            enqueErrorPack(pkg);
                        }
                    }
                    else
                    {
                        // Utility.Log.Trace("断线发包: code=" + pkg.getCode().ToString());
                        enqueErrorPack(pkg);
                        break;
                    }
                } // end while(m_packetList.m_OutList.Count > 0)
            }//end while
        }
    }
}