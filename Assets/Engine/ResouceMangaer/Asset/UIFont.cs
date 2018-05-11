using System;
using System.Collections.Generic;
using UnityEngine;


namespace Engine
{
    public delegate void CreateFontEvent(IUIFont obj, object param = null);

    class UIFont : IUIFont
    {
        private Font m_font = null;
        private CreateFontEvent m_callback = null;
        private AssetBundleResource m_res = null;
         
        public void CrateFont(ref string strFontName, CreateFontEvent callBack, object custumParam = null,
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

            if (m_res.m_objRes != null && m_res.m_objRes is Font)
            {
                m_font = m_res.m_objRes as Font;

                if (m_callback != null)
                {
                    m_callback(this, customParam);
                }

                return;
            }
            string fontName = "";
            int index = strResName.LastIndexOf("/");
            if (index != -1)
            {
                fontName = strResName.Substring(index + 1);
            }
            index = fontName.LastIndexOf(".");
            if (index != -1)
            {
                fontName = fontName.Substring(0,index);
            }
            m_font = m_res.LoadAsset(fontName, typeof(Font)) as Font;
            m_res.m_objRes = m_font;
            if (m_callback != null)
            {
                m_callback(this, customParam);
            }
        }

        public void Release()
        {
            //Engine.Log.Info("FunTexture Clear");
            if (m_res != null)
            {
                m_res.Release();
            }
        }

        public Font GetFont()
        {
            return m_font;
        }
    }
}
