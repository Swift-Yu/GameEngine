using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Upgrade
{
    // 远程版本
    struct VersionItem
    {
        public VersionInfo version;     // 版本号
        public string m_strFileUrl;     // 文件下载地址
        public string m_strFileName;    // 文件名称
        public string m_strMD5;         // 文件MD5码
        public int m_nSize;           //更新包大小
        public bool m_IsRes;          //是否是资源包

        public VersionItem(string strVerItem)
        {
            version = VersionInfo.zero;
            m_strFileUrl = "";
            m_strFileName = "";
            m_strMD5 = "";
            m_IsRes = false;
            m_nSize = 0;
            string[] vers = strVerItem.Split(',');
            if (vers.Length == 5)
            {
                version.Parse(ref vers[0]);
                m_strFileUrl = vers[1];
                m_strFileName = vers[2];
                m_strMD5 = vers[3];
                m_nSize = int.Parse(vers[4]);
                if (m_strFileName.Contains(".data") && version.resVer > 0)
                {
                    m_IsRes = true;
                }
            }

        }
    }
}
