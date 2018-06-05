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
    public class EngineAudioManagerWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Engine.AudioManager), L, translator, 0, 18, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTimer", _m_OnTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetListener", _m_SetListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayMusic", _m_PlayMusic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopMusic", _m_StopMusic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMusicVolume", _m_SetMusicVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMusicVolume", _m_GetMusicVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopEffect", _m_StopEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopAllEffect", _m_StopAllEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayUIEffect", _m_PlayUIEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEffectVolume", _m_SetEffectVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEffectVolume", _m_GetEffectVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MuteMusic", _m_MuteMusic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MuteEffect", _m_MuteEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Mute", _m_Mute);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsMute", _m_IsMute);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsPlaying", _m_IsPlaying);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayEffect", _m_PlayEffect);
			
			
			
			
			Utils.EndObjectRegister(typeof(Engine.AudioManager), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Engine.AudioManager), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.AudioManager));
			
			
			Utils.EndClassRegister(typeof(Engine.AudioManager), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Engine.AudioManager __cl_gen_ret = new Engine.AudioManager();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AudioManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTimer(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    uint uTimerID = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.OnTimer( uTimerID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetListener(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    UnityEngine.AudioListener listener = (UnityEngine.AudioListener)translator.GetObject(L, 2, typeof(UnityEngine.AudioListener));
                    
                    __cl_gen_to_be_invoked.SetListener( listener );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayMusic(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string strMusic = LuaAPI.lua_tostring(L, 2);
                    float fDelay = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.PlayMusic( strMusic, fDelay );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string strMusic = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.PlayMusic( strMusic );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AudioManager.PlayMusic!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopMusic(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.StopMusic(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMusicVolume(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    float fVolume = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.SetMusicVolume( fVolume );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMusicVolume(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetMusicVolume(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    uint uid = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.StopEffect( uid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopAllEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.StopAllEffect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayUIEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string strEffect = LuaAPI.lua_tostring(L, 2);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.PlayUIEffect( strEffect );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEffectVolume(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    float fVolume = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.SetEffectVolume( fVolume );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEffectVolume(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetEffectVolume(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MuteMusic(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    bool bMute = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.MuteMusic( bMute );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MuteEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    bool bMute = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.MuteEffect( bMute );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Mute(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    bool bMute = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.Mute( bMute );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsMute(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsMute(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPlaying(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    uint nid = LuaAPI.xlua_touint(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsPlaying( nid );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayEffect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.AudioManager __cl_gen_to_be_invoked = (Engine.AudioManager)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.GameObject obj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string strEffect = LuaAPI.lua_tostring(L, 3);
                    bool bLoop = LuaAPI.lua_toboolean(L, 4);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.PlayEffect( obj, strEffect, bLoop );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.GameObject obj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string strEffect = LuaAPI.lua_tostring(L, 3);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.PlayEffect( obj, strEffect );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.AudioManager.PlayEffect!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
