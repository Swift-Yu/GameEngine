using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Engine
{
    class TextureResource : IResource
    {
        AssetBundle m_abRes = null;
        public TextureResource(ResourceManagerImpl mgr, int nID)
            : base(mgr, nID)
        {
            m_eType = EResourceType.EResourceType_Texture;
        }

        /// <summary>
        /// 加载完毕处理
        /// </summary>
        /// <returns></returns>
        override public void OnLoaded(AssetBundle ab)
        {
            if (m_eState != EResourceState.EResourceState_Loaded)
            {
                return;
            }
            string strPath, strFileName, strFileNameNoExt, strExt;
            StringUtility.ParseFileName(ref m_strResName, out strPath, out strFileName, out strFileNameNoExt, out strExt);
            m_objRes = ab.LoadAsset(strFileNameNoExt, typeof(Texture)) as Texture;
            base.OnLoaded(ab);
        }

        public override void EndDo()
        {
            if (m_nFileSize <= 0)
            {
                Log.Error("{0}不存在，请检查资源！", m_strResName);
                m_eState = IResource.EResourceState.EResourceState_Complete;
                OnFinish();
                return;
            }

            // 同步创建assetBundle
           
            try
            {
#if (UNITY_5)

                m_abRes = AssetBundle.LoadFromMemory(m_FileBuff);
#else
                m_abRes = AssetBundle.CreateFromMemoryImmediate(m_FileBuff);
#endif
            }
            catch (System.Exception ex)
            {
                Log.Error("加载资源出错:{0}", ex.Message);
            }

            m_eState = IResource.EResourceState.EResourceState_Loaded;

            if (m_abRes == null)
            {
                Log.Error("CreateFromMemoryImmediate失败:{0}", m_strResName);
                OnFinish();
                return;
            }

            // 资源加载完成处理
            OnLoaded(m_abRes);
            bool unload = true;
            for (int i = 0; i < customParams.Count; i++)
            {
                bool c = (bool)customParams[i];
                if (c == false)
                {
                    unload = false;
                    break;
                }
            }
            if (unload)
            {
                m_abRes.Unload(false);
                m_abRes = null;
            }
            m_FileBuff = null;

            OnFinish();
        }

        public override void Destroy()
        {
            base.Destroy();
            if (m_abRes)
            {
                m_abRes.Unload(false);
            }
        }
    }
}
