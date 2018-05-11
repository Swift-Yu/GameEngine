using System;
using System.Collections.Generic;
using UnityEngine;


namespace Engine.Upgrade
{
    public class UpgradeDesc
    {
        public const string NetWorkError = "您的网络不通畅，请您检查下网络设置！";
        public const string GetRomoteVersion = "正在检测版本...";
        public const string DownloadFile = "正在努力下载中...";
        public const string DownloadFileComplete = "下载完毕";
        public const string DownloadFileError = "下载文件出错,请您更换网络环境后再试";
        public const string UpgradeRes = "正在升级资源...";
        public const string UpgradeResComplete = "升级资源完毕！";
        public const string CopyStreamFile = "正在初始化资源（不消耗网络流量），请稍候...";
        public const string EnterGame = "开始进入游戏！";
    }

    public class GameUpgrade : Singleton<GameUpgrade>
    {
        // 本地文件路径
        public static readonly string localFilePath = Application.persistentDataPath + "/";
        public enum UpgradeError
        {
            UpgradeError_NetWorkExp = 0,        // 网络异常 要提示网络不通畅，下载文件失败
            UpgradeError_GetRomoteVersionList,  // 获取远程版本
            UpgradeError_DownloadFile,          // 下载文件 参数 strDesc 文件名称 nCur/nTotal 下载进度
            UpgradeError_DownloadFileComplete,  // 下载文件完成 strDesc 文件名称
            UpgradeError_DownloadFileError,     // 下载文件出错 需要重新下载 参数 strDesc
            UpgradeError_UpgradeRes,            // 升级资源 strDesc 文件名称 nCur/nTotal 升级进度
            UpgradeError_UpgradeResComplete,    // 升级资源完成 strDesc 文件名称
            UpgradeError_CopyStreamFile,        // 复制StreamAsset目录文件到持久化目录
            UpgradeError_GameRun,               // 更新完成 开始游戏
        }

        // 升级更新回调
        public delegate void UpgradeCallback(UpgradeError nErrorCode, int nCur, int nTotal, string strDesc);
        private UpgradeCallback m_callBack = null;     // 升级更新回调
        private Action<int> m_downloadConfirm = null;
        public UpgradeCallback callback
        {
            get { return m_callBack; }
        }
        
        /// <summary>
        /// 当前在更新的插件名称
        /// </summary>
        public string PluginName
        {
            get;
            set;
        }

        public string VersionFileName = "";
        private string m_strRomoteVersionFileUrl = "";  //远程版本文件地址
        private VersionInfo m_localVersion = VersionInfo.zero;     // 本地版本号
        private VersionInfo m_buildinVersion = VersionInfo.zero;     // 安装包内部版本号

        // 远程版本列表
        private List<VersionItem> m_lstRemoteVersion = new List<VersionItem>();
        //当前版本号
        private VersionItem m_curVersion;

        // 需要下载的资源包数量
        private int m_nResPackageCount = 0;
        public int ResPackageCount
        {
            get { return m_nResPackageCount; } 
        }
        // 还需要下载的资源包数量
        private int m_nResPackageRemain = 0;
        public int ResPackageRemain
        {
            get { return m_nResPackageRemain; }
        }
        private IGameEntry m_gameEntry = null;
        public IGameEntry GameEntry
        {
            get { return m_gameEntry; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="callBack"></param>
        public void Init(Utility.StateMachine<IGameEntry> machine, UpgradeCallback upcallBack,Action<int>  confirmCallback)
        {
            m_gameEntry = machine.GetOwner();
            machine.RegisterState(new CheckVersion(machine));
            machine.RegisterState(new CopyFile(machine));
            machine.RegisterState(new DownloadFile(machine));
            machine.RegisterState(new GameRun(machine));
            machine.RegisterState(new GameUpdate(machine));

            m_callBack = upcallBack;
            m_downloadConfirm = confirmCallback;
        }

        public void SetCallback(UpgradeCallback upcallBack, Action<int> confirmCallback)
        {
            m_callBack = upcallBack;
            m_downloadConfirm = confirmCallback;
        }

        public void OnEvent(int nEventID, object param)
        {
            if (nEventID == (int)MainEvent.DOWNLOAD_UPGRADEFILE_CONFIRM)
            {
                DownloadUpgradeFile();
            }
        }

        public void Close()
        {
            m_nResPackageRemain = 0;
            m_nResPackageCount = 0;
            m_lstRemoteVersion.Clear();
            Utility.EventEngine.Instance().RemoveEventListener((int)MainEvent.DOWNLOAD_UPGRADEFILE_CONFIRM, OnEvent);
        }

        //开始检测版本更新
        public void StartUpdate( string strPluginName)
        {
            if (m_gameEntry == null)
            {
                Utility.Log.Error("未初始化");
                return;
            }
            // 更新资源文件确认事件
            Utility.EventEngine.Instance().AddEventListener((int)MainEvent.DOWNLOAD_UPGRADEFILE_CONFIRM, OnEvent);
            PluginName = strPluginName.ToLower();
            VersionFileName = PluginName + ".version";
            m_strRomoteVersionFileUrl = m_gameEntry.GetRemoteVersionFileUrl() + PluginName + ".ver";
            m_buildinVersion.ReadFromFile(ref VersionFileName);
            Utility.Log.Trace("StartUpdate {0} ver:{1}",PluginName, m_buildinVersion.ToString());
            m_gameEntry.ChangeState(GameState.CHKVersion, true);
        }

        /// <summary>
        /// 读取本地版本
        /// </summary>
        /// <returns></returns>
        public bool ReadLocalVersion()
        {
            bool ret = m_localVersion.ReadFromFile(ref VersionFileName);
            Utility.Log.Info("ReadLocalVersion {0}", m_localVersion.ToString());
            return ret;
        }

        public void SaveLocalVersion()
        {
            m_localVersion = m_curVersion.version;
            Utility.Log.Info("SaveLocalVersion {0}", m_localVersion.ToString());

            string strLocalVersipnFile = Utility.FileUtils.Instance().FullPathFileName(ref VersionFileName, Utility.FileUtils.UnityPathType.UnityPath_CustomPath);
      
            m_localVersion.SaveToFile(ref strLocalVersipnFile);
        }

        /// <summary>
        /// 获取远程版本号文件列表
        /// </summary>
        /// <returns></returns>
        public bool GetRemoteVersion()
        {
            if (m_lstRemoteVersion.Count > 0)
            {
                return true;
            }
            GameObject root = m_gameEntry.GetGameObj();
            //从服务器下载到本地
            string strURL = m_strRomoteVersionFileUrl + "?m=" + UnityEngine.Random.Range(1, int.MaxValue);
            Utility.FileUtils.Instance().LoadHttpURL(strURL, LoadRemoteFileFinish, root);
            return true;
        }

        private void LoadRemoteFileFinish(WWW www, object param = null)
        {
            if (www.error != null || www.text == string.Empty || www.text.Length == 0)
            {
                return;
            }
            string[] vers = www.text.Split('\n', '\r');
            for (int i = 0; i < vers.Length; i++)
            {
                string verdata = vers[i];
                if (verdata == string.Empty || verdata.Length == 0)
                    continue;
                m_lstRemoteVersion.Add(new VersionItem(vers[i]));

            }
            Utility.Log.Trace("LoadRemoteFileFinish :{0}", m_lstRemoteVersion.Count);
            Utility.EventEngine.Instance().DispatchEvent((int)MainEvent.GET_REMOTE_VERSIONFILE_OK, null);
        }

        public void Execute()
        {
            // 比较版本号 如果有非资源版本号的更新，提示去下载完整包 资源版本更新则进入Download状态
            // 如果没有更新 如果资源版本号是0 则需要将streamingAsset中的资源Copy到SD卡

            // 比较版本号
            if (m_lstRemoteVersion.Count <= 0)
            {
                // 不需要更新
                if (IsNeedCopy())
                {
                    m_gameEntry.ChangeState(GameState.CopyFile, null);
                }
                else
                {
                    m_curVersion.version = m_localVersion;
                    m_gameEntry.ChangeState(GameState.Run, null);
                }
                return;
            }
            Utility.Log.Trace("GameUpgrade.Execute rometeVersion");

            int nNewVersionPos = -1;
            // 首先查找所有比本地版本大的版本列表
            for (int i = 0; i < m_lstRemoteVersion.Count; ++i)
            {
                if (m_lstRemoteVersion[i].version > m_localVersion)
                {
                    nNewVersionPos = i;
                    break;
                }
            }
            Utility.Log.Trace("GameUpgrade.Execute 查找新版本位置 {0}", nNewVersionPos);

            if (nNewVersionPos < 0)
            {
                if (IsNeedCopy())
                {
                    m_gameEntry.ChangeState(GameState.CopyFile, null);
                }
                else
                {
                    m_curVersion.version = m_localVersion;
                    m_gameEntry.ChangeState(GameState.Run, null);
                }
                return;
            }

            m_curVersion = GetLastFullPackage(nNewVersionPos);
            Utility.Log.Trace("GameUpgrade.Execute fullPackage {0}", m_curVersion.version.ToString());
            if (m_curVersion.version != VersionInfo.zero) // 需要更新完整包，弹出下载提示
            {
                //TODO
                //m_State = UpgradeState.UpgradeState_DownloadFullPackage;
                // 弹出提示去下载完整包
                return;
            }

            m_curVersion = GetLastResPackage();
            Utility.Log.Trace("GameUpgrade.Execute resPackage {0} {1}", m_curVersion.version.ToString(), m_curVersion.m_strFileUrl);
            if (m_curVersion.version != VersionInfo.zero)
            {
                // 询问外部是否需要下载更新文件
                if (m_nResPackageCount == m_nResPackageRemain)
                {
                    if (m_downloadConfirm != null)
                    {
                         int size = CalcUpgradeSize();
                         m_downloadConfirm(size);
                    }
                }
                else
                {
                    DownloadUpgradeFile();
                }
                return;
            }
            else
            {
                Utility.Log.Trace("GameUpgrade.Execute final {0} {1}", m_localVersion.ToString(), m_buildinVersion.ToString());
                // 不需要更新 升级完成
                if (IsNeedCopy())
                {
                    m_gameEntry.ChangeState(GameState.CopyFile, null);
                }
                else
                {
                    m_gameEntry.ChangeState(GameState.Run, null);
                }
                return;
            }
        }

        public bool IsNeedCopy()
        {
            return m_buildinVersion > m_localVersion && m_buildinVersion.IsFullVersion();
        }

        private string GetDownloadDir()
        {
            string strFileName = "";
            string strDir = Utility.FileUtils.Instance().FullPathFileName(ref strFileName, Utility.FileUtils.UnityPathType.UnityPath_TemporaryCache);
            strDir += "download/";
            return strDir;
        }
        /// <summary>
        ///  查找最近的资源包
        /// </summary>
        /// <returns></returns>
        private VersionItem GetLastResPackage()
        {
            // m_lstRemoteVersion 必须是按从小到大排好序制作version文件时保证
            List<VersionItem> resList = m_lstRemoteVersion.FindAll(x => x.m_IsRes == true);
            if (resList != null)
            {
                if (resList.Count > 0)
                {
                    for (int i = 0, iCount = resList.Count; i < iCount; i++)
                    {
                        if (resList[i].version.resVer > m_localVersion.resVer // 查找比本地版本小的资源
                            && resList[i].version.mainVer == m_localVersion.mainVer
                            && resList[i].version.fullVer == m_localVersion.fullVer)
                        {
                            return resList[i];
                        }
                    }
                }
            }

            Utility.Log.Trace("没有资源包更新");
            return new VersionItem("");
        }

        private int CalcUpgradeSize()
        {
            Utility.Log.Trace("获取更新逻辑包和资源包大小");
            int nUpgradeSize = 0;

            m_nResPackageCount = 0;
            //资源包
            List<VersionItem> resList = m_lstRemoteVersion.FindAll(x => x.m_IsRes == true);
            if (resList != null)
            {
                if (resList.Count > 0)
                {
                    for (int i = 0, iCount = resList.Count; i < iCount; i++)
                    {
                        if (resList[i].version.resVer > m_localVersion.resVer // 查找比本地版本大的资源
                            && resList[i].version.mainVer == m_localVersion.mainVer
                            && resList[i].version.fullVer == m_localVersion.fullVer)
                        {
                            m_nResPackageCount++;
                            nUpgradeSize += resList[i].m_nSize;
                        }
                    }
                }
            }
            m_nResPackageRemain = m_nResPackageCount;
            Utility.Log.Trace("获取更新逻辑包和资源包总大小：" + nUpgradeSize);
            return nUpgradeSize;
        }

        /// <summary>
        /// 查找最近的完整包URL
        /// </summary>
        /// <param name="nNewVersion"></param>
        /// <returns></returns>
        private VersionItem GetLastFullPackage(int nNewVersion)
        {
            VersionInfo newVersion = m_lstRemoteVersion[m_lstRemoteVersion.Count - 1].version;
            if (newVersion.mainVer <= m_localVersion.mainVer && newVersion.fullVer <= m_localVersion.fullVer)
            {
                return new VersionItem("");
            }

            if (m_lstRemoteVersion.Count == 1)
            {
                if (newVersion.mainVer > m_localVersion.mainVer || newVersion.fullVer > m_localVersion.fullVer)
                {
                    return m_lstRemoteVersion[0];
                }
            }
            else
            {
                for (int i = m_lstRemoteVersion.Count - 2; i >= nNewVersion - 1 && nNewVersion - 1 >= 0; --i)
                {
                    if (m_lstRemoteVersion[i].version.mainVer < newVersion.mainVer || m_lstRemoteVersion[i].version.fullVer < newVersion.fullVer)
                    {
                        return m_lstRemoteVersion[i + 1];
                    }
                }
            }

            return new VersionItem("");
        }


        //-------------------------------------------------------------------------------------------------------
        private void DownloadUpgradeFile()
        {
            if (string.IsNullOrEmpty(m_curVersion.m_strFileName))
            {
                return;
            }

            string strDestFile = GetDownloadDir() + m_curVersion.m_strFileName;
            DownloadInfo info = new DownloadInfo();
            info.strNewFileURL = m_curVersion.m_strFileUrl;
            info.strDestFile = strDestFile;
            info.strMD5 = m_curVersion.m_strMD5;
            m_gameEntry.ChangeState(GameState.DownLoad, info);

            m_nResPackageRemain--;
        }
    }
}
