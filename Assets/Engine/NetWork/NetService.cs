using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utility;

namespace Engine
{
    public class NetService : Singleton<NetService>
    {
        private NetLinkSink netLinkSink = new NetLinkSink();
        private INetLink netLink = null;
        private INetLinkMonitor netLinkMoniter = new NetLinkMonitor();
        private System.Text.StringBuilder m_stringBuild = new System.Text.StringBuilder();
        public override void Initialize()
        {
            base.Initialize();
            netLink = NetWork.Instance().CreateNetLink(netLinkSink, netLinkMoniter);
            netLinkMoniter.IsOpen = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="nPort"></param>
        /// <param name="callback"></param>
        /// <param name="receiveCallback"></param>
        /// <param name="disconnectCallback"></param>
        /// <param name="owner">lua table </param>
        public void Connect(string strIP, int nPort, Engine.ConnectCallback callback, Engine.ReceiveMsgCallback receiveCallback,
            Engine.DisconnectCallback disconnectCallback, object owner = null)
        {
            if (netLink == null || netLinkSink == null)
            {
                Log.Error("NetService:netLink or netLinkSink is null");
                return;
            }

            netLinkSink.connectCallback = callback;
            netLinkSink.DisconnectCallback = disconnectCallback;
            netLinkSink.ReceiveMsgCallback = receiveCallback;
            netLinkSink.CallbackOwner = owner;
            netLinkSink.netState = NetLinkSink.NetState.NetState_Login;
            netLink.Connect(strIP, nPort);
        }

        public void CheckNetLink()
        {
            Engine.NetWork.Instance().CheckNetLink();
        }


        public void Send(string strMsg)
        {
            if (netLink != null)
            {
                using (System.IO.MemoryStream mem = new System.IO.MemoryStream())
                {
#if PROFILER
                    UnityEngine.Profiling.Profiler.BeginSample("NetService->Send");
#endif
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(strMsg);

                    string strHead = System.Convert.ToString(bytes.Length, 16);

                    m_stringBuild.Remove(0, m_stringBuild.Length);
                    int length = PackageIn.HEADER_SIZE;
                    while (strHead.Length < length)
                    {
                        m_stringBuild.Append('0');
                        length--;
                    }
                    m_stringBuild.Append(strHead);
                    byte[] head = System.Text.UTF8Encoding.UTF8.GetBytes(m_stringBuild.ToString());

                    int seed = Engine.NetWork.Instance().Seed + bytes.Length;

#if FTCODE
                    int ret = Utility.FTCode.ftcode(seed, 1, ref bytes, bytes.Length);
#else     
                    Utility.FTCode.xorCodeing((uint)seed, ref bytes);
#endif
                    byte[] msg = new byte[PackageIn.HEADER_SIZE + bytes.Length];

                    for (int i = head.Length - 1,k = PackageIn.HEADER_SIZE - 1; i >= 0; i--,k--)
                    {
                        msg[k] = head[i];
                    }
 
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        msg[PackageIn.HEADER_SIZE + i] = bytes[i];
                    }
                    mem.Write(msg, 0, msg.Length);
                    mem.Flush();
                    Engine.PackageOut cmd = new PackageOut(mem,0);
                    netLink.SendMsg(cmd);
#if PROFILER
                    UnityEngine.Profiling.Profiler.EndSample();
#endif
                }
            }
        }

        public void SendToMe(string strMsg)
        {
            if (netLinkSink != null)
            {
                netLinkSink.SendToMe(strMsg);
            }
        }

        public void Close()
        {
            Engine.NetWork.Instance().Seed = 0;

            if (netLink != null)
            {
                netLinkSink.connectCallback = null;
                netLinkSink.DisconnectCallback = null;
                netLinkSink.ReceiveMsgCallback = null;
                netLinkSink.CallbackOwner = null;
                netLink.Disconnect();
            }
        }

        public override void UnInitialize()
        {

            Close();
        }

        // 发送http请求
        public void SendWithHttps(string URL, string postString, SendHttpsCallback cb, params object[] extrans)
        {
            HttpClient client = new HttpClient();
            client.SendPostRequest(URL, postString, cb, extrans);
        }

        public void SendWithHttpImmeditor(string URL, string postString, SendHttpsCallback cb, params object[] extrans)
        {
            HttpClient client = new HttpClient();
            client.SendPostRequest(URL, postString, cb, extrans, true);
        }
    }
}