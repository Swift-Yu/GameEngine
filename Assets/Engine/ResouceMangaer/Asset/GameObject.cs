using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Engine
{
    public delegate void CreateGameObjectEvent(IGameObject obj, object param = null);

    class GameObj : IGameObject
    {

        IPrefab m_prefab = null;
        CreateGameObjectEvent m_callback = null;

        GameObject m_obj = null;

        object m_customData = null;

        string m_strObjName = "";

        public GameObject gameObject
        {
            get { return m_obj; }
        }

        public Transform transform
        {
            get { return m_obj != null ? m_obj.transform : null; }
        }
        float m_fIdleStartTime = 0;
        public float IdleStartTime
        { 
            get { return m_fIdleStartTime; } 
            set { m_fIdleStartTime = value; }
        }
        public bool bCacheForever { get; set; }

        private List<IAsset> lstDepends = new List<IAsset>();
        public void CreateGameObject(ref string strObjName, CreateGameObjectEvent callBack, object custumParam = null, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_callback = callBack;
            m_strObjName = strObjName;
            m_prefab = AssetManager.Instance().CreatePrefab(strObjName, OnCreatePrefab, custumParam, ePriority);
        }

        void OnCreatePrefab(IPrefab obj, object param)
        {
            m_prefab = obj;
            if (m_prefab != null)
            {
                if (m_prefab.GetObj() != null)
                {
                    m_obj = GameObject.Instantiate(m_prefab.GetObj()) as GameObject;
                    RefreshShader(m_obj);
                    AssetManager.Instance().AddGameObjPool(this);

                    if (m_callback != null)
                    {
                        m_callback(this, param);
                    }
                }

            }
            else
            {
                Utility.Log.Error("Create gameobject failed {0}", m_strObjName);
            }
        }

        public void AddDepends(List<IAsset> lst)
        {
            lstDepends.AddRange(lst);
        }

        public static void RefreshShader(GameObject obj)
        {
            if (Application.isEditor)
            {
                Renderer[] pss = obj.GetComponentsInChildren<Renderer>(true);
                if (pss != null)
                {
                    foreach (Renderer ps in pss)
                    {
                        if (ps.sharedMaterials != null)
                        {
                            Material[] mtarr = ps.sharedMaterials;
                            if (mtarr != null)
                            {
                                for (int i = 0, len = mtarr.Length; i < len; i++)
                                {
                                    if (mtarr[i] == null)
                                    {
                                        continue;
                                    }
                                    Shader shader = Shader.Find(mtarr[i].shader.name);
                                    if (shader == null)
                                    {
                                        Debug.LogError("Not found shader " + mtarr[i].shader.name);
                                    }
                                    else
                                    {
                                        mtarr[i].shader = shader;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Release()
        {
            for (int i = 0; i < lstDepends.Count; i++)
            {
                lstDepends[i].Release();
            }
            RenderObjManager.Instance().RemoveGameObj(this);
        }

        public void Destroy()
        {
            if (m_obj != null)
            {
                GameObject.DestroyImmediate(m_obj);
            }
            if (m_prefab != null)
            {
                m_prefab.Release();
            }
        }

        public GameObject GetGameObject()
        {
            return m_obj;
        }

        public string GetResName()
        {
            return m_strObjName;
        }

        public void SetVisible(bool visible)
        {
            if (m_obj != null && m_obj.activeSelf != visible)
            {
                m_obj.SetActive(visible);
            }
        }
        
        public void SetLayer(int layer)
        {
            if (m_obj != null)
            {
                m_obj.layer = layer;
                SetChildLayer(m_obj.transform, layer);
            }
        }

        private void SetChildLayer(Transform trans,int layer)
        {
            foreach (Transform item in trans)
            {
                if (item.childCount > 0)
                {
                    SetChildLayer(item, layer);
                }
                item.gameObject.layer = layer;
            }
        }
        public void SetCustomData(object data)
        {
            m_customData = data;
        }

        public object GetCustomData()
        {
            return m_customData;
        }

        public void SetParent(Transform trans)
        {
            if (m_obj != null )
            {
                m_obj.transform.SetParent(trans);
            }
        }

        public void SetLocalPos(Vector3 pos)
        {
            if (m_obj != null)
            {
                m_obj.transform.localPosition = pos;
            }
        }

        public Vector3 GetLocalPos()
        {
            if (m_obj != null)
            {
                return m_obj.transform.localPosition;
            }
            return Vector3.zero;
        }
        public void SetLocalRotation(Vector3 rot)
        {
            if (m_obj != null)
            {
                m_obj.transform.localEulerAngles = rot;
            }
        }


        public void SetScale(Vector3 scale)
        {
            if (m_obj != null)
            {
                m_obj.transform.localScale = scale;
            }
        }


    }
}