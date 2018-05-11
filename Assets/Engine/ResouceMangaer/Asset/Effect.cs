using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    class Effect :IEffect
    {
        private AssetBundleResource m_res = null;
        private GameObject m_effect = null;
        private GameObject m_node = null;
        // 特效开始时间
        private float m_fStartTime = 0.0f;
        // 持续时间
        private float m_fDruation = 0.0f;
        public GameObject Node
        {
            get { return m_node; }
        }
        float m_fIdleStartTime = 0;
        public float IdleStartTime
        {
            get { return m_fIdleStartTime; }
            set { m_fIdleStartTime = value; }
        }
        public bool bIsPlaying{get;set;}
        //t
        public int code { get; set; }

        public bool bCacheForever{get;set;}
        private string m_strObjName;

        System.Action m_endCallback;
        public void Create(ref string strObjName,TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_strObjName = strObjName;
            m_node = new GameObject("EffectNode");
            m_node.SetActive(false);
            code = GetHashCode();
            m_res = ResourceManager.Instance().GetAssetBundle(ref strObjName, LoadFinishDelegate, null, ePriority);

        }

        public string GetResName()
        {
            return m_strObjName;
        }

        void LoadFinishDelegate(IResource res, string strResName, object customParam)
        {
            m_res = res as AssetBundleResource;
            if (m_res != null)
            {
                if (m_res.m_objRes == null && m_res.assetBundle != null)
                {
                    Object[] objs = m_res.assetBundle.LoadAllAssets(typeof(UnityEngine.Object));
                    if (objs != null && objs.Length > 0)
                    {
                        m_res.m_objRes = objs[0];

                    }
                }
                if (m_effect == null && m_res.m_objRes != null)
                {
                    m_effect = GameObject.Instantiate(m_res.m_objRes) as GameObject;
                    m_effect.transform.SetParent(m_node.transform);
                    GameObj.RefreshShader(m_effect);
                    m_node.name = m_effect.name.Replace("(Clone)", "");
                    m_fDruation = CalcParticleSystemDuration(m_effect.transform);
                }
            }
        }
        
        private float CalcParticleSystemDuration(Transform transform)
        {
            if (transform == null)
            {
                return 0.0f;
            }

            float fDruation = 0.0f;
            ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>(true);
            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.main.loop)
                {
                    fDruation = 0.0f;
                    break;
                }
                else
                {
                    float dunration = 0f;
                    if (ps.emission.rateOverTime.constantMax <= 0)
                    {

                        dunration = ps.main.startDelayMultiplier + ps.main.startLifetimeMultiplier;
                    }
                    else
                    {
                        dunration = ps.main.startDelayMultiplier + Mathf.Max(ps.main.duration, ps.main.startLifetimeMultiplier);
                    }
                    if (dunration > fDruation)
                    {
                        fDruation = dunration;
                    }
                }
            }
            return fDruation;
        }

        public void Update()
        {
            if (m_effect != null && bIsPlaying)
            {
                if (m_fDruation > 0.001f && Time.realtimeSinceStartup - m_fStartTime >= m_fDruation)
                {

                    Release();
                }
            }    
        }

        public void SetParent(Transform trans)
        {
            if (m_node != null)
            {
                m_node.transform.SetParent(trans);
            }
        }

        public void Play()
        {
            bIsPlaying = true;
            m_fStartTime = Time.realtimeSinceStartup;
            m_node.SetActive(true);

        }
        public void Play(System.Action callback)
        {
            m_endCallback = callback;
            Play();
        }

        public void Release()
        {
            bIsPlaying = false;
            m_node.SetActive(false);
            RenderObjManager.Instance().AddRemoveEffectId(code);
        }
        public void OnComplete()
        {
            if (m_endCallback != null)
            {
                m_endCallback();
                m_endCallback = null;
            }
        }
        public void Destroy()
        {
            if (m_node != null)
            {
                GameObject.DestroyImmediate(m_node);
                m_node = null;
            }
            if (m_res != null)
            {
                m_res.Release();
                m_res = null;
            }
        }
    }
}
