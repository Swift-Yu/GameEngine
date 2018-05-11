using System;
using System.Collections.Generic;
using UnityEngine;


namespace Engine
{
    public delegate void CreateSpriteEvent(ISprite obj, object param = null);

    class UISprite : ISprite
    {
        private Sprite m_sprite = null;
        private CreateSpriteEvent m_callback = null;
        private AssetBundleResource m_res = null;
        public void CrateSprite(ref string strFontName, CreateSpriteEvent callBack, object custumParam = null,
TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_callback = callBack;
            m_res = ResourceManager.Instance().GetAssetBundle(ref strFontName, LoadFinishDelegate, custumParam, ePriority);

        }

        void LoadFinishDelegate(IResource res, string strResName, object customParam)
        {
            m_res = res as AssetBundleResource;

            if (m_res == null)
            {
                return;
            }

            if (m_res.m_objRes != null && m_res.m_objRes is Sprite)
            {
                m_sprite = m_res.m_objRes as Sprite;

                if (m_callback != null)
                {
                    m_callback(this, customParam);
                }

                return;
            }

            string strSpriteName = "";
            int index = strResName.LastIndexOf("/");
            if (index != -1)
            {
                strSpriteName = strResName.Substring(index + 1);
            }
            index = strSpriteName.LastIndexOf(".");
            if (index != -1)
            {
                strSpriteName = strSpriteName.Substring(0, index);
            }

            m_sprite = m_res.LoadAsset(strSpriteName, typeof(Sprite)) as Sprite;
            m_res.m_objRes = m_sprite;

            if (m_callback != null)
            {
                m_callback(this, customParam);
            }
        }

        public Sprite GetSprite()
        {
            return m_sprite;
        }

        public void Release()
        {
            if (m_res != null)
            {
                m_res.Release();
            }
        }
    }
}
