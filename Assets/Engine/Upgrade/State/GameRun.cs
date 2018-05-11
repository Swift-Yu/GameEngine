using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Upgrade
{
    class GameRun : GameStateBase
    {
        private IGameEntry m_Owner = null;

        public GameRun(Utility.StateMachine<IGameEntry> machine)
            : base(machine)
        {
            m_nStateID = (int)GameState.Run;
        }

        public override void Enter(object param)
        {
            base.Enter(param);
            m_Owner = this.m_Statemachine.GetOwner();
            if (m_Owner == null)
            {
                return;
            }

            Utility.Log.Info("GameRun.Enter");

            // 写runVersion
            GameUpgrade.Instance.SaveLocalVersion();

            //加载游戏
            if (GameUpgrade.Instance.callback != null)
            {
                GameUpgrade.Instance.callback(GameUpgrade.UpgradeError.UpgradeError_GameRun, 0, 0, UpgradeDesc.EnterGame);
            }
            // 清理更新模块
            GameUpgrade.Instance.Close();
        }
    }
}
