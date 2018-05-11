using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Utility
{
    public class FileUtils
    {
        // 复制文件回调
        public delegate void CopyFileCallback(string strFileName);

        private static FileUtils _instance = null;

        private static string ApplicationStreamingPath = "";
        private static string ApplicationPersistentDataPath = "";
        private static string ApplicationTemporaryDataPath = "";

        public enum UnityPathType
        {
            UnityPath_Resources = 0,    // Unity内置路径，只支持Resource.Load方法读取 
            UnityPath_StreamAsset,      // Unity流目录，只读目录
            UnityPath_PersistentData,   // Untity持久目录， 支持读写 文本文件或者二进制文件可以存放 IOS下该目录会自动备份到iCound
            UnityPath_TemporaryCache,   // Unity临时缓存目录 功能同 UnityPath_PersistentData IOS下不会自动备份到iCound
            UnityPath_CustomPath,       // 用户自定义路径 即ResourcePath 
        }

        public static FileUtils Instance()
        {
            if (_instance == null)
            {
                _instance = new FileUtils();
            }
            return _instance;
        }

        public FileUtils()
        {
            if (Application.platform == RuntimePlatform.Android) /// Application.streamingAssetsPath在安卓平台下已经自带了 jar:file:///
            {
                ApplicationStreamingPath = Application.dataPath + "!/assets/";
            }
            else
            {
                ApplicationStreamingPath = Application.streamingAssetsPath + "/";
            }

            ApplicationPersistentDataPath = Application.persistentDataPath + "/";
            ApplicationTemporaryDataPath = Application.temporaryCachePath + "/";

        }

        // 资源路径
        public string ResourcePath
        {
            set;
            get;
        }

        public string FullPathFileName(ref string strFileName, UnityPathType pathType = UnityPathType.UnityPath_PersistentData)
        {
            string strFullPath = "";

            switch (pathType)
            {
                case UnityPathType.UnityPath_StreamAsset:
                    strFullPath = ApplicationStreamingPath + strFileName;
                    break;
                case UnityPathType.UnityPath_PersistentData:
                    strFullPath = ApplicationPersistentDataPath + strFileName;
                    break;
                case UnityPathType.UnityPath_TemporaryCache:
                    strFullPath = ApplicationTemporaryDataPath + strFileName;
                    break;
                case UnityPathType.UnityPath_CustomPath:
                    strFullPath = ResourcePath + strFileName;
                    break;
            }

            return strFullPath;
        }

		#region C++ Plugin读文件

		/**
        @brief 获取文件长度
        @param strFileName 相对路径
        @param nThreadIndex 线程索引
        */
		private int GetFileLength(string strFileName, int nThreadIndex = 0)
		{
			int nFileSize = 0;
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.IPhonePlayer) // MAC读取文件
			{
				try
				{
					nFileSize = EngineNativeMethod.GetFileLength_MAC(strFileName, nThreadIndex);
				}
				catch (Exception e)
				{
					Utility.Log.Error("C++Plugin获取文件{0}失败:{1}", strFileName, e.ToString());
				}
			}
			else
			{
				try
				{
					nFileSize = EngineNativeMethod.GetFileLength(strFileName, nThreadIndex);
				}
				catch (Exception e)
				{
					Utility.Log.Error("C++Plugin获取文件{0}失败:{1}", strFileName, e.ToString());
				}
			}

			return nFileSize;
		}

		private int GetFileBuff(string strFileName, [MarshalAs(UnmanagedType.LPArray)] ref byte[] lpBuffer, int nStartPos, int nSize, int nThreadIndex = 0)
		{
			int nRet = 0;
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.IPhonePlayer) // MAC读取文件
			{
				try
				{
					nRet = EngineNativeMethod.GetFileBuff_MAC(strFileName, ref lpBuffer, nStartPos, nSize, nThreadIndex);
				}
				catch (Exception e)
				{
					Utility.Log.Error("C++Plugin读取文件{0}失败:{1}", strFileName, e.ToString());
				}
			}
			else
			{
				try
				{
					nRet = EngineNativeMethod.GetFileBuff(strFileName, ref lpBuffer, nStartPos, nSize, nThreadIndex);
				}
				catch (Exception e)
				{
					Utility.Log.Error("C++Plugin读取文件{0}失败:{1}", strFileName, e.ToString());
				}

			}

			return nRet;
		}

		//读文件 使用相对路径 先尝试外部存储卡，再尝试包内文件 需要外部先设置自定义资源路径
		public byte[] ReadFile(string strFileName, int nStartPos = 0, int nSize = 0, int nThreadIndex = 0)
		{
			byte[] buff = null;
			GetBinaryFileBuff(strFileName, out buff, nStartPos, nSize, nThreadIndex);
			return buff;
		}

		// 读文件 使用相对路径 先尝试外部存储卡，再尝试包内文件 需要外部先设置自定义资源路径
		public string ReadTextFile(string strFileName, int nStartPos = 0, int nSize = 0, int nThreadIndex = 0)
		{
			string strContent = "";
			GetTextFileBuff(strFileName, out strContent, nThreadIndex);
			return strContent;
		}

		// 读取二进制数据 注意 数据使用完成需要将 buff置null 以释放内存
		// strFileName 外面读取资源时，全部使用相对路径（相对于资源目录）
		public int GetBinaryFileBuff(string strFileName, out byte[] buff, int nStartPos = 0, int nSize = 0, int nThreadIndex = 0)
		{
			string strResPath = FullPathFileName(ref strFileName, UnityPathType.UnityPath_CustomPath); // 资源路径
																									   //Log.Trace("GetBinaryFileBuff:{0}", strResPath);
			int nFileSize = 0;
			try
			{
				nFileSize = GetFileLength(strResPath, nThreadIndex);
				if (nFileSize == 0)
				{
					strResPath = FullPathFileName(ref strFileName, UnityPathType.UnityPath_StreamAsset);
					nFileSize = GetFileLength(strResPath, nThreadIndex);
					//Log.Trace("GetBinaryFileBuff3:{0}", strResPath);
				}
			}
			catch (System.Exception ex)
			{
				Log.Error("GetBinaryFileBuff {0} Filed! {1}", strResPath, ex);
			}

			if (nFileSize == 0)
			{
				buff = null;
				Utility.Log.Error("读取文件失败：{0}不存在!", strFileName);
				return 0;
			}

			if (nSize == 0)
			{
				nSize = nFileSize;
			}

			buff = new byte[nSize];

			int nRet = 0;
			try
			{
				nRet = GetFileBuff(strResPath, ref buff, nStartPos, nSize, nThreadIndex);
			}
			catch (System.Exception ex)
			{
				Log.Error("GetBinaryFileBuff {0} Filed! {1}", strResPath, ex);
			}
			return nRet;
		}

		// 读取文本文件 返回UTF-8编码数据
		public int GetTextFileBuff(string strFileName, out string strBuff, int nTheadIndex = 0)
		{
			byte[] buff = null;
			int nSize = GetBinaryFileBuff(strFileName, out buff, nTheadIndex);
			if (nSize == 0)
			{
				buff = null;
				strBuff = "";
				return nSize;
			}
			strBuff = System.Text.Encoding.UTF8.GetString(buff);
			buff = null;
			return nSize;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  使用绝对路径读取文件
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="buff"></param>
        /// <returns></returns>
		public bool ReadFile(string strFileName, out byte[] buff)
		{
			int nFileSize = GetFileLength(strFileName);
			if (nFileSize == 0)
			{
				buff = null;
				Utility.Log.Error("读取文件失败：{0}不存在!", strFileName);
				return false;
			}

			buff = new byte[nFileSize];
			GetFileBuff(strFileName, ref buff, 0, nFileSize);

			return true;
		}

        /// <summary>
        /// 使用绝对路径读取文件
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="strBuff"></param>
        /// <returns></returns>
		public bool ReadFile(string strFileName, out string strBuff)
		{
			strBuff = "";
			int nFileSize = GetFileLength(strFileName);
			if (nFileSize == 0)
			{
				Utility.Log.Error("读取文件失败：{0}不存在!", strFileName);
				return false;
			}

			byte[] buff = new byte[nFileSize];
			GetFileBuff(strFileName, ref buff, 0, nFileSize);
			strBuff = System.Text.Encoding.UTF8.GetString(buff);
			buff = null;
			return true;
		}

		#endregion

		public string GetFileMD5(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(file);
            file.Close();
            string md5str = System.BitConverter.ToString(bytes);
            md5str = md5str.Replace("-", "");
            md5str = md5str.ToLower();
            return md5str;
        }

        /// <summary>
        /// 递归创建目录
        /// </summary>
        /// <param name="strDir">String dir.</param>
        public void CreateDir(string strDir)
        {
            string strTemp = strDir.Replace("\\", "/");
            if (!Directory.Exists(strTemp))
            {
                int pos = strDir.LastIndexOf('/');
                if (pos == -1)
                {
                    Directory.CreateDirectory(strTemp);
                }
                else
                {
                    CreateDir(strTemp.Substring(0, pos));
                    Directory.CreateDirectory(strTemp);
                }
            }
        }

        /// <summary>
        /// Clears the directory.
        /// </summary>
        /// <param name="strDir">目录名</param>
        /// <param name="bRemove">是否删除目录本身</param>
        public void ClearDirectory(string strDir, bool bRemove = false)
        {
            DirectoryInfo info = new DirectoryInfo(strDir);
            ClearDirectory(info, bRemove);
        }

        private void ClearDirectory(FileSystemInfo info, bool bRemove = false)
        {
            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录 
            if (dir == null) return;
            if (!info.Exists) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                {
                    file.Delete(); // 删除文件
                }
                else
                {
                    ClearDirectory(files[i], true);
                }
            }

            if (bRemove)
            {
                dir.Delete();
            }
        }

        /// <summary>
        /// 获取目录下文件列表
        /// </summary>
        /// <param name="strDir">String dir.</param>
        /// <param name="lstFile">Lst file.</param>
        public void GetFileList(string strDir, ref List<string> lstFile)
        {
            strDir = strDir.Replace("\\", "/");
            DirectoryInfo dir = new DirectoryInfo(strDir);
            if (dir == null)
            {
                return;
            }
            if (!dir.Exists)
            {
                return;
            }

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                {
                    lstFile.Add(file.FullName);
                }
                else
                {
                    string strFullDirName = files[i].FullName;
                    GetFileList(strFullDirName, ref lstFile);
                }
            }
        }

        /// <summary>
        /// 拷贝目录下所有文件
        /// </summary>
        /// <param name="strSrcDir"></param>
        /// <param name="strDestDir"></param>
        /// <param name="lstFile"></param>
        /// <param name="lstFilter"></param>
        /// <param name="callback"></param>
        public void CopyDirectory(string strSrcDir, string strDestDir, ref List<string> lstFile, List<string> lstFilter = null, CopyFileCallback callback = null,bool toLower = false)
        {
            strSrcDir = strSrcDir.Replace("\\", "/");
            strDestDir = strDestDir.Replace("\\", "/");
            if (toLower)
            {
                strDestDir = strDestDir.ToLower();
                strSrcDir = strSrcDir.ToLower();
            }

            DirectoryInfo src = new DirectoryInfo(strSrcDir);
            if (!src.Exists)
            {
                return;
            }
            DirectoryInfo dest = new DirectoryInfo(strDestDir);
            if (!dest.Exists)
            {
                CreateDir(strDestDir);
            }

            if (strSrcDir.LastIndexOf('/') != strSrcDir.Length - 1)
            {
                strSrcDir += "/";
            }

            DirectoryInfo dir = src as DirectoryInfo;
            //不是目录 
            if (dir == null) return;
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                {
                    string strDestFileName = strDestDir + "/";
                    strDestFileName += file.Name;

                    string strExt = Path.GetExtension(strDestFileName);
                    if (lstFilter != null && lstFilter.Contains(strExt))
                    {
                        continue;
                    }

                    file.CopyTo(strDestFileName.ToLower(), true);
                    if (lstFile != null)
                    {
                        lstFile.Add(file.FullName);
                    }

                    if (callback != null)
                    {
                        callback(file.FullName);
                    }
                }
                else
                {
                    string strFullDirName = files[i].FullName;
                    strFullDirName = strFullDirName.Replace("\\", "/");
                    DirectoryInfo srcDir = new DirectoryInfo(strSrcDir);
                    strSrcDir = srcDir.FullName.Replace("\\", "/");
                    string strDir = strFullDirName.Replace(strSrcDir, "");
                    string strSubDir = strDestDir + "/";
                    strSubDir += strDir;
                    if (toLower)
                    {
                        strFullDirName = strFullDirName.ToLower();
                        strSubDir = strSubDir.ToLower();
                    }
                    CopyDirectory(strFullDirName, strSubDir, ref lstFile, lstFilter, callback,toLower);
                }
            }
        }

        /// <summary>
        /// 写文件 写可读可写空间里的文本文件
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="info"></param>
        public void WriteStreamFile(ref string strName, string info)
        {
            WriteStreamFile(ref strName, info, true);
        }

        /**   用于写可读写空间的文本文件
           * name：文件的名称    
           *  info：写入的内容    
           */
        public void WriteStreamFile(ref string strName, string info, bool overwrite)
        {
            //文件流信息  
            int pos = strName.LastIndexOf('/');
            string strDir = "";
            if (pos > 0)
            {
                strDir = strName.Substring(0, pos);
                CreateDir(strDir);
            }

            StreamWriter sw;
            FileInfo t = new FileInfo(strName);
            if (!t.Exists)
            {
                //如果此文件不存在则创建

                sw = t.CreateText();
            }
            else
            {
                if (overwrite)
                {
                    sw = t.CreateText();
                }
                else
                {
                    sw = t.AppendText();
                }
            }
            sw.Write(info);
            //关闭流  
            sw.Close();
            //销毁流  
            sw.Dispose();
        }

        public void LoadHttpURL(string strURL, WWWLoad.WWWCallback callback, GameObject root, object param = null, bool bCache = false)
        {
            if (!strURL.StartsWith("http://"))
            {
                return;
            }

            //  加载数据
            {
                if (root == null)
                {
                    Log.Error("Engine没有初始化,root为null!");
                    return;
                }

                WWWLoad www = root.GetComponent<WWWLoad>();
                if (www == null)
                {
                    www = root.AddComponent<WWWLoad>();
                }

                if (www != null)
                {
                    www.LoadURL(strURL, callback, param, bCache);
                }
            }
        }
    }
}