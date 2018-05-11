using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using System.Threading;

namespace Engine.Upgrade
{
    class CopyFile : GameStateBase
    {
        private static Utility.EngineNativeMethod.PluginCallbackDelegate PluginCallback = new Utility.EngineNativeMethod.PluginCallbackDelegate(CopyFile.OnPluginCallback);

        static IGameEntry m_owner = null;
        public CopyFile(Utility.StateMachine<IGameEntry> machine)
            : base(machine)
        {
            m_nStateID = (int)GameState.CopyFile;
        }

        // 进入状态
        public override void Enter(object param)
        {
            Utility.Log.Info("CopyFile.Enter");
            m_owner = m_Statemachine.GetOwner();
            if (!Application.isEditor)
            {
                string strDataPath = Application.dataPath;
                Thread t = new Thread(() =>
                {
                    // 更新资源文件
                    Utility.EngineNativeMethod.ExtractZip(strDataPath, GameUpgrade.localFilePath, PluginCallback, "assets", "assets/bin");
                });
                t.Start();
            }
            else
            {
                GameUpgrade.Instance.ReadLocalVersion();
                // 版本检测
                m_owner.ChangeState(GameState.CHKVersion, false);
            }
        }


        [MonoPInvokeCallbackAttribute(typeof(EngineNativeMethod.PluginCallbackDelegate))]
        private static void OnPluginCallback(int nCur, int nTotal, string strName, IntPtr param)
        {
            Engine.ThreadHelper.RunOnMainThread(() =>
            {
                //Utility.Log.Info("CopyFile Callback {0}", (GameUpgrade.Instance.callback == null) ? 0 : 1);

                if (GameUpgrade.Instance.callback != null)
                {
                    GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_CopyStreamFile, nCur, nTotal,UpgradeDesc.CopyStreamFile);
                }

                Utility.Log.Info("CopyFile Callback {2} {0}/{1}", nCur, nTotal, strName);

                // 表示已经完成，切换地图
                if (nCur >= nTotal)
                {
                    GameUpgrade.Instance.ReadLocalVersion();
                    // 版本检测
                    m_owner.ChangeState(GameState.CHKVersion, false);
                }
            });
        }

    }


}
