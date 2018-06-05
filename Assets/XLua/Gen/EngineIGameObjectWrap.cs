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
    public class EngineIGameObjectWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Engine.IGameObject), L, translator, 0, 13, 4, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGameObject", _m_GetGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddDepends", _m_AddDepends);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVisible", _m_SetVisible);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCustomData", _m_SetCustomData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCustomData", _m_GetCustomData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetParent", _m_SetParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalPos", _m_GetLocalPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalPos", _m_SetLocalPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResName", _m_GetResName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalRotation", _m_SetLocalRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLayer", _m_SetLayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetScale", _m_SetScale);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "bCacheForever", _g_get_bCacheForever);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IdleStartTime", _g_get_IdleStartTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "transform", _g_get_transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "gameObject", _g_get_gameObject);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "bCacheForever", _s_set_bCacheForever);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IdleStartTime", _s_set_IdleStartTime);
            
			Utils.EndObjectRegister(typeof(Engine.IGameObject), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Engine.IGameObject), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.IGameObject));
			
			
			Utils.EndClassRegister(typeof(Engine.IGameObject), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Engine.IGameObject does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGameObject(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        UnityEngine.GameObject __cl_gen_ret = __cl_gen_to_be_invoked.GetGameObject(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddDepends(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    System.Collections.Generic.List<Engine.IAsset> lst = (System.Collections.Generic.List<Engine.IAsset>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Engine.IAsset>));
                    
                    __cl_gen_to_be_invoked.AddDepends( lst );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVisible(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    bool visible = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.SetVisible( visible );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCustomData(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    object data = translator.GetObject(L, 2, typeof(object));
                    
                    __cl_gen_to_be_invoked.SetCustomData( data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCustomData(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetCustomData(  );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParent(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    UnityEngine.Transform trans = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    __cl_gen_to_be_invoked.SetParent( trans );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalPos(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        UnityEngine.Vector3 __cl_gen_ret = __cl_gen_to_be_invoked.GetLocalPos(  );
                        translator.PushUnityEngineVector3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPos(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    UnityEngine.Vector3 pos;translator.Get(L, 2, out pos);
                    
                    __cl_gen_to_be_invoked.SetLocalPos( pos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResName(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.GetResName(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalRotation(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    UnityEngine.Vector3 rot;translator.Get(L, 2, out rot);
                    
                    __cl_gen_to_be_invoked.SetLocalRotation( rot );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLayer(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    int layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.SetLayer( layer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetScale(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    UnityEngine.Vector3 scale;translator.Get(L, 2, out scale);
                    
                    __cl_gen_to_be_invoked.SetScale( scale );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bCacheForever(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.bCacheForever);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IdleStartTime(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.IdleStartTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_transform(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.transform);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_gameObject(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.gameObject);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bCacheForever(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.bCacheForever = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IdleStartTime(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Engine.IGameObject __cl_gen_to_be_invoked = (Engine.IGameObject)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IdleStartTime = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
