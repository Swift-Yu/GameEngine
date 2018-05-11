using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public delegate void CreateTextureEvent(ITexture obj, object param = null);

    class UITexture : ITexture
    {
        private Texture m_tex = null;

        private TextureResource m_res = null;

        private CreateTextureEvent m_callback = null;

        public void CrateTexture(ref string strTextureName, CreateTextureEvent callBack, object custumParam = null,
    TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_callback = callBack;
            m_res = ResourceManager.Instance().GetTexture(ref strTextureName, LoadFinishDelegate, custumParam, ePriority);
        }

        void LoadFinishDelegate(IResource res, string strResName, object customParam)
        {
            if (res == null)
            {
                return;
            }

            if (res.m_objRes != null && res.m_objRes is Texture)
            {
                m_tex = (Texture)res.m_objRes;
                if (m_callback != null)
                {
                    m_callback(this, customParam);
                }
            }
            else
            {
                Utility.Log.Trace("加载贴图{0}失败！", strResName);
            }
        }

        public void Release()
        {
            if (m_res != null)
            {
                m_res.Release();
            }
        }

        public Texture GetTexture()
        {
            return m_tex;
        }
    }
}
