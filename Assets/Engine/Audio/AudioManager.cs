using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Engine
{
    public class AudioManager : Singleton<AudioManager>, IAudio,Utility.ITimer
    {
        private static uint AUDIO_TIME = 21220;
        // 音乐
        private string m_strMusic = "";
        private IAudioSource m_musicSource = null;
        // 特效
        // 特效IDSeed
        private uint m_uIDSeed = 0;

        // 特效音源
        private Dictionary<uint, IAudioSource> m_fxAudio = new Dictionary<uint, IAudioSource>();
        private List<uint> m_lstRemove = new List<uint>();// 删除列表

        // 是否静音
        private bool m_bMute = false;
        private bool mm_bMute = false;      //背景音乐
        private bool me_bMute = false;      //音效

        // 音量
        private float m_fEffectVolume = 1.0f;
        private float m_fMusicVolume = 1.0f;

        private AudioListener m_listener = null;

        private GameObject m_UIRoot = null;
        private GameObject m_mainCam = null;
        public AudioManager()
        {
            Utility.TimerAxis.Instance().SetTimer(AudioManager.AUDIO_TIME, 1000, this, Utility.TimerAxis.INFINITY_CALL, "AudioTimer");
        }

        public void Close()
        {
            Utility.TimerAxis.Instance().KillTimer(AudioManager.AUDIO_TIME, this);

            if (m_UIRoot != null)
            {
                GameObject.DestroyObject(m_UIRoot);
                m_UIRoot = null;
            }
        }

        public void OnTimer(uint uTimerID)
        {
            Dictionary<uint, IAudioSource>.Enumerator iter = m_fxAudio.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value != null && iter.Current.Value != null)
                {
                    if (!iter.Current.Value.isPlaying)
                    {
                        if (!m_lstRemove.Contains(iter.Current.Key))
                        {
                            m_lstRemove.Add(iter.Current.Key);
                        }

                        iter.Current.Value.Release(); // 释放资源引用
                    }
                }
            }

            // 清理数据
            if (m_lstRemove.Count > 0)
            {
                for (int i = 0; i < m_lstRemove.Count; ++i)
                {
                    m_fxAudio.Remove(m_lstRemove[i]);
                }

                m_lstRemove.Clear();
            }
        }

        public void SetListener(AudioListener listener)
        {
            m_listener = listener;
        }

        public void PlayMusic(string strMusic, float fDelay = 0.0f)
        {

            if (m_strMusic == strMusic)
            {
                return;
            }

            CheckAduioListener();

            GameObject cam = FindMainCamera();
            if (cam == null)
            {
                return;
            }

            if (m_musicSource != null)
            {
                m_musicSource.Release();
                m_musicSource = null;
            }

            m_musicSource = AssetManager.Instance().CreateAudio(strMusic, cam.transform, null);

            if (m_musicSource == null)
            {
                return;
            }

            AudioSource source = m_musicSource.GetSource();
            if (source != null)
            {
                source.priority = 10; // 背景音乐优先级较高
                source.loop = true;
                source.volume = m_fMusicVolume;
                source.playOnAwake = false;
                //source.mute = m_bMute;
                source.mute = mm_bMute;
                source.PlayDelayed(fDelay);
            }
        }

        public void StopMusic()
        {
            if (m_musicSource == null)
            {
                return;
            }

            m_musicSource.Stop();

            m_musicSource.Release();

            m_strMusic = "";
        }

        public void SetMusicVolume(float fVolume)
        {
            if (fVolume == m_fMusicVolume)
            {
                return;
            }

            m_fMusicVolume = fVolume;
            if (m_musicSource != null)
            {
                m_musicSource.volume = m_fMusicVolume;
            }
        }

        public float GetMusicVolume()
        {
            return m_fMusicVolume;
        }

        public void StopEffect(uint uid)
        {
            Dictionary<uint, IAudioSource>.Enumerator iter = m_fxAudio.GetEnumerator();
            IAudioSource ae = null;
            if (m_fxAudio.TryGetValue(uid, out ae))
            {
                if (ae.isPlaying)
                {
                    ae.Stop();
                    ae.Release();
                    m_fxAudio.Remove(uid);
                }
            }
        }

        public void StopAllEffect()
        {
            Dictionary<uint, IAudioSource>.Enumerator iter = m_fxAudio.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value != null)
                {
                    if (iter.Current.Value.isPlaying)
                    {
                        iter.Current.Value.Stop();
                        iter.Current.Value.Release();
                    }
                }
            }

            m_fxAudio.Clear();       
        }

        public uint PlayUIEffect(string strEffect)
        {
            GameObject cam = FindMainCamera();
            if (cam == null)
            {
                return 0;
            }

            if (m_UIRoot == null)
            {
                m_UIRoot = new GameObject();
                m_UIRoot.transform.parent = cam.transform;
                m_UIRoot.hideFlags = HideFlags.HideAndDontSave;
            }

            return PlayEffect(m_UIRoot, strEffect);
        }

        public void SetEffectVolume(float fVolume)
        {
            if (m_fMusicVolume == fVolume)
            {
                return;
            }

            m_fMusicVolume = fVolume;

            if (m_musicSource != null)
            {
                m_musicSource.volume = fVolume;
            }
        }

        public float GetEffectVolume()
        {
            return m_fMusicVolume;
        }

        /// <summary>
        /// 背景音乐设置静音
        /// </summary>
        /// <param name="bMute"></param>
        public void MuteMusic(bool bMute)
        {
            if (mm_bMute == bMute)
            {
                return;
            }

            mm_bMute = bMute;

            // 背景音乐
            if (m_musicSource != null)
            {
                m_musicSource.mute = bMute;
            }
        }

        /// <summary>
        /// 音效设置静音
        /// </summary>
        /// <param name="bMute"></param>
        public void MuteEffect(bool bMute)
        {

            if (me_bMute == bMute)
            {
                return;
            }

            me_bMute = bMute;

            // 音效
            Dictionary<uint, IAudioSource>.Enumerator iter = m_fxAudio.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value != null)
                {
                    if (iter.Current.Value.isPlaying)
                    {
                        iter.Current.Value.mute = bMute;
                    }
                }
            }
        }

        public void Mute(bool bMute)
        {
            if (m_bMute == bMute)
            {
                return;
            }

            m_bMute = bMute;

            // 背景音乐
            if (m_musicSource != null)
            {
                m_musicSource.mute = bMute;
            }

            // 音效
            Dictionary<uint, IAudioSource>.Enumerator iter = m_fxAudio.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value != null)
                {
                    if (iter.Current.Value.isPlaying)
                    {
                        iter.Current.Value.mute = bMute;
                    }
                }
            }
        }

        public bool IsMute()
        {
            return m_bMute;
        }

        public bool IsPlaying(uint nid)
        {
            foreach (var item in m_fxAudio)
            {
                if (item.Key == nid)
                {
                    return item.Value.isPlaying;
                }
            }
            return true;
        }

        void CheckAduioListener()
        {
            if (m_listener == null)
            {
                GameObject cam = FindMainCamera();
                if (cam == null)
                {
                    return;
                }

                AudioListener al = cam.GetComponent<AudioListener>();
                if (al == null)
                {
                    m_listener = cam.AddComponent<AudioListener>();
                }
            }
        }

        // 播放音效
        public uint PlayEffect(GameObject obj, string strEffect, bool bLoop = false)
        {
            if (obj == null)
            {
                return 0;
            }
            CheckAduioListener();


            IAudioSource isource = AssetManager.Instance().CreateAudio(strEffect,obj.transform,null);
            AudioSource source = isource.GetSource();
            if (source == null)
            {
                return 0;
            }
            source.loop = bLoop;
            source.volume = m_fEffectVolume;
            //source.mute = m_bMute;
            source.mute = me_bMute;
            source.spatialBlend = 0.0f;
            source.Play();

            ++m_uIDSeed;
            m_fxAudio.Add(m_uIDSeed, isource);

            return m_uIDSeed;
        }

        private GameObject FindMainCamera()
        {
            if (m_mainCam == null)
            {
                Camera cam = Camera.main;
                if (cam == null)
                {
                    GameObject go = new GameObject("Audio");
                    m_mainCam = go;
                    UnityEngine.Object.DontDestroyOnLoad(go);
                }
                else
                {
                    m_mainCam = cam.gameObject;
                }
            }
            return m_mainCam;
        }

        AudioSource AddAudioResource(GameObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return obj.AddComponent<AudioSource>();
        }
    }
}
