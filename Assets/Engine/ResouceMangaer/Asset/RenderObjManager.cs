using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    class RenderObjManager
    {
        Transform m_ObjPool = null;
        private static RenderObjManager _inst = null;

        public static RenderObjManager Instance()
        {
            if (_inst == null)
            {
                _inst = new RenderObjManager();
            }

            return _inst;
        }

        public RenderObjManager()
        {
            GameObject go = new GameObject("ObjPool");
            go.SetActive(false);
            m_ObjPool = go.transform;
            UnityEngine.Object.DontDestroyOnLoad(go);
        }
        //特效
        private Dictionary<string, List<IEffect>> m_mapEffectObjIdle = new Dictionary<string, List<IEffect>>();
        private List<string> m_lstCacheEffectObj = new List<string>();
        private Dictionary<int, IEffect> m_mapEffect = new Dictionary<int, IEffect>();
        private List<int> m_lstEffectTodelete = new List<int>();
        //
        private Dictionary<string, List<IGameObject>> m_mapRenderObjIdle = new Dictionary<string, List<IGameObject>>();
        private List<string> m_lstCacheRenderObj = new List<string>();
        // 上一次删除资源时间
        private float m_fElapseTime = 0;
        public IGameObject CreateGameObj(ref string strObjFileName, CreateGameObjectEvent callBack, object custumParam = null, TaskPriority ePriority = TaskPriority.TaskPriority_Normal, bool bCacheObj = true, bool bCacheForever = false)
        {
            List<IGameObject> objList = null;
            IGameObject objNew = null;
            if (bCacheObj)
            {
                if (m_mapRenderObjIdle.TryGetValue(strObjFileName, out objList)) // 缓存池里面有数据
                {
                    if (objList != null)
                    {
                        while (objList.Count > 0)
                        {
                            if (objList[0].GetGameObject() != null)
                            {
                                objNew = objList[0];
                                objList.RemoveAt(0);
                                objNew.SetCustomData(custumParam);
                                if (callBack != null)
                                {
                                    callBack(objNew,custumParam);
                                }
                                return objNew;
                            }
                            else
                            {
                                objList.RemoveAt(0);
                            }
                        }
                    }
                }

                if (!m_lstCacheRenderObj.Contains(strObjFileName))
                {
                    m_lstCacheRenderObj.Add(strObjFileName);
                }
            }

            // 创建对象
            {
                GameObj obj = new GameObj();
                obj.CreateGameObject(ref strObjFileName, callBack, custumParam, ePriority);
                obj.bCacheForever = bCacheForever;
                objNew = obj;
                
                objNew.SetCustomData(custumParam);
            }
            return objNew;
        }

        public IEffect CreateEffect(ref string strObjFileName, TaskPriority ePriority = TaskPriority.TaskPriority_Normal, bool bCacheObj = true, bool bCacheForever = false)
        {
            List<IEffect> objList = null;
            IEffect objNew = null;
            if (bCacheObj)
            {
                if (m_mapEffectObjIdle.TryGetValue(strObjFileName, out objList)) // 缓存池里面有数据
                {
                    if (objList != null)
                    {
                        while (objList.Count > 0)
                        {
                            if (objList[0].Node != null)
                            {
                                objNew = objList[0];
                                objList.RemoveAt(0);
                                m_mapEffect.Add(objNew.code, objNew);
                                return objNew;
                            }
                            else
                            {
                                objList.RemoveAt(0);
                            }
                        }
                    }
                }

                if (!m_lstCacheEffectObj.Contains(strObjFileName))
                {
                    m_lstCacheEffectObj.Add(strObjFileName);
                }
            }

            Effect obj = new Effect();
            obj.Create(ref strObjFileName,ePriority);
            obj.bCacheForever = bCacheForever;
            objNew = obj;
            m_mapEffect.Add(objNew.code, objNew);
            return objNew;
        }
        public void RemoveGameObj(IGameObject renderObj)
        {
            if (renderObj == null)
            {
                return;
            }

            List<IGameObject> objList = null;
            string strObjName = renderObj.GetResName();
            if (m_lstCacheRenderObj.Contains(strObjName))
            {
                if (renderObj.GetGameObject() == null)
                {
                    renderObj.Destroy();
                    Utility.Log.Error("RemoveGameObj error {0}",renderObj.GetResName());
                    return;
                }
                if (!m_mapRenderObjIdle.TryGetValue(renderObj.GetResName(), out objList)) // 缓存池里面有数据
                {
                    objList = new List<IGameObject>();
                    m_mapRenderObjIdle.Add(renderObj.GetResName(), objList);
                }
                renderObj.IdleStartTime = Time.realtimeSinceStartup;
                objList.Add(renderObj);
                renderObj.SetParent(m_ObjPool);
            }
            else
            {
                renderObj.Destroy();
                renderObj = null;
            }
        }

        public void RemoveEffectObj(IEffect effect)
        {
            if (effect == null)
            {
                return;
            }
            m_mapEffect.Remove(effect.code);
            List<IEffect> objList = null;
            string strObjName = effect.GetResName();
            if (m_lstCacheEffectObj.Contains(strObjName))
            {
                if (effect.Node == null)
                {
                    effect.Destroy();
                    Utility.Log.Error("RemoveEffect error {0}", effect.GetResName());
                    return;
                }
                if (!m_mapEffectObjIdle.TryGetValue(effect.GetResName(), out objList)) // 缓存池里面有数据
                {
                    objList = new List<IEffect>();
                    m_mapEffectObjIdle.Add(effect.GetResName(), objList);
                }
                effect.Node.SetActive(false);
                effect.IdleStartTime = Time.realtimeSinceStartup;
                objList.Add(effect);
                effect.SetParent(m_ObjPool);
            }
            else
            {
                effect.Destroy();
                effect = null;
            }
        }

        public void AddRemoveEffectId(int id)
        {
            m_lstEffectTodelete.Add(id);
        }

        public void Update(float dt)
        {
            m_fElapseTime += dt;
            if (m_fElapseTime < 0.5f)
            {
                return;
            }
            {
                Dictionary<string, List<IGameObject>>.Enumerator iter = m_mapRenderObjIdle.GetEnumerator();
                while (iter.MoveNext()) // 缓存池里面有数据
                {
                    List<IGameObject> lstRemove = iter.Current.Value;
                    for (int i = 0, imax = lstRemove.Count; i < imax; ++i)
                    {
                        var item = lstRemove[i];
                        if (item.bCacheForever)
                        {
                            continue;
                        }
                        if (Time.realtimeSinceStartup - item.IdleStartTime > 60)
                        {
                            AssetManager.Instance().RemoveGameObjFromPool(item);
                            lstRemove[i].Destroy(); //一个个慢慢删除避免GC过大造成卡顿
                            lstRemove.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            {
                // 特效更新时间
                Dictionary<int, IEffect>.Enumerator effectiter = m_mapEffect.GetEnumerator();
                while (effectiter.MoveNext()) 
                {
                    IEffect e = effectiter.Current.Value;
                    if (e != null)
                    {
                        e.Update();
                    }
                    
                }
                // 移除播放完成的特效
                for (int i = 0, imax = m_lstEffectTodelete.Count; i < imax; i++)
                {
                    IEffect obj = null;
                    if (m_mapEffect.TryGetValue(m_lstEffectTodelete[i], out obj))
                    {
                        if (obj != null)
                        {
                            obj.OnComplete();
                            RemoveEffectObj(obj);
                        }
                    }
                }
                m_lstEffectTodelete.Clear();
                //删除特效
                Dictionary<string, List<IEffect>>.Enumerator effectMaoIter = m_mapEffectObjIdle.GetEnumerator();
                while (effectMaoIter.MoveNext()) // 缓存池里面有数据
                {
                    List<IEffect> lstRemove = effectMaoIter.Current.Value;
                    for (int i = 0, imax = lstRemove.Count; i < imax; ++i)
                    {
                        var item = lstRemove[i];
                        if (item.bCacheForever)
                        {
                            continue;
                        }
                        if (Time.realtimeSinceStartup - item.IdleStartTime > 60)
                        {
                            lstRemove[i].Destroy(); 
                            lstRemove.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            m_fElapseTime = 0;
        }

        public void Clear()
        {
            Dictionary<string, List<IGameObject>>.Enumerator iter = m_mapRenderObjIdle.GetEnumerator();
            while (iter.MoveNext()) // 缓存池里面有数据
            {
                for (int i = 0; i < iter.Current.Value.Count; ++i)
                {
                    if (iter.Current.Value[i] != null)
                    {
                        iter.Current.Value[i].Destroy();
                    }
                }

                iter.Current.Value.Clear();
            }

            m_mapRenderObjIdle.Clear();
        }
    }
}
