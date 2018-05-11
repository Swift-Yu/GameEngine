using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;

namespace Engine
{
    public delegate void HttpCallback(NetWorkError e, string state, object extrans);
    // Get请求
    class HttpGetRequest
    {
        public HttpWebResponse m_hResponse = null;
        public HttpWebRequest m_hRequest = null;
        //public event HttpCallback m_httpCallback = null;
        public object param = null;
        public string m_strURL = "";
        // 线程
        public Thread m_httpThread = null;

        public void Proc()
        {

        }
    }

    // post请求
    class HttpPostRequest
    {
        private HttpWebResponse m_hResponse = null;
        private HttpWebRequest m_hRequest = null;
        private event HttpCallback m_httpCallback = null;
        private object param = null;
        private string m_strURL = "";
        private string m_PostString = "";
        // 线程
        private Thread m_httpThread = null;

        private int m_nID = 0;

        public HttpPostRequest(int nID )
        {
            m_nID = nID;
        }

        public void Start(ref string strURL, ref string strPostString, HttpCallback callback, object extrans, bool bImmediate = false)
        {
            m_strURL = strURL;
            m_PostString = strPostString;
            this.m_httpCallback += callback;
            this.param = extrans;

            if (!bImmediate)
            {
                m_httpThread = new Thread(this.Proc);
                if (m_httpThread != null)
                {
                    m_httpThread.Start();
                }
            }
            else
            {
                Proc();
            }
        }

        public void Close()
        {
            if (m_hResponse != null)
            {
                m_hResponse.Close();
                m_hResponse = null;
            }

            if (m_hRequest != null)
            {
                m_hRequest.Abort();
                m_hRequest = null;
            }

            m_httpThread = null;
        }

        public void Proc()
        {
            try
            {
                m_hRequest = System.Net.WebRequest.Create(m_strURL) as HttpWebRequest;
                m_hRequest.Method = "POST";
                m_hRequest.ServicePoint.Expect100Continue = false;
                m_hRequest.Timeout = 1000 * 10;
                m_hRequest.KeepAlive = false;
                m_hRequest.ContentType = "application/x-www-form-urlencoded";
                
                byte[] data = System.Text.Encoding.UTF8.GetBytes(m_PostString);
                using (Stream stream = m_hRequest.GetRequestStream())
                {
                    //m_hRequest.ContentLength = data.Length;
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                }
            }
            catch(System.Net.WebException e)
            {
                ThreadHelper.RunOnMainThread(() =>
                {
                    if (m_httpCallback != null)
                    {
                        m_httpCallback(NetWorkError.NetWorkError_ConnectFailed, "CreateWebRequest失败:"+e.ToString(), param);
                    }
                });
                //Debuger.LogTrace("WebRequest连接错误");
                Close();
                return;
            }

            try
            {
                ////获取文件读取到的长度
                m_hResponse = (HttpWebResponse)m_hRequest.GetResponse();
            }
            catch (System.Exception e)
            {
                Close();

                ThreadHelper.RunOnMainThread(() =>
                {
                    if (m_httpCallback != null)
                    {
                        m_httpCallback(NetWorkError.NetWorkError_ConnectFailed, "服务器没有回应或者文件不存在:" + e.ToString(), param);
                    }
                });
                return;
            }

            if (m_hResponse == null)
            {
                return;
            }

            if (m_hResponse.StatusCode != HttpStatusCode.OK && m_hResponse.StatusCode != HttpStatusCode.PartialContent) //如果服务器未响应，那么继续等待相应
            {
                string strCode = string.Format("服务器返回状态码错误:{0}", m_hResponse.StatusCode);
                ThreadHelper.RunOnMainThread(() =>
                {
                    if (m_httpCallback != null)
                    {
                        m_httpCallback(NetWorkError.NetWorkError_ConnectFailed, strCode, param);
                    }
                });

                Close();
                return;
            }

            Stream streamRead = null;
            try
            {
                streamRead = m_hResponse.GetResponseStream();
            }
            catch (System.Exception e)
            {
                ThreadHelper.RunOnMainThread(() =>
                {
                    if (m_httpCallback != null)
                    {
                        m_httpCallback(NetWorkError.NetWorkError_ConnectFailed, e.ToString(), param);
                    }
                });

                Close();
                return;
            }

            StreamReader streamReadResponse = new StreamReader(streamRead, Encoding.UTF8);
            string content = streamReadResponse.ReadToEnd();
            ThreadHelper.RunOnMainThread(() =>
            {
                if (m_httpCallback != null)
                {
                    m_httpCallback(NetWorkError.NetWorkError_ConnectSuccess, content, param);
                }
            });

            Close();
        }
    }
}