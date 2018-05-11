using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public interface IAudio
    {
        void SetListener(AudioListener listener);
        // 播放背景音乐
        void PlayMusic(string strMusic, float fDelay = 0.0f);
        //停止播放
        void StopMusic();
        // 背景音乐音量
        // fVolume  (0~1) 
        void SetMusicVolume(float fVolume);
        float GetMusicVolume();
        // 停止播放
        void StopEffect(uint uid);

        // 停止所音效播放
        void StopAllEffect();

        // 播放UI音效
        uint PlayUIEffect(string strEffect);

        // 音效音量
        // fVolume  (0~1) 
        void SetEffectVolume(float fVolume);
        float GetEffectVolume();

        // 设置静音
        void Mute(bool bMute);

        bool IsMute();

        bool IsPlaying(uint nid);
    }
}
