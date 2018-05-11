using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public delegate void CreatePrefabEvent(IPrefab obj, object param = null);

    class Prefab : IPrefab
    {

        private AssetBundleResource m_res = null;

        CreatePrefabEvent m_callback = null;

        private AssetBundleResource[] m_denpenArray = null;
        int m_nLoadCount = 0;
        int m_LoadIndex = 0;
        string m_strPrefabName;
        TaskPriority m_ePriority;
        public void CratePrefab(ref string strPrefabName, CreatePrefabEvent callBack, object custumParam = null, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_callback = callBack;
            m_ePriority = ePriority;
            m_strPrefabName = strPrefabName;
            //custumParam
            if (custumParam is List<string>)
            {
                
                List<string> lstDepend = (List<string>)custumParam;
                if (lstDepend.Count > 0)
                {
                    m_nLoadCount = lstDepend.Count;
                    m_LoadIndex = 0;

                    m_denpenArray = new AssetBundleResource[lstDepend.Count];

                    for (int i = 0; i < lstDepend.Count; i++)
                    {
                        string path = lstDepend[i];
                        AssetBundleResource ab = ResourceManager.Instance().GetAssetBundle(ref path, OnLoadDepende, null, ePriority);
                    }
                }
                else
                {
                    m_res = ResourceManager.Instance().GetAssetBundle(ref m_strPrefabName, LoadFinishDelegate, null, m_ePriority);
                }
            }
            else
            {
                m_res = ResourceManager.Instance().GetAssetBundle(ref m_strPrefabName, LoadFinishDelegate, null, m_ePriority);
            }
        }

        void OnLoadDepende(IResource res, string strResName, object customParam)
        {
            var ab = res as AssetBundleResource;
            if (ab != null)
            {
                m_denpenArray[m_LoadIndex++] = ab;
                if (m_LoadIndex == m_nLoadCount)
                {
                    m_res = ResourceManager.Instance().GetAssetBundle(ref m_strPrefabName, LoadFinishDelegate, null, m_ePriority);
                }
            }
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
            }

            if (m_callback != null)
            {
                m_callback(this, customParam);
            }
        }

        Object IPrefab.GetObj()
        {
            if (m_res == null)
            {
                return null;
            }
            return m_res.m_objRes;
        }
        public void AddRef()
        {
            if (m_res != null)
            {
                m_res.AddRef();
            }
        }
        void IAsset.Release()
        {
            if (m_denpenArray != null)
            {
                for (int i = 0; i < m_denpenArray.Length; i++)
                {
                    if (m_denpenArray[i] != null)
                    {
                        m_denpenArray[i].Release();
                    }
                }
            }

            if (m_res != null)
            {
                m_res.Release();
                //Engine.Log.Info("FunPrefab Clear ref :" + m_res.GetRef());
            }
        }
    }
}