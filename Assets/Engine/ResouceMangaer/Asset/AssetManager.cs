using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class GameObjRef
    {
        public GameObject go;
        public Engine.IGameObject goObj;
        public int hashCode;
    }

    public class AssetManager
    {
        static AssetManager s_Inst = null;
     
        public static AssetManager Instance()
        {
            if (null == s_Inst)
            {
                s_Inst = new AssetManager();
                DaemonManager.Instance().Init();

            }

            return s_Inst;
        }

        Dictionary<int, GameObjRef> m_dicObjRef = new Dictionary<int, GameObjRef>();
        List<int> m_lstObjsRefKey = new List<int>();


        public void AddGameObjPool(IGameObject obj)
        {
            if (obj == null || obj.GetGameObject() == null)
            {
                return;
            }
            int code = obj.GetGameObject().GetHashCode();
            if (!m_dicObjRef.ContainsKey(code))
            {
                m_dicObjRef.Add(code, new GameObjRef() { go = obj.GetGameObject(), hashCode = code, goObj = obj });
            }
        }

        public void RemoveGameObjFromPool(IGameObject obj)
        {
            if (obj == null || obj.GetGameObject() == null)
            {
                return;
            }
            int code = obj.GetGameObject().GetHashCode();
            if (m_dicObjRef.ContainsKey(code))
            {
                m_dicObjRef.Remove(code);
            }
        }

        public ITexture CrateTexture(string strTextureName, CreateTextureEvent callBack, object custumParam = null,
        TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            UITexture tex = new UITexture();
            tex.CrateTexture(ref strTextureName, callBack, custumParam, ePriority);
            return tex;
        }

        public IUIAtlas CreateAtlas(string stratlasName, CreateAtlasEvent callBack, object custumParam = null,
        TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            UIAtlas atlas = new UIAtlas();
            atlas.CrateAtlas(ref stratlasName, callBack, custumParam, ePriority);
            return atlas;
        }

        public IUIFont CreateUIFont(string stratlasName, CreateFontEvent callBack, object custumParam = null,
      TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            UIFont font = new UIFont();
            font.CrateFont(ref stratlasName, callBack, custumParam, ePriority);
            return font;
        }

        public IPrefab CreatePrefab(string strPrefabName, CreatePrefabEvent callBack, object custumParam = null,
      TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            Prefab prefab = new Prefab();
            prefab.CratePrefab(ref strPrefabName, callBack, custumParam, ePriority);
            return prefab;
        }

        public ISprite CreateSprite(string strSpriteName, CreateSpriteEvent callBack, object custumParam = null,
      TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            UISprite sprite = new UISprite();
            sprite.CrateSprite(ref strSpriteName, callBack, custumParam, ePriority);
            return sprite;
        }

        public IGameObject CreateGameObject(string strPrefabName, CreateGameObjectEvent callBack, object custumParam = null,
      TaskPriority ePriority = TaskPriority.TaskPriority_Normal, bool bCacheObj = true, bool bCacheForever = false)
        {
            return RenderObjManager.Instance().CreateGameObj(ref strPrefabName, callBack, custumParam, ePriority, bCacheObj, bCacheForever);
        }

        public IAudioSource CreateAudio(string strAudioName, Transform root, CreateAudioEvent callBack, object custumParam = null, TaskPriority ePriority = TaskPriority.TaskPriority_Immediate)
        {
            AudioAsset audio = new AudioAsset();
            audio.CreateAudio(ref strAudioName, root, callBack, custumParam, ePriority);
            return audio;
        }

        public IEffect CreateEffect(string strEffectName, TaskPriority ePriority = TaskPriority.TaskPriority_Immediate, bool bCacheObj = true, bool bCacheForever = false)
        {
            return RenderObjManager.Instance().CreateEffect(ref strEffectName,ePriority, bCacheObj, bCacheForever);
        }

        public void Update(float dt)
        {
            DaemonManager.Instance().Run();

            Dictionary<int, GameObjRef>.Enumerator it = m_dicObjRef.GetEnumerator();
            m_lstObjsRefKey.Clear();
            while (it.MoveNext())
            {
                if (it.Current.Value.go == null)
                {
                    m_lstObjsRefKey.Add(it.Current.Key);
                    it.Current.Value.goObj.Release();
                }
            }
            for (int i = 0; i < m_lstObjsRefKey.Count; i++)
            {
                m_dicObjRef[m_lstObjsRefKey[i]] = null;
                m_dicObjRef.Remove(m_lstObjsRefKey[i]);
            }




            RenderObjManager.Instance().Update(dt);
            ResourceManager.Instance().Update(dt);
        }

        public void ClearObjPool()
        {
            RenderObjManager.Instance().Clear();
        }
    }
}