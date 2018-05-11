using System;
using System.Collections.Generic;
using Utility;

namespace Engine.Upgrade
{
    public class DownloadInfo
    {
        public string strNewFileURL;
        public string strDestFile;
        public string strMD5; // 下载文件的MD5 用于文件校验
    }
    class DownloadFile : GameStateBase
    {
        private DownloadInfo m_DownloadInfo = null;

        public DownloadFile(Utility.StateMachine<IGameEntry> machine)
            : base(machine)
        {
            m_nStateID = (int)GameState.DownLoad;

        }
        public override void Enter(object param)
        {

            m_DownloadInfo = param as DownloadInfo;
            if (m_DownloadInfo == null)
            {
                return;
            }

            // 设置回调
            Engine.HttpDownload.Instance().SetDownloadCallback(OnDownFinish, OnDownProgress);
            // 下载文件
            Log.Trace("DownloadFile {0}", m_DownloadInfo.strNewFileURL);
            Engine.HttpDownload.Instance().DownLoadFile(m_DownloadInfo.strNewFileURL, m_DownloadInfo.strDestFile);
        }
        public override void Leave()
        {
            Engine.HttpDownload.Instance().SetDownloadCallback(null, null);
        }

        public override void Update(float dt)
        {
            Engine.HttpDownload.Instance().Run();
        }

        private void OnDownFinish(Engine.HttpDownloadError eError, string strResName)
        {
            switch (eError)
            {
                case Engine.HttpDownloadError.HttpDownloadError_NetExp:
                    {
                        // 弹出提示，然后再转换状态
                        Engine.ThreadHelper.RunOnMainThread(() =>
                        {
                            if (GameUpgrade.Instance.callback != null)
                            {
                                GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_DownloadFileError, 0, 0, UpgradeDesc.DownloadFileError);
                            }
                        });
                        break;
                    }
                case Engine.HttpDownloadError.HttpDownloadError_Completed:
                    {
                        // 进行文件校验
                        // 下载文件出错，需要重新下载
                        if (!CompareFileMD5(ref strResName, ref m_DownloadInfo.strMD5))
                        {
                            // 下载文件
                            Engine.HttpDownload.Instance().DownLoadFile(m_DownloadInfo.strNewFileURL, m_DownloadInfo.strDestFile);
                        }
                        else
                        {
                            Engine.ThreadHelper.RunOnMainThread(() =>
                            {
                                if (m_Statemachine != null)
                                {
                                    // 更新下载的文件
                                    // strResName 下载后的文件名称
                                    Log.Trace("下载完毕 {0}", strResName);
                                    m_Statemachine.ChangeState((int)GameState.Update, strResName);
                                }
                            });
                        }

                        break;
                    }
                case Engine.HttpDownloadError.HttpDownloadError_Downloading:
                    {
                        break;
                    }
            }
        }
        private void OnDownProgress(string strResName, int nDownloadSize, int nTotalSize)
        {
            if (nTotalSize == 0)
            {
                return;
            }

            Engine.ThreadHelper.RunOnMainThread(() =>
            {
                if (GameUpgrade.Instance.callback != null)
                {
                    GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_DownloadFile, nDownloadSize, nTotalSize, UpgradeDesc.DownloadFile);
                }
            });
        }
        //-------------------------------------------------------------------------------------------------------
        /**
        @brief 校验文件的MD5码
        @param 
        */
        private bool CompareFileMD5(ref string strFielName, ref string strMD5)
        {
            return strMD5 == Utility.FileUtils.Instance().GetFileMD5(strFielName);
        }
    }
}
