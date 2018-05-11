using System;
using System.Collections.Generic;
using Utility;


namespace Engine.Upgrade
{
    public enum GameState
    {
        Null = 0,       // 无效状态
        CHKVersion = 1, // 从服务器获取版本号
        DownLoad,       // 下载版本文件
        Update,         // 更新版本文件
        CopyFile,       // 完整包，则需要Copy文件到SD卡目录
        Run,            // 游戏运行
    }

    class GameStateBase : Utility.State
    {
        protected Utility.StateMachine<IGameEntry> m_Statemachine = null;

        public GameStateBase(Utility.StateMachine<IGameEntry> machine)
        {
            m_Statemachine = machine;
        }
    }
}
