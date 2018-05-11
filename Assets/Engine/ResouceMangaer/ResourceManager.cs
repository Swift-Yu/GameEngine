using System;
using System.Collections.Generic;

namespace Engine
{
    class ResourceManager
    {
        static ResourceManager s_Inst = null;
        public static ResourceManager Instance()
        {
            if (null == s_Inst)
            {
                s_Inst = new ResourceManager();
            }

            return s_Inst;
        }

        // 资源ID种子
        private static int s_ResouceIDSeed = 0;
        Dictionary<IResource.EResourceType, ResourceManagerImpl> m_dicResourcesImpl = new Dictionary<IResource.EResourceType, ResourceManagerImpl>();

        ResourceManagerImpl GetMgr(IResource.EResourceType type)
        {
            ResourceManagerImpl mgr = null;
            if (m_dicResourcesImpl.TryGetValue(type,out mgr))
            {
                return mgr;
            }
            mgr = new ResourceManagerImpl();
            m_dicResourcesImpl.Add(type, mgr);
            return mgr;
        }

        public TextureResource GetTexture(ref string strResourceName, LoadFinishDelegate callBack, object custumParam = null,
            TaskPriority ePriority = TaskPriority.TaskPriority_Normal, IResource.ResourceCacheLevel rcl = IResource.ResourceCacheLevel.ResourceCacheLevel_1)
        {
            ResourceManagerImpl mgr = GetMgr(IResource.EResourceType.EResourceType_Texture);
            return (TextureResource)mgr.GetResource(IResource.EResourceType.EResourceType_Texture, ref strResourceName, callBack, custumParam, rcl, ePriority);
        }

        public AssetBundleResource GetAssetBundle(ref string strResourceName, LoadFinishDelegate callBack, object custumParam = null,
    TaskPriority ePriority = TaskPriority.TaskPriority_Immediate, IResource.ResourceCacheLevel rcl = IResource.ResourceCacheLevel.ResourceCacheLevel_1)
        {
            ResourceManagerImpl mgr = GetMgr(IResource.EResourceType.EResourceType_AssetBundle);
            return (AssetBundleResource)mgr.GetResource(IResource.EResourceType.EResourceType_AssetBundle, ref strResourceName, callBack, custumParam, rcl, ePriority);
        }


        public IResource CreateResource(IResource.EResourceType eType, ResourceManagerImpl mgr, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            IResource res = null;
            s_ResouceIDSeed++;
            switch (eType)
            {
                case IResource.EResourceType.EResourceType_AssetBundle:
                    res = new AssetBundleResource(GetMgr(eType), s_ResouceIDSeed);
                    break;
                case IResource.EResourceType.EResourceType_Texture:
                    res = new TextureResource(GetMgr(eType), s_ResouceIDSeed);
                    break;
            }

            res.SetPriority(ePriority);
            return res;
        }

        public void Update(float dt)
        {

            Dictionary<IResource.EResourceType, ResourceManagerImpl>.Enumerator it = m_dicResourcesImpl.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.Update(dt);
            }
        }
    }
}
