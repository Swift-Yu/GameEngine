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
    public class UtilityLogWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Utility.Log), L, translator, 0, 0, 0, 0);
			
			
			
			
			
			Utils.EndObjectRegister(typeof(Utility.Log), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Utility.Log), L, __CreateInstance, 12, 5, 5);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenLog", _m_OpenLog_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Info", _m_Info_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Error", _m_Error_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Warning", _m_Warning_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Trace", _m_Trace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenGroup", _m_OpenGroup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CloseGroup", _m_CloseGroup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogGroup", _m_LogGroup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HandleLog", _m_HandleLog_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFromStack", _m_ReadFromStack_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Close", _m_Close_xlua_st_);
            
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Utility.Log));
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableLog", _g_get_EnableLog);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MaxLogLevel", _g_get_MaxLogLevel);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "m_bWriteLogFile", _g_get_m_bWriteLogFile);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "logException", _g_get_logException);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "m_lstOpenGroup", _g_get_m_lstOpenGroup);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableLog", _s_set_EnableLog);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MaxLogLevel", _s_set_MaxLogLevel);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "m_bWriteLogFile", _s_set_m_bWriteLogFile);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "logException", _s_set_logException);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "m_lstOpenGroup", _s_set_m_lstOpenGroup);
            
			Utils.EndClassRegister(typeof(Utility.Log), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Utility.Log does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenLog_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    Utility.Log.LogLevel maxLevel;translator.Get(L, 1, out maxLevel);
                    bool bWriteLogFile = LuaAPI.lua_toboolean(L, 2);
                    
                    Utility.Log.OpenLog( maxLevel, bWriteLogFile );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Info_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string strFormat = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    Utility.Log.Info( strFormat, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Error_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string strFormat = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    Utility.Log.Error( strFormat, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Warning_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string strFormat = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    Utility.Log.Warning( strFormat, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Trace_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string strFormat = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    Utility.Log.Trace( strFormat, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenGroup_xlua_st_(RealStatePtr L)
        {
            
            
            
            try {
                
                {
                    string strGroupName = LuaAPI.lua_tostring(L, 1);
                    
                    Utility.Log.OpenGroup( strGroupName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseGroup_xlua_st_(RealStatePtr L)
        {
            
            
            
            try {
                
                {
                    string strGroupName = LuaAPI.lua_tostring(L, 1);
                    
                    Utility.Log.CloseGroup( strGroupName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogGroup_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string strGroup = LuaAPI.lua_tostring(L, 1);
                    string strFormat = LuaAPI.lua_tostring(L, 2);
                    object[] args = translator.GetParams<object>(L, 3);
                    
                    Utility.Log.LogGroup( strGroup, strFormat, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleLog_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    string logString = LuaAPI.lua_tostring(L, 1);
                    string stackTrace = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.LogType type;translator.Get(L, 3, out type);
                    
                    Utility.Log.HandleLog( logString, stackTrace, type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFromStack_xlua_st_(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
            try {
                
                {
                    System.Collections.Generic.List<string> OutList = (System.Collections.Generic.List<string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<string>));
                    
                    Utility.Log.ReadFromStack( ref OutList );
                    translator.Push(L, OutList);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close_xlua_st_(RealStatePtr L)
        {
            
            
            
            try {
                
                {
                    
                    Utility.Log.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableLog(RealStatePtr L)
        {
            
            try {
			    LuaAPI.lua_pushboolean(L, Utility.Log.EnableLog);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MaxLogLevel(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    translator.Push(L, Utility.Log.MaxLogLevel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_bWriteLogFile(RealStatePtr L)
        {
            
            try {
			    LuaAPI.lua_pushboolean(L, Utility.Log.m_bWriteLogFile);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_logException(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    translator.Push(L, Utility.Log.logException);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_lstOpenGroup(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    translator.Push(L, Utility.Log.m_lstOpenGroup);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableLog(RealStatePtr L)
        {
            
            try {
			    Utility.Log.EnableLog = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MaxLogLevel(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			Utility.Log.LogLevel __cl_gen_value;translator.Get(L, 1, out __cl_gen_value);
				Utility.Log.MaxLogLevel = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_bWriteLogFile(RealStatePtr L)
        {
            
            try {
			    Utility.Log.m_bWriteLogFile = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_logException(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    Utility.Log.logException = translator.GetDelegate<Utility.Log.LogException>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_lstOpenGroup(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    Utility.Log.m_lstOpenGroup = (System.Collections.Generic.List<string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
