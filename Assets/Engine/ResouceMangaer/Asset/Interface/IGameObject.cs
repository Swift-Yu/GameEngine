using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface IGameObject : IAsset
    {
        bool bCacheForever { get; set; }
        float IdleStartTime{ get; set; }
        GameObject GetGameObject();

        void AddDepends(List<IAsset> lst);
        void SetVisible(bool visible);

        void SetCustomData(object data);

        object GetCustomData();

        void SetParent(Transform trans);

        Vector3 GetLocalPos();
        void SetLocalPos(Vector3 pos);

        string GetResName();

        void Destroy();

        void SetLocalRotation(Vector3 rot);

        void SetLayer(int layer);

        void SetScale(Vector3 scale);
        Transform transform
        {
            get;
        }
        GameObject gameObject
        {
            get;
        }

    }
}