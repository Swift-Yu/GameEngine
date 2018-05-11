using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utility;

namespace Engine
{
    /// <summary>
    /// 资源加载回调
    /// </summary>
    /// <param name="wwwObj">www对象</param>
    /// <param name="customParam">自定义参数</param>
    public delegate void LoadFinishDelegate(IResource res, string strResName, object customParam);

    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManagerImpl
    {
        /// <summary>
        /// 资源列表
        /// </summary>
        private Dictionary<string, IResource> m_mapResource = new Dictionary<string, IResource>();

        // 空闲资源列表
        private Dictionary<string, IResource> m_mapIdleResource = new Dictionary<string, IResource>();

        // 加载完成待处理资源列表
        private List<IResource> m_lstCompleteResource = new List<IResource>();

        // 上一次删除资源时间
        private float m_fElapseTime = 0;

        //////////////////////////////////////////////////////////////////////////
        /// 方法
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="eType">资源类型</param>
        /// <param name="strResourceName">资源名称</param>
        /// <param name="callBack">加载完成回调</param>
        /// <param name="custemParam">自定义参数</param>
        /// <param name="rcl">资源缓存等级</param>
        /// <returns></returns>
        public IResource GetResource(IResource.EResourceType eType, ref string strResourceName, LoadFinishDelegate callBack, object custumParam = null,
            IResource.ResourceCacheLevel rcl = IResource.ResourceCacheLevel.ResourceCacheLevel_NULL, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            if (strResourceName == null)
            {
                return null;
            }
            strResourceName = strResourceName.ToLower();
            IResource res = null;
            if (!m_mapResource.TryGetValue(strResourceName, out res))
            {
                if (m_mapIdleResource.TryGetValue(strResourceName, out res))
                {
                    m_mapResource.Add(strResourceName, res);
                    m_mapIdleResource.Remove(strResourceName);
                }
            }

            if (res != null)
            {
                res.AddRef();

                // 缓存等级修正
                if (rcl > res.m_eResourceCacheLevel)
                {
                    res.m_eResourceCacheLevel = rcl;
                }

                if (res.m_eState == IResource.EResourceState.EResourceState_Complete)
                {
                    res.loadFinishCallBack += callBack;
                    res.customParams.Add(custumParam);

                    if (ePriority == TaskPriority.TaskPriority_Immediate)
                    {
                        res.OnFinish();
                    }
                    else
                    {
                        m_lstCompleteResource.Add(res);
                    }
                }
                else
                {
                    res.loadFinishCallBack += callBack;
                    res.customParams.Add(custumParam);
                }
            }
            else
            {
                /// 根据资源后缀名称判定资源类型
                res = ResourceManager.Instance().CreateResource(eType, this, ePriority);
                m_mapResource[strResourceName] = res;
                res.m_strResName = strResourceName;
                res.m_strResFileName = strResourceName;
                res.m_eState = IResource.EResourceState.EResourceState_Loading;
                res.m_objRes = null;
                res.m_eResourceCacheLevel = rcl;
                res.loadFinishCallBack += callBack;
                res.customParams.Add(custumParam);

                // 投递资源加载
                res.LoadRes();
            }

            return res;
        }

        // 删除资源
        public void RemoveResource(string strResourceName)
        {
            IResource res = null;
            if (m_mapResource.TryGetValue(strResourceName, out res))
            {
                // 将资源放入空闲队列，按回收策略回收资源
                res.m_fIdleStartTime = Time.realtimeSinceStartup;
                m_mapIdleResource.Add(strResourceName, res);
                m_mapResource.Remove(strResourceName);
            }
        }

        // 清理资源
        public void Update(float dt)
        {
            m_fElapseTime += dt;
            if (m_fElapseTime < 0.5f)
            {
                return;
            }

            int nNum = 0;
            List<string> lstDelRes = new List<string>();
            Dictionary<string, IResource>.Enumerator it = m_mapIdleResource.GetEnumerator();
            while (it.MoveNext() && nNum < 1)
            {
                IResource res = (IResource)it.Current.Value;
                if (res.m_eState == IResource.EResourceState.EResourceState_Loading)
                {
                    continue;
                }

                if (res.m_eResourceCacheLevel != IResource.ResourceCacheLevel.ResourceCacheLevel_FOREVER
                    && Time.realtimeSinceStartup - res.m_fIdleStartTime > (int)res.m_eResourceCacheLevel * IResource.RES_CACHE_TIME)
                {
                    res.Destroy();
                    lstDelRes.Add(res.m_strResName);
                    res = null;
                    nNum++;

                    // 系统不主动释放资源
                    //Resources.UnloadUnusedAssets();
                }
            }

            for (int i = 0; i < lstDelRes.Count; ++i)
            {
                m_mapIdleResource.Remove(lstDelRes[i]);
            }

            m_fElapseTime = 0.0f;

            // 处理已经加载完成的资源回调
            for (int i = 0; i < m_lstCompleteResource.Count; ++i)
            {
                if (m_lstCompleteResource[i] != null)
                {
                    m_lstCompleteResource[i].OnFinish();
                }
            }
            m_lstCompleteResource.Clear();
        }

        // 立即删除没有使用的资源
        public void UnloadUnusedResources()
        {
            Dictionary<string, IResource>.Enumerator it = m_mapIdleResource.GetEnumerator();
            while (it.MoveNext())
            {
                IResource res = (IResource)it.Current.Value;
                if (res.m_eResourceCacheLevel != IResource.ResourceCacheLevel.ResourceCacheLevel_FOREVER)
                {
                    res.Destroy();
                    res = null;
                }
            }

            m_mapIdleResource.Clear();
            Resources.UnloadUnusedAssets(); // 这里会比较卡 考虑优化或者去掉
        }
    }
}
