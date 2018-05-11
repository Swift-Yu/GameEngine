using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    /// <summary>
    /// 网络接口 目前只支持单连接
    /// </summary>
    class NetWork
    {
        private static NetWork _inst = null;

        public static NetWork Instance()
        {
            if (_inst == null)
            {
                _inst = new NetWork();
            }

            return _inst;
        }

        public const float MaxDelayTime = 300f;
        public const short HEADER = 0x71ab;

        public const bool DEENCODE = true;

        // 连接种子
        private int m_nLinkIDSeed = 0;
        // 网络连接
        private NetLink m_NetLink = null;

        //服务器下发的加密种子
        public int Seed
        {
            get;
            set;
        }

        public NetWork()
        {
            // 设置默认连接数
            System.Net.ServicePointManager.DefaultConnectionLimit = 10;
        }


        public bool IsValid()
        {
            return m_NetLink != null;
        }

        // 创建网络连接
        public INetLink CreateNetLink(INetLinkSink linkSink, INetLinkMonitor monitor = null)
        {
            m_NetLink = new NetLink(++m_nLinkIDSeed, linkSink, monitor);
            if (m_NetLink == null)
            {
                Utility.Log.Error("CreateNetLink失败！");
                return null;
            }

            return m_NetLink;
        }

        public void CheckNetLink()
        {
            if( m_NetLink != null )
            {
                m_NetLink.Run();
            }
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
