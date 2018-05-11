using System;
using System.Collections.Generic;
using System.IO;

namespace Engine.Upgrade
{
    class CheckVersion : GameStateBase
    {
        IGameEntry m_owner = null;
        public CheckVersion(Utility.StateMachine<IGameEntry> machine)
            : base(machine)
        {
            m_nStateID = (int)GameState.CHKVersion;
            m_owner = m_Statemachine.GetOwner();
        }

        // 进入状态
        public override void Enter(object param)
        {
            bool bRomete = (bool)param;
            if (bRomete)
            {
                // 获取远程版本号 // www
                Utility.EventEngine.Instance().AddEventListener((int)MainEvent.GET_REMOTE_VERSIONFILE_OK, OnEvent);
                Utility.Log.Info("GetRemoteVersion");
                if (GameUpgrade.Instance.callback != null)
                {
                    GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_GetRomoteVersionList, 0, 0, UpgradeDesc.GetRomoteVersion);
                }
                GameUpgrade.Instance.GetRemoteVersion();
            }
            else
            {
                // 读取本地版本文件
                GameUpgrade.Instance.ReadLocalVersion();
                // 执行版本比较
                GameUpgrade.Instance.Execute();
            }
        }

        public override void OnEvent(int nEventID, object param)
        {
            if (nEventID == (int)MainEvent.GET_REMOTE_VERSIONFILE_OK)
            {
                Utility.Log.Info("Upgrade.Excute");
                string strVersionFile = GameUpgrade.Instance.VersionFileName;
                string strLocalVersionFile = Utility.FileUtils.Instance().FullPathFileName(ref strVersionFile, Utility.FileUtils.UnityPathType.UnityPath_CustomPath);
                if (!File.Exists(strLocalVersionFile))
                {
                    m_owner.ChangeState(GameState.CopyFile, null);
                }
                else
                {
                    // 读取本地版本文件
                    GameUpgrade.Instance.ReadLocalVersion();
                    // 询问是否需要下载
                    GameUpgrade.Instance.Execute();
                }
            }
        }
    }
}
