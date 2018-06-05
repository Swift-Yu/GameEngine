#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class EngineAssetManagerWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Engine.AssetManager), L, translator, 0, 12, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddGameObjPool", _m_AddGameObjPool);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveGameObjFromPool", _m_RemoveGameObjFromPool);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CrateTexture", _m_CrateTexture);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateAtlas", _m_CreateAtlas);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateUIFont", _m_CreateUIFont);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreatePrefab", _m_CreatePrefab);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateSprite", _m_CreateSprite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateGameObject", _m_CreateGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateAudio", _m_CreateAudio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateEffect", _m_CreateEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearObjPool", _m_ClearObjPool);
			
			
			
			
			Utils.EndObjectRegister(typeof(Engine.AssetManager), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Engine.AssetManager), L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Instance", _m_Instance_xlua_st_);
            
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.AssetManager));
			
			
			Utils.EndClassRegister(typeof(Engine.AssetManager), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Engine.AssetManager __cl_gen_ret = new Engine.AssetManager();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Instance_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    
                        Engine.AssetManager __cl_gen_ret = Engine.AssetManager.Instance(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddGameObjPool(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    Engine.IGameObject obj = (Engine.IGameObject)translator.GetObject(L, 2, typeof(Engine.IGameObject));
                    
                    __cl_gen_to_be_invoked.AddGameObjPool( obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveGameObjFromPool(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    Engine.IGameObject obj = (Engine.IGameObject)translator.GetObject(L, 2, typeof(Engine.IGameObject));
                    
                    __cl_gen_to_be_invoked.RemoveGameObjFromPool( obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CrateTexture(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateTextureEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string strTextureName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateTextureEvent callBack = translator.GetDelegate<Engine.CreateTextureEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.ITexture __cl_gen_ret = __cl_gen_to_be_invoked.CrateTexture( strTextureName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateTextureEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string strTextureName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateTextureEvent callBack = translator.GetDelegate<Engine.CreateTextureEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.ITexture __cl_gen_ret = __cl_gen_to_be_invoked.CrateTexture( strTextureName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateTextureEvent>(L, 3)) 
                {
                    string strTextureName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateTextureEvent callBack = translator.GetDelegate<Engine.CreateTextureEvent>(L, 3);
                    
                        Engine.ITexture __cl_gen_ret = __cl_gen_to_be_invoked.CrateTexture( strTextureName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CrateTexture!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateAtlas(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateAtlasEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateAtlasEvent callBack = translator.GetDelegate<Engine.CreateAtlasEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.IUIAtlas __cl_gen_ret = __cl_gen_to_be_invoked.CreateAtlas( stratlasName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateAtlasEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateAtlasEvent callBack = translator.GetDelegate<Engine.CreateAtlasEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.IUIAtlas __cl_gen_ret = __cl_gen_to_be_invoked.CreateAtlas( stratlasName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateAtlasEvent>(L, 3)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateAtlasEvent callBack = translator.GetDelegate<Engine.CreateAtlasEvent>(L, 3);
                    
                        Engine.IUIAtlas __cl_gen_ret = __cl_gen_to_be_invoked.CreateAtlas( stratlasName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateAtlas!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateUIFont(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateFontEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateFontEvent callBack = translator.GetDelegate<Engine.CreateFontEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.IUIFont __cl_gen_ret = __cl_gen_to_be_invoked.CreateUIFont( stratlasName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateFontEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateFontEvent callBack = translator.GetDelegate<Engine.CreateFontEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.IUIFont __cl_gen_ret = __cl_gen_to_be_invoked.CreateUIFont( stratlasName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateFontEvent>(L, 3)) 
                {
                    string stratlasName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateFontEvent callBack = translator.GetDelegate<Engine.CreateFontEvent>(L, 3);
                    
                        Engine.IUIFont __cl_gen_ret = __cl_gen_to_be_invoked.CreateUIFont( stratlasName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateUIFont!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreatePrefab(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreatePrefabEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreatePrefabEvent callBack = translator.GetDelegate<Engine.CreatePrefabEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.IPrefab __cl_gen_ret = __cl_gen_to_be_invoked.CreatePrefab( strPrefabName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreatePrefabEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreatePrefabEvent callBack = translator.GetDelegate<Engine.CreatePrefabEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.IPrefab __cl_gen_ret = __cl_gen_to_be_invoked.CreatePrefab( strPrefabName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreatePrefabEvent>(L, 3)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreatePrefabEvent callBack = translator.GetDelegate<Engine.CreatePrefabEvent>(L, 3);
                    
                        Engine.IPrefab __cl_gen_ret = __cl_gen_to_be_invoked.CreatePrefab( strPrefabName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreatePrefab!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateSprite(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateSpriteEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string strSpriteName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateSpriteEvent callBack = translator.GetDelegate<Engine.CreateSpriteEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.ISprite __cl_gen_ret = __cl_gen_to_be_invoked.CreateSprite( strSpriteName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateSpriteEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string strSpriteName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateSpriteEvent callBack = translator.GetDelegate<Engine.CreateSpriteEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.ISprite __cl_gen_ret = __cl_gen_to_be_invoked.CreateSprite( strSpriteName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateSpriteEvent>(L, 3)) 
                {
                    string strSpriteName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateSpriteEvent callBack = translator.GetDelegate<Engine.CreateSpriteEvent>(L, 3);
                    
                        Engine.ISprite __cl_gen_ret = __cl_gen_to_be_invoked.CreateSprite( strSpriteName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateSprite!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateGameObject(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 7&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateGameObjectEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateGameObjectEvent callBack = translator.GetDelegate<Engine.CreateGameObjectEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    bool bCacheObj = LuaAPI.lua_toboolean(L, 6);
                    bool bCacheForever = LuaAPI.lua_toboolean(L, 7);
                    
                        Engine.IGameObject __cl_gen_ret = __cl_gen_to_be_invoked.CreateGameObject( strPrefabName, callBack, custumParam, ePriority, bCacheObj, bCacheForever );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateGameObjectEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateGameObjectEvent callBack = translator.GetDelegate<Engine.CreateGameObjectEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    bool bCacheObj = LuaAPI.lua_toboolean(L, 6);
                    
                        Engine.IGameObject __cl_gen_ret = __cl_gen_to_be_invoked.CreateGameObject( strPrefabName, callBack, custumParam, ePriority, bCacheObj );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateGameObjectEvent>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<Engine.TaskPriority>(L, 5)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateGameObjectEvent callBack = translator.GetDelegate<Engine.CreateGameObjectEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 5, out ePriority);
                    
                        Engine.IGameObject __cl_gen_ret = __cl_gen_to_be_invoked.CreateGameObject( strPrefabName, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateGameObjectEvent>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateGameObjectEvent callBack = translator.GetDelegate<Engine.CreateGameObjectEvent>(L, 3);
                    object custumParam = translator.GetObject(L, 4, typeof(object));
                    
                        Engine.IGameObject __cl_gen_ret = __cl_gen_to_be_invoked.CreateGameObject( strPrefabName, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.CreateGameObjectEvent>(L, 3)) 
                {
                    string strPrefabName = LuaAPI.lua_tostring(L, 2);
                    Engine.CreateGameObjectEvent callBack = translator.GetDelegate<Engine.CreateGameObjectEvent>(L, 3);
                    
                        Engine.IGameObject __cl_gen_ret = __cl_gen_to_be_invoked.CreateGameObject( strPrefabName, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateGameObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateAudio(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<Engine.CreateAudioEvent>(L, 4)&& translator.Assignable<object>(L, 5)&& translator.Assignable<Engine.TaskPriority>(L, 6)) 
                {
                    string strAudioName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Transform root = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    Engine.CreateAudioEvent callBack = translator.GetDelegate<Engine.CreateAudioEvent>(L, 4);
                    object custumParam = translator.GetObject(L, 5, typeof(object));
                    Engine.TaskPriority ePriority;translator.Get(L, 6, out ePriority);
                    
                        Engine.IAudioSource __cl_gen_ret = __cl_gen_to_be_invoked.CreateAudio( strAudioName, root, callBack, custumParam, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<Engine.CreateAudioEvent>(L, 4)&& translator.Assignable<object>(L, 5)) 
                {
                    string strAudioName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Transform root = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    Engine.CreateAudioEvent callBack = translator.GetDelegate<Engine.CreateAudioEvent>(L, 4);
                    object custumParam = translator.GetObject(L, 5, typeof(object));
                    
                        Engine.IAudioSource __cl_gen_ret = __cl_gen_to_be_invoked.CreateAudio( strAudioName, root, callBack, custumParam );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<Engine.CreateAudioEvent>(L, 4)) 
                {
                    string strAudioName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Transform root = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    Engine.CreateAudioEvent callBack = translator.GetDelegate<Engine.CreateAudioEvent>(L, 4);
                    
                        Engine.IAudioSource __cl_gen_ret = __cl_gen_to_be_invoked.CreateAudio( strAudioName, root, callBack );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateAudio!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.TaskPriority>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    string strEffectName = LuaAPI.lua_tostring(L, 2);
                    Engine.TaskPriority ePriority;translator.Get(L, 3, out ePriority);
                    bool bCacheObj = LuaAPI.lua_toboolean(L, 4);
                    bool bCacheForever = LuaAPI.lua_toboolean(L, 5);
                    
                        Engine.IEffect __cl_gen_ret = __cl_gen_to_be_invoked.CreateEffect( strEffectName, ePriority, bCacheObj, bCacheForever );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.TaskPriority>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string strEffectName = LuaAPI.lua_tostring(L, 2);
                    Engine.TaskPriority ePriority;translator.Get(L, 3, out ePriority);
                    bool bCacheObj = LuaAPI.lua_toboolean(L, 4);
                    
                        Engine.IEffect __cl_gen_ret = __cl_gen_to_be_invoked.CreateEffect( strEffectName, ePriority, bCacheObj );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Engine.TaskPriority>(L, 3)) 
                {
                    string strEffectName = LuaAPI.lua_tostring(L, 2);
                    Engine.TaskPriority ePriority;translator.Get(L, 3, out ePriority);
                    
                        Engine.IEffect __cl_gen_ret = __cl_gen_to_be_invoked.CreateEffect( strEffectName, ePriority );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string strEffectName = LuaAPI.lua_tostring(L, 2);
                    
                        Engine.IEffect __cl_gen_ret = __cl_gen_to_be_invoked.CreateEffect( strEffectName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AssetManager.CreateEffect!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    float dt = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.Update( dt );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearObjPool(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AssetManager __cl_gen_to_be_invoked = (Engine.AssetManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.ClearObjPool(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
