using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Engine
{
    class AssetBundleResource : IResource
    {
        private AssetBundle m_AssetBundle = null;

        public AssetBundle assetBundle { get { return m_AssetBundle; } }


        public AssetBundleResource(ResourceManagerImpl mgr, int nID) : base(mgr, nID)
        {
            m_eType = EResourceType.EResourceType_AssetBundle;
        }

        public UnityEngine.Object LoadAsset(string name,Type type)
        {
            if (m_AssetBundle != null)
            {
                return m_AssetBundle.LoadAsset(name, type);
            }
            return null;
        }
        public override void Execute()
        {
            // 这里是异步线程
            m_nFileSize = FileUtils.Instance().GetBinaryFileBuff(m_strResFileName, out m_FileBuff, 0, 0, GetPriority() == TaskPriority.TaskPriority_Immediate ? 0 : 1);
        }

        // 主线程中执行
        // 任务完成后处理
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
            AssetBundle ab = null;
            try
            {
#if (UNITY_5)

                ab = AssetBundle.LoadFromMemory(m_FileBuff);
#else
                ab = AssetBundle.CreateFromMemoryImmediate(m_FileBuff);
#endif
            }
            catch (System.Exception ex)
            {
                Log.Error("加载资源出错:{0}", ex.Message);
            }

            m_eState = IResource.EResourceState.EResourceState_Loaded;

            if (ab == null)
            {
                Log.Error("CreateFromMemoryImmediate失败:{0}", m_strResName);
                OnFinish();
                return;
            }

            // 资源加载完成处理
            OnLoaded(ab);

            m_FileBuff = null;

            OnFinish();
        }

        override public void OnLoaded(AssetBundle ab)
        {
            if (m_eState != EResourceState.EResourceState_Loaded)
            {
                return;
            }

            m_AssetBundle = ab;

            base.OnLoaded(ab);
        }

        public override void Destroy()
        {
            base.Destroy();
            if (m_AssetBundle != null)
            {
                m_AssetBundle.Unload(true);
                Utility.Log.Trace("Destroy AssetBundle {0}", m_strResName);
            }
        }

    }
}
