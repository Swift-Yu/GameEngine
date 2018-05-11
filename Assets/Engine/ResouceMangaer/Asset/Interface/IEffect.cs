using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public interface IEffect : IAsset
    {
        UnityEngine.GameObject Node
        {
            get;
        }
        bool bCacheForever{get;set;}
        bool bIsPlaying{get;set;}
        int code { get; set; }
        float IdleStartTime { get; set; }
        void SetParent(UnityEngine.Transform trans);
        void Update();

        void Play();
        void Play(Action callback);
        string GetResName();

        void OnComplete();
        void Destroy();
    }
}
