using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.AccessControl;

namespace Engine
{
    public enum HttpDownloadError
    {
        HttpDownloadError_null = 0,
        HttpDownloadError_NetExp,   // 网络异常 erroreDesc
        //HttpDownloadError_Connecting, // 无参数 连接状态
        HttpDownloadError_Downloading, // param cur/total
        HttpDownloadError_Completed, // 无参数
    }

    // httpDownload下载回调
    public delegate void HttpDownloadCallback(HttpDownloadError eErrorCode, object data);

    public class HttpDownloadFile
    {
        // 下载回调
        public event HttpDownloadCallback downloadCallback = null;

        // 文件下载描述信息
        private int m_nDownloadSize = 0;
        private int m_nTotalSize = 0;
        // 正在下载文件，不允许同时下载多个文件
        private bool m_bDownloading = false;

        // 目标文件
        private FileStream m_destFile = null;
        // 配置文件
        private FileStream m_cfgFile = null;

        private HttpWebResponse m_hResponse = null;
        private HttpWebRequest m_hRequest = null;

        private string m_strURL = "";
        private string m_strDestFileName = "";

        // 下载线程
        Thread m_dlThread = null;

        // 下载缓冲区 10k
        private byte[] m_Buffer = new byte[10240];

        public static void ThreadProc(object obj)
        {
            HttpDownloadFile http = (HttpDownloadFile)obj;
            if (http != null)
            {
                http.DownloadProc();
            }
        }

        public void Close()
        {
            if (m_dlThread != null)
            {
                WriteCfgFile();
                m_dlThread.Abort();
                m_dlThread = null;
            }
        }

        public void DownloadFileAsync(Uri address, string strFileName)
        {
            if (m_bDownloading)
            {
                return;
            }

            m_bDownloading = true;

            m_strURL = address.ToString();
            m_strDestFileName = strFileName;

            int nRange = CheckRange(ref strFileName);
            if (nRange < 0)
            {
                Utility.Log.Trace("CheckRange < 0 ");
                return;
            }

            ////请求开始位置
            m_nDownloadSize = nRange;

            m_dlThread = new Thread(ThreadProc);
            if (m_dlThread != null)
            {
                m_dlThread.Start(this);
            }
        }

        //-------------------------------------------------------------------------------------------------------
        private int CheckRange(ref string strFileName)
        {
            int nRange = 0;
            string strCFGName = strFileName + ".fd";
            FileInfo cfgi = new FileInfo(strCFGName);
            if (!cfgi.Exists)
            {
                m_destFile = new FileStream(strFileName, FileMode.Create);
                if (m_destFile == null)
                {
                    return -1;
                }

                m_cfgFile = new FileStream(strCFGName, FileMode.Create);
                if (m_cfgFile == null)
                {
                    return -1;
                }
            }
            else
            {
                m_cfgFile = new FileStream(strCFGName, FileMode.OpenOrCreate);
                if (m_cfgFile == null)
                {
                    return -1;
                }

                FileInfo fi = new FileInfo(strFileName);
                if (!fi.Exists)
                {
                    m_destFile = new FileStream(strFileName, FileMode.CreateNew);
                    if (m_destFile == null)
                    {
                        return -1;
                    }

                    return 0;
                }
                else
                {
                    m_destFile = new FileStream(strFileName, FileMode.OpenOrCreate);
                    if (m_destFile == null)
                    {
                        return -1;
                    }
                }

                long lSize = m_destFile.Length;

                StreamReader fr = new StreamReader(m_cfgFile);
                string strMsg = fr.ReadLine();
                if (strMsg != null)
                {
                    string[] objs = strMsg.Split(',');
                    if (objs.Length == 4)
                    {
                        if (objs[0] != m_strURL)
                        {
                            nRange = 0;
                        }
                        else
                        {
                            nRange = int.Parse(objs[2]);
                            if (lSize < nRange)
                            {
                                nRange = 0;
                            }

                            if (nRange > int.Parse(objs[3]))
                            {
                                fr.Close();
                                cfgi.Delete();
                                return -1;
                            }
                        }
                    }
                }

                //fr.Close();
            }

            return nRange;
        }

        //-------------------------------------------------------------------------------------------------------
        // 线程处理函数
        private void DownloadProc()
        {
            Utility.Log.Trace("线程开始下载 m_destFile = " + m_destFile + " m_cfgFile = " + m_cfgFile + " m_hRequest = " + m_hRequest);
            //if (m_destFile == null || m_cfgFile == null || m_hRequest == null )
            if (m_destFile == null || m_cfgFile == null)
            {
                return;
            }

            try
            {
                m_hRequest = (HttpWebRequest)HttpWebRequest.Create(m_strURL);
                Utility.Log.Trace("申请Http请求 ：" + m_strURL);
                if (m_hRequest == null)
                {
                    Utility.Log.Trace("创建网络连接失败 ：" + m_strURL);
                    this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { "创建网络连接失败！" });
                    return;
                }
            }
            catch (System.Exception e)
            {
                if (m_hRequest != null)
                {
                    m_hRequest.Abort();
                }

                m_destFile.Close();
                m_cfgFile.Close();

                this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { e.ToString() });
                Utility.Log.Trace("申请Http请求异常");
                return;
            }

            m_hRequest.Method = "GET";
            m_hRequest.KeepAlive = true;
            m_hRequest.Timeout = 15000;
            m_hRequest.AddRange("bytes", m_nDownloadSize);

            try
            {
                ////获取文件读取到的长度
                Utility.Log.Trace("获取文件读取到的长度");
                m_hResponse = (HttpWebResponse)m_hRequest.GetResponse();
            }
            catch (System.Exception e)
            {
                if (m_hRequest != null)
                {
                    m_hRequest.Abort();
                    m_hRequest = null;
                }

                m_destFile.Close();

                WriteCfgFile();
                //m_cfgFile.Close();
                Utility.Log.Error("Exeception: " + e);
                Utility.Log.Trace("获取文件读取到的长度异常");
                this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { "服务器没有回应或者文件不存在！" });
                return;
            }

            if (m_hResponse == null)
            {
                return;
            }

            if (m_hResponse.StatusCode != HttpStatusCode.OK && m_hResponse.StatusCode != HttpStatusCode.PartialContent) //如果服务器未响应，那么继续等待相应
            {
                m_hResponse.Close();

                m_destFile.Close();
                WriteCfgFile();
                m_hRequest.Abort();
                string strCode = string.Format("服务器返回状态码错误:{0}", m_hResponse.StatusCode);
                Utility.Log.Trace(strCode);
                this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { strCode });
                return;
            }

            if (m_hResponse.Headers["Content-Range"] == null)
            {
                // 是重新下载完整文件
            }

            m_nTotalSize = m_nDownloadSize + (int)m_hResponse.ContentLength;

            m_destFile.Seek(m_nDownloadSize, SeekOrigin.Begin);

            byte[] buffer = this.m_Buffer;
            Stream stream = null;
            try
            {
                stream = m_hResponse.GetResponseStream();
            }
            catch (System.Exception e)
            {
                if (m_hRequest != null)
                {
                    m_hRequest.Abort();
                }

                m_destFile.Close();
                m_cfgFile.Close();
                Utility.Log.Trace("获取文件读取到的长度异常2");
                this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { e.ToString() });
                return;
            }

            if (stream != null)
            {
                int size = stream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    //只将读出的字节写入文件
                    m_destFile.Write(buffer, 0, size);
                    m_nDownloadSize += size;
                    this.downloadCallback(HttpDownloadError.HttpDownloadError_Downloading, new object[] { m_nDownloadSize, m_nTotalSize, m_strURL });
                    m_destFile.Flush();
                    size = stream.Read(buffer, 0, buffer.Length);
                    
                    WriteCfgFile(false);
                }

                WriteCfgFile();
                stream.Close();
            }

            m_hResponse.Close();
            m_hResponse = null;
            m_destFile.Close();
            m_destFile = null;
            m_hRequest.Abort();
            m_hResponse = null;

            if (m_nDownloadSize < m_nTotalSize)
            {
                Utility.Log.Trace("文件下载失败");
                this.downloadCallback(HttpDownloadError.HttpDownloadError_NetExp, new object[] { "文件下载失败！" });
            }
            else
            {
                string strCFGName = m_strDestFileName + ".fd";
                FileInfo fi = new FileInfo(strCFGName);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                Utility.Log.Trace("下载完成");
                // 在完成回调中不能再添加下载任务
                this.downloadCallback(HttpDownloadError.HttpDownloadError_Completed, null);
            }

            m_bDownloading = false;
        }

        //-------------------------------------------------------------------------------------------------------
        void WriteCfgFile( bool bClose = true )
        {
            if (m_cfgFile != null)
            {
                // 写入配置表信息
                StreamWriter cfg = new StreamWriter(m_cfgFile);
                m_cfgFile.Seek(0, SeekOrigin.Begin);
                cfg.WriteLine("{0},{1},{2},{3}", m_strURL, m_strDestFileName, m_nDownloadSize, m_nTotalSize);
                cfg.Flush();
                if (bClose)
                {
                    cfg.Close();
                    m_cfgFile.Close();
                    m_cfgFile = null;
                }
            }
        }
    }
}
