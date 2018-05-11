using System;
using System.Collections.Generic;
using UnityEngine;


namespace Engine
{
    public interface IAudioSource : IAsset
    {
        AudioSource GetSource();
        bool isPlaying{get;}
        bool mute { get; set; }
        float volume { get; set; }
        void Stop();
    }
}
