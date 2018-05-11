using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utility;

namespace Engine
{
    public interface IRefObj
    {
        // 添加引用计数
        void AddRef();

        // 释放资源
        void Release();

        int GetRefCount();
    }
    /// 
    /// 资源 资源中包含预制对象（从AssetBundle中Loadb出来的对象） 
    /// 
    public abstract class IResource : BaseTask, IRefObj
    {
        public enum EResourceState
        {
            EResourceState_UnLoaded = 0,
            EResourceState_Loading = 1,
            EResourceState_Loaded = 2,
            EResourceState_Complete = 3,
        }

        public enum EResourceType
        {
            EResourceType_null = 0, /// 未定义类型
            EResourceType_Texture,  /// tex
            EResourceType_Mesh, /// Mesh rs
            EResourceType_SkeletalMesh, /// mesh+骨骼骨架 rm         
            EResourceType_Effect, /// Effect fx
            EResourceType_Material, // 材质
            EResourceType_Terrain,  // 地表资源
            EResourceType_AssetBundle, //  AssetBundle资源 使用LoadURL加载
        }

        public enum ResourceCacheLevel
        {
            ResourceCacheLevel_NULL = 0,   // 资源缓存0级,立即回收
            ResourceCacheLevel_1,          // 资源缓存1级 * 60s 后回收
            ResourceCacheLevel_2,          // 资源缓存2级
            ResourceCacheLevel_3,          // 资源缓存3级
            ResourceCacheLevel_4,          // 资源缓存4级
            ResourceCacheLevel_5,          // 资源缓存5级
            ResourceCacheLevel_FOREVER,    // 资源长驻内存
        }
        public const float RES_CACHE_TIME = 60f; // 缓存等级单位时间

        // 引用计数
        private int m_nRef = 1;

        private ResourceManagerImpl m_Mgr;  // 资源管理器

        // 资源ID
        protected int m_nResID = 0;

        // 资源文件缓冲
        protected byte[] m_FileBuff = null;
        // 资源缓冲大小
        protected int m_nFileSize = 0;

        public void AddRef() { m_nRef++; }

        // 释放资源
        public void Release()
        {
            m_nRef--;
            if (m_nRef == 0)
            {
                m_Mgr.RemoveResource(m_strResName);
            }
            //Utility.Log.Info("Release Ressource {0}", m_strResFileName);
        }

        public int GetRefCount()
        {
            return m_nRef;
        }

        public int GetID() { return m_nResID; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string m_strResName = "";

        /// <summary>
        /// 资源名称
        /// </summary>
        public string m_strResFileName = "";

        /// <summary>
        /// 资源加载状态
        /// </summary>
        public EResourceState m_eState = EResourceState.EResourceState_UnLoaded;

        /// <summary>
        /// 资源类型
        /// </summary>
        public EResourceType m_eType = EResourceType.EResourceType_null;

        /// <summary>
        /// 资源对象
        /// </summary>

        public UnityEngine.Object m_objRes = null;

        /// <summary>
        /// 父对象
        /// </summary>
        public LoadFinishDelegate loadFinishCallBack = null;

        // 资源空闲时间
        public float m_fIdleStartTime = 0;

        /// <summary>
        /// 自定义对象
        /// </summary>
        public List<object> customParams = new List<object>();

        public ResourceCacheLevel m_eResourceCacheLevel = ResourceCacheLevel.ResourceCacheLevel_NULL;

        public IResource(ResourceManagerImpl mgr, int nID)
            : base(nID)
        {
            m_Mgr = mgr;
        }

        public EResourceType GetResType()
        {
            return m_eType;
        }

        // 资源释放
        virtual public void Destroy()
        {
            if (m_objRes != null)
            {
                GameObject.DestroyImmediate(m_objRes, true);
                m_objRes = null;
                Utility.Log.Trace("Destroy {0}", m_strResName);
            }
            
            loadFinishCallBack = null; // 清空回调聊表
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <returns></returns>
        virtual public bool LoadRes()
        {
            // 投递任务到后台管理器
            DaemonManager.Instance().AddTask(this);
            return false;
        }

        /// <summary>
        /// 加载完毕处理
        /// </summary>
        /// <returns></returns>
        virtual public void OnLoaded(AssetBundle ab)
        {
            m_eState = EResourceState.EResourceState_Complete;
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        virtual public void UnloadRes()
        {

        }

        /// <summary>
        /// 资源加载完毕
        /// </summary>
        virtual public void OnFinish()
        {
            if (loadFinishCallBack != null)
            {
                Delegate[] delegates = loadFinishCallBack.GetInvocationList();
                if (delegates == null)
                {
                    return;
                }
                for (int i = 0; i < delegates.Length; i++)
                {
                    LoadFinishDelegate dele = (LoadFinishDelegate)delegates[i];
                   // Profiler.BeginSample(dele.Method.DeclaringType.FullName + "->" + dele.Method.Name);
                    dele.Invoke(this, m_strResName, customParams[i]);
                    //Profiler.EndSample();
                }

                loadFinishCallBack = null;
                customParams.Clear();
            }
        }

        // 可能会在非主线程执行，注意不能使用unity相关的API
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

            ab.Unload(false);
            ab = null;
            m_FileBuff = null;

            OnFinish();
        }
    }
}
