using System;
using System.Collections.Generic;
using System.Threading;
using Utility;

namespace Engine.Upgrade
{

    class GameUpdate : GameStateBase
    {
        private static int unzipRet = -1;
        private static Utility.EngineNativeMethod.PluginCallbackDelegate PluginCallback = new Utility.EngineNativeMethod.PluginCallbackDelegate(GameUpdate.OnPluginCallback);

        [MonoPInvokeCallbackAttribute(typeof(EngineNativeMethod.PluginCallbackDelegate))]
        private static void OnPluginCallback(int nCur, int nTotal, string strName, IntPtr param)
        {
            Engine.ThreadHelper.RunOnMainThread(() =>
            {
                Utility.Log.Error("表示已经完成,切换状态 " + nCur);
                if (GameUpgrade.Instance.callback != null)
                {
                    if (nCur >= nTotal)
                    {
                        GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_UpgradeResComplete, nCur, nTotal, UpgradeDesc.UpgradeResComplete);
                    }
                    else
                    {
                        GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_UpgradeRes, nCur, nTotal, UpgradeDesc.UpgradeRes);
                    }
                }

                // 表示已经完成,切换状态
                if (nCur >= nTotal)
                {
                    Utility.Log.Error("表示已经完成,切换状态 写入本地版本号");
                    // 写入本地版本号
                    GameUpgrade.Instance.SaveLocalVersion();

                    // 进入版本检测状态
                    GameUpgrade.Instance.GameEntry.ChangeState(GameState.CHKVersion, false);
                }
            });
        }

        public GameUpdate(Utility.StateMachine<IGameEntry> machine)
            : base(machine)
        {
            m_nStateID = (int)GameState.Update;
        }
        
        public override void Enter(object param)
        {
            string strResName = param as string;
            if (strResName == null)
            {
                return;
            }

            string strResPath = m_Statemachine.GetOwner().GetLocalFilePath() + "assets/";
            Log.Trace("解压{0}--->>{1}", strResName, strResPath);
            
            Thread t = new Thread(() =>
            {
                // 更新资源文件
                unzipRet = Utility.EngineNativeMethod.UnPackage(strResName, strResPath, PluginCallback);
                Console.Write(unzipRet);
            });
            t.Start();

        }
    }
}
