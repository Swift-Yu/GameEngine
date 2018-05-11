using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine
{
    public delegate void SendHttpsCallback(NetWorkError e, string state, object extrans);
    public class HttpClient
    {
        class HttpPostRequestInfo
        {
            public HttpPostRequest req = null;
            public SendHttpsCallback m_httpCallback = null;
            public object param = null;
        }
        private Dictionary<int, HttpPostRequestInfo> m_dicPostRequest = new Dictionary<int, HttpPostRequestInfo>();
        private int m_PostIDSeed = 0;

        public void SendPostRequest(string URL, string postString, SendHttpsCallback cb, object extrans, bool bImmediate = false)
        {
            HttpPostRequestInfo info = new HttpPostRequestInfo();

            info.req = new HttpPostRequest(++m_PostIDSeed);
            info.m_httpCallback = cb;
            info.param = extrans;
            m_dicPostRequest[m_PostIDSeed] = info;
            info.req.Start(ref URL, ref postString, OnHttpCallPostCallBack, m_PostIDSeed, bImmediate);
        }

        private void OnHttpCallPostCallBack(NetWorkError e, string state, object extrans)
        {
            int nPostID = (int)extrans;

            HttpPostRequestInfo info;
            if (m_dicPostRequest.TryGetValue(nPostID, out info))
            {
                if (info.req == null)
                {
                    m_dicPostRequest.Remove(nPostID);
                    return;
                }

                ThreadHelper.RunOnMainThread(() =>
                {
                    if (info.req != null && info.m_httpCallback != null)
                    {
                        info.m_httpCallback(e, state,info.param);
                        info.req.Close();
                    }
                });
            }
        }
    }
}
