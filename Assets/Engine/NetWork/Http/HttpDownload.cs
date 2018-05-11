using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    ///  http下载文件实现
    /// </summary>
    public class HttpDownload
    {
        public delegate void DownFinishDelegate(Engine.HttpDownloadError eError, string strResName);
        public delegate void DownProgressDelegate(string strResName, int nDownloadSize, int nTotalSize);

        private static HttpDownload _inst = null;
        public static HttpDownload Instance()
        {
            if (_inst == null)
            {
                _inst = new HttpDownload();
            }
            return _inst;
        }

        // 下载文件信息
        private struct SDownloadInfo
        {
            public string strURL;  // 下载地址
            public string strDestFile; // 保存路径

            public SDownloadInfo(ref string url, ref string destfile)
            {
                strURL = url;
                strDestFile = destfile;
            }
        }

        private HttpDownloadFile m_httpDownLoad = new HttpDownloadFile();

        private DownFinishDelegate m_DownFinishCallBack = null;        // 下载完成
        private DownProgressDelegate m_DownProgressCallBack = null;      // 下载进程
        private List<SDownloadInfo> m_lstDownloadFile = new List<SDownloadInfo>();          // 下载文件列表
        private SDownloadInfo m_curDownloadFile;                        // 当前正在下载的文件
        private bool m_bDownload = false;                               // 当前是否正在下载 不允许同时下载多个文件

        public HttpDownload()
        {
           

            if (m_httpDownLoad != null)
            {
                m_httpDownLoad.downloadCallback += OnHttpDownloadCallback;
            }
        }

        public void SetDownloadCallback(DownFinishDelegate finishCallback, DownProgressDelegate progressCallback)
        {
            m_DownFinishCallBack += finishCallback;
            m_DownProgressCallBack += progressCallback;
        }

        public void ClearDownloadCallback(DownFinishDelegate finishCallback, DownProgressDelegate progressCallback)
        {
            m_DownFinishCallBack = null;
            m_DownProgressCallBack = null;
        }

        //下载文件
        public void DownLoadFile(string file, string destFile)
        {
            // 添加到下载队列
            m_lstDownloadFile.Add(new SDownloadInfo(ref file, ref destFile));
        }


        public void Run()
        {
            if (m_lstDownloadFile.Count <= 0 || m_bDownload == true)
            {
                return;
            }

            m_curDownloadFile = m_lstDownloadFile[0];
            Uri url = new Uri(m_curDownloadFile.strURL);

            string strPath, strFileName, strExt, strNoExt;
            Utility.StringUtility.ParseFileName(ref m_curDownloadFile.strDestFile, out strPath, out strFileName, out strNoExt, out strExt);
            Utility.FileUtils.Instance().CreateDir(strPath);

            m_httpDownLoad.DownloadFileAsync(url, m_curDownloadFile.strDestFile);
            m_bDownload = true;
            m_lstDownloadFile.RemoveAt(0);
        }

        public void Release()
        {
            if (m_httpDownLoad != null)
            {
                m_httpDownLoad.Close();
            }
        }

        void OnHttpDownloadCallback(Engine.HttpDownloadError eErrorCode, object data)
        {
            switch (eErrorCode)
            {
                case Engine.HttpDownloadError.HttpDownloadError_NetExp:
                    {
                        //object[] arr = data as object[];
                        if (m_DownFinishCallBack != null)
                        {
                            m_DownFinishCallBack(eErrorCode, m_curDownloadFile.strDestFile); // 返回错误
                        }
                        break;
                    }
                case Engine.HttpDownloadError.HttpDownloadError_Downloading:
                    {
                        object[] arr = data as object[];
                        if (m_DownProgressCallBack != null)
                        {
                            m_DownProgressCallBack(m_curDownloadFile.strURL, (int)arr[0], (int)arr[1]);
                        }
                        break;
                    }
                case Engine.HttpDownloadError.HttpDownloadError_Completed:
                    {
                        //m_strMsg = string.Format("下载完成");
                        m_bDownload = false;
                        if (m_DownFinishCallBack != null)
                        {
                            m_DownFinishCallBack(eErrorCode, m_curDownloadFile.strDestFile);
                            m_DownProgressCallBack = null;
                            m_DownFinishCallBack = null;
                        }
                        break;
                    }
                //case Star.HttpDownloadError.HttpDownloadError_Connecting:
                //    {
                //        if (m_DownFinishCallBack != null)
                //        {
                //            m_DownFinishCallBack(0, m_curDownloadFile.strDestFile);
                //        }
                //        break;
                //    }
            }
        }
    }
}