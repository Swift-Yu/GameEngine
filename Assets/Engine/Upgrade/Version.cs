/********************************************************************
	创建日期:	2017:9:17  19:29
	文件名称: 	Version.cs
	创 建 人:	zhaochengxue
	版权所有: 	途游深圳研发中心
	说    明:	版本号
*********************************************************************/
using System;
using System.Collections.Generic;

namespace Engine.Upgrade
{
    /// <summary>
    /// 版本号定义 0.0.0 主版本.小版本.资源版本
    /// </summary>
    public class VersionInfo : IComparer<VersionInfo>
    {
        //版本号字符串
        private string m_strVersion = string.Empty;
        //
        private int m_nMainVer = 0;
        //
        private int m_nFullVer = 0;
        //
        private int m_nResVer = 0;


        public int mainVer
        {
            get { return m_nMainVer; }
            set
            {
                m_nMainVer = value;
            }
        }

        public int fullVer
        {
            get { return m_nFullVer; }
            set { m_nFullVer = value; }
        }

        public int resVer
        {
            get { return m_nResVer; }
            set { m_nResVer = value; }
        }

        public static VersionInfo zero 
        {
            get { return new VersionInfo(); } 
        }

        public VersionInfo()
        {

        }

        public VersionInfo(string strVersion)
        {
            Parse(ref strVersion);
        }

        public VersionInfo(int nMain,int nFull,int nRes)
        {
            m_nMainVer = nMain;
            m_nFullVer = nFull;
            m_nResVer = nRes;

            m_strVersion = string.Format("{0}.{1}.{2}", nMain, nFull, nRes);
        }

        /// <summary>
        /// 解析版本号
        /// </summary>
        /// <param name="strVersion"></param>
        /// <returns></returns>
        public bool Parse(ref string strVersion)
        {
            m_strVersion = strVersion;

            List<string> lstVerstring = new List<string>(3);
            Utility.StringUtility.SplitString(ref m_strVersion, ".", out lstVerstring);

            if (lstVerstring.Count != 3)
            {
                Utility.Log.Error("版本号格式错误！");
                return false;
            }
            try
            {
                m_nMainVer = int.Parse(lstVerstring[0]);
                m_nFullVer = int.Parse(lstVerstring[1]);
                m_nResVer = int.Parse(lstVerstring[2]);
            }
            catch (System.Exception ex)
            {
                Utility.Log.Error("版本号格式错误！{0}",ex.ToString());
            }

            return true;
        }

        /// <summary>
        /// 从版本文件中读取
        /// </summary>
        /// <param name="strVerConfigFile"></param>
        /// <returns></returns>
        public bool ReadFromFile(ref string strVerConfigFile)
        {
            string strVer = Utility.FileUtils.Instance().ReadTextFile(strVerConfigFile);
            if (string.IsNullOrEmpty(strVer))
            {
                m_nMainVer = 0;
                m_nFullVer = 0;
                m_nResVer = 0;
                return false;
            }
            return Parse(ref strVer);
        }

        public void SaveToFile(ref string strVerConfigFile)
        {
            m_strVersion = this.ToString();
            Utility.FileUtils.Instance().WriteStreamFile(ref strVerConfigFile, m_strVersion);
        }

        /// <summary>
        /// 比较ver1是否小于ver2
        /// </summary>
        /// <param name="ver1"></param>
        /// <param name="ver2"></param>
        /// <returns></returns>
        public static bool operator <(VersionInfo ver1, VersionInfo ver2)
        {
            if (ver1.m_nMainVer < ver2.m_nMainVer)
            {
                return true;
            }
            else if (ver1.m_nMainVer > ver2.m_nMainVer)
            {
                return false;
            }

            if (ver1.m_nFullVer < ver2.m_nFullVer)
            {
                return true;
            }
            else if (ver1.m_nFullVer > ver2.m_nFullVer)
            {
                return false;
            }

            if (ver1.m_nResVer < ver2.m_nResVer)
            {
                return true;
            }
            else if (ver1.m_nResVer > ver2.m_nResVer)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 比较ver1是否大于ver2
        /// </summary>
        /// <param name="ver1"></param>
        /// <param name="ver2"></param>
        /// <returns></returns>
        public static bool operator >(VersionInfo ver1, VersionInfo ver2)
        {
            if (ver1.m_nMainVer < ver2.m_nMainVer)
            {
                return false;
            }
            else if (ver1.m_nMainVer > ver2.m_nMainVer)
            {
                return true;
            }

            if (ver1.m_nFullVer < ver2.m_nFullVer)
            {
                return false;
            }
            else if (ver1.m_nFullVer > ver2.m_nFullVer)
            {
                return true;
            }

            if (ver1.m_nResVer < ver2.m_nResVer)
            {
                return false;
            }
            else if (ver1.m_nResVer > ver2.m_nResVer)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断两个版本号是否相等
        /// </summary>
        /// <param name="ver1"></param>
        /// <param name="ver2"></param>
        /// <returns></returns>
        public static bool operator ==(VersionInfo ver1, VersionInfo ver2)
        {
            if (ver1.m_nMainVer != ver2.m_nMainVer)
            {
                return false;
            }

            if (ver1.m_nFullVer != ver2.m_nFullVer)
            {
                return false;
            }

            if (ver1.m_nResVer != ver2.m_nResVer)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsFullVersion()
        {
            if (m_nResVer == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断两个版本号是否不等于
        /// </summary>
        /// <param name="ver1"></param>
        /// <param name="ver2"></param>
        /// <returns></returns>
        public static bool operator !=(VersionInfo ver1, VersionInfo ver2)
        {
            return !ver1.Equals(ver2);
        }

        public override string ToString()
        {
            return m_strVersion = string.Format("{0}.{1}.{2}", m_nMainVer, m_nFullVer, m_nResVer);
        }

        public override bool Equals(object obj)
        {
            return obj is VersionInfo && this == (VersionInfo)obj;
        }

        int CompareTo(VersionInfo other)
        {
            if (this < other)
            {
                return 1;
            }
            else if (this > other)
            {
                return -1;
            }

            return 0;
        }

        public int Compare(VersionInfo x, VersionInfo y)
        {
            return x.CompareTo(y);
        }
    }
}
