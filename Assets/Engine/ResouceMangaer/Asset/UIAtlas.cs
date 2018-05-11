using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public delegate void CreateAtlasEvent(IUIAtlas obj, object param = null);

    class UIAtlas : IUIAtlas
    {
        private AssetBundleResource m_res = null;

        CreateAtlasEvent m_callback = null;
        Dictionary<string, Sprite> m_dicSprite = new Dictionary<string, Sprite>();
        public Sprite GetSprite(string strName)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return null;
            }
            if (m_res == null || m_res.assetBundle == null)
            {
                return null;
            }
            Sprite sprite = null;
            if (!m_dicSprite.TryGetValue(strName, out sprite))
            {
                Utility.Log.Error("GetSprite error {0}", strName);
            }
            return sprite;
        }

        public void CrateAtlas(ref string strTextureName, CreateAtlasEvent callBack, object custumParam = null,TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_callback = callBack;
            m_res = ResourceManager.Instance().GetAssetBundle(ref strTextureName, LoadFinishDelegate, custumParam, ePriority);
        }

        void LoadFinishDelegate(IResource res, string strResName, object customParam)
        {
            m_res = res as AssetBundleResource;
            if (m_res != null && m_res.assetBundle != null)
            {
                Sprite[] sps = m_res.assetBundle.LoadAllAssets<Sprite>();
                foreach (var item in sps)
                {
                    m_dicSprite.Add(item.name,item);
                }
            }
            if (m_callback != null)
            {
                m_callback(this, customParam);
            }
        }

        public void Release()
        {
            if (m_dicSprite != null)
            {
                m_dicSprite.Clear();
                m_dicSprite = null;
            }
            if (m_res != null)
            {
                m_res.Release();
            }
            m_res = null;
        }
    }
}
