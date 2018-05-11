/********************************************************************
	创建日期:	2017:9:17  20:31
	文件名称: 	WWWLoad.cs
	创 建 人:	zhaochengxue
	版权所有: 	途游深圳研发中心
	说    明:	www下载文件
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class WWWLoad : MonoBehaviour
    {
        public static bool showLog = true;
        // 添加一个新的加载文件和远程URL的方法
        public delegate void WWWCallback(WWW www, object param = null);
        private class WWWRequest
        {
            public WWW www = null;
            public string strURL = "";
            public WWWCallback callback = null;
            public bool m_bCache = false;
            public object m_param = null;

        }
        private bool isLoading = false;
        private List<WWWRequest> wwwRequest = new List<WWWRequest>();

        public void LoadURL(string strURL, WWWCallback callback, object param = null, bool bCache = false)
        {
            WWWRequest req = new WWWRequest();
            req.strURL = strURL;
            req.callback = callback;
            req.m_bCache = bCache;
            req.m_param = param;
            wwwRequest.Add(req);
            checkQueue();
        }
        //
        private IEnumerator WWWProcess(WWWRequest req)
        {
            req.www = new WWW(req.strURL);

            yield return req.www;

            OnWWWFinish(req);

            if (req != null && !req.m_bCache)
            {
                if (req.www != null)
                {
                    if (req.www.assetBundle != null)
                    {
                        req.www.assetBundle.Unload(false);
                    }

                    req.www.Dispose();
                    req.www = null;
                }
            }
        }

        private void ExecuteOne(WWWRequest req)
        {
            isLoading = true;
            StartCoroutine(WWWProcess(req));
        }

        private void OnWWWFinish(WWWRequest req)
        {
            if (req.callback != null)
            {
                req.callback(req.www, req.m_param);
                if (showLog)
                {
                    Log.Trace("OnWWWFinish-----> " + req.strURL + "over");
                }
            }
            wwwRequest.Remove(req);
            isLoading = false;
            this.checkQueue();
        }

        private void checkQueue()
        {
            if (!isLoading)
            {
                if (wwwRequest.Count > 0)
                {
                    WWWRequest req = wwwRequest[0];
                    if (req != null)
                    {
                        ExecuteOne(req);
                    }
                }
            }
        }
    }
}
