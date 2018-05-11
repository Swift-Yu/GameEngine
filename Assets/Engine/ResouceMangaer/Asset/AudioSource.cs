using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public delegate void CreateAudioEvent(IAudioSource obj, object param = null);

    class AudioAsset : IAudioSource
    {
        CreateAudioEvent m_callback = null;
        AssetBundleResource m_res = null;
        AudioSource m_musicSource = null;
        Transform m_root = null;
        public void CreateAudio(ref string strAudioName, Transform root, CreateAudioEvent callBack, object custumParam = null, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            if ( root == null)
            {
                return;
            }
            m_root = root;
            m_callback = callBack;
            m_res = ResourceManager.Instance().GetAssetBundle(ref strAudioName, LoadFinishDelegate, custumParam, ePriority);
        }


        void LoadFinishDelegate(IResource res, string strResName, object customParam)
        {
            m_res = res as AssetBundleResource;
            if (m_res != null && m_res.assetBundle != null)
            {
                AudioClip[] audios = m_res.assetBundle.LoadAllAssets<AudioClip>();
                if (audios == null || audios.Length <= 0)
                {
                    Utility.Log.Error("创建声音文件{0}失败！", strResName);
                    return;
                }
                m_musicSource = m_root.gameObject.AddComponent<AudioSource>();
                m_musicSource.clip = audios[0];
            }
            if (m_callback != null)
            {
                m_callback(this, customParam);
            }
        }

        public AudioSource GetSource()
        {
            return m_musicSource;
        }

        public void Release()
        {
            if (m_res != null)
            {
                m_res.Release(); // 释放资源引用
            }

            if (m_musicSource != null)
            {
                GameObject.DestroyObject(m_musicSource);
            }
        }


        public bool isPlaying
        {
            get { return m_musicSource != null ? m_musicSource.isPlaying : false; }
        }

        public bool mute
        {
            get
            {
                return m_musicSource != null ? m_musicSource.mute : true; 
            }
            set
            {
                if (m_musicSource != null)
                {
                    m_musicSource.mute = value;
                }
            }
        }

        public float volume
        {
            get
            {
                return m_musicSource != null ? m_musicSource.volume : 0f; 
            }
            set {
                if (m_musicSource != null)
                {
                    m_musicSource.volume = value;
                }
            }
        }

        public void Stop()
        {
            if (m_musicSource != null)
            {
                m_musicSource.Stop();
            }
        }
    }
}
