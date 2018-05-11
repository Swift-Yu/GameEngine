using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Upgrade
{
    public interface IGameEntry 
    {
        void ChangeState(GameState stateID, object param);

        string GetRemoteVersionFileUrl();

        string GetLocalFilePath();

        GameObject GetGameObj();

        void EnterGame();

        void SetActive(bool active);
    }
}
