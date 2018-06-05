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
    public class EngineNetServiceWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Engine.NetService), L, translator, 0, 9, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Connect", _m_Connect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckNetLink", _m_CheckNetLink);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Send", _m_Send);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendToMe", _m_SendToMe);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnInitialize", _m_UnInitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWithHttps", _m_SendWithHttps);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWithHttpImmeditor", _m_SendWithHttpImmeditor);
			
			
			
			
			Utils.EndObjectRegister(typeof(Engine.NetService), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Engine.NetService), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.NetService));
			
			
			Utils.EndClassRegister(typeof(Engine.NetService), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Engine.NetService __cl_gen_ret = new Engine.NetService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.NetService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Connect(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 7&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<Engine.ConnectCallback>(L, 4)&& translator.Assignable<Engine.ReceiveMsgCallback>(L, 5)&& translator.Assignable<Engine.DisconnectCallback>(L, 6)&& translator.Assignable<object>(L, 7)) 
                {
                    string strIP = LuaAPI.lua_tostring(L, 2);
                    int nPort = LuaAPI.xlua_tointeger(L, 3);
                    Engine.ConnectCallback callback = translator.GetDelegate<Engine.ConnectCallback>(L, 4);
                    Engine.ReceiveMsgCallback receiveCallback = translator.GetDelegate<Engine.ReceiveMsgCallback>(L, 5);
                    Engine.DisconnectCallback disconnectCallback = translator.GetDelegate<Engine.DisconnectCallback>(L, 6);
                    object owner = translator.GetObject(L, 7, typeof(object));
                    
                    __cl_gen_to_be_invoked.Connect( strIP, nPort, callback, receiveCallback, disconnectCallback, owner );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<Engine.ConnectCallback>(L, 4)&& translator.Assignable<Engine.ReceiveMsgCallback>(L, 5)&& translator.Assignable<Engine.DisconnectCallback>(L, 6)) 
                {
                    string strIP = LuaAPI.lua_tostring(L, 2);
                    int nPort = LuaAPI.xlua_tointeger(L, 3);
                    Engine.ConnectCallback callback = translator.GetDelegate<Engine.ConnectCallback>(L, 4);
                    Engine.ReceiveMsgCallback receiveCallback = translator.GetDelegate<Engine.ReceiveMsgCallback>(L, 5);
                    Engine.DisconnectCallback disconnectCallback = translator.GetDelegate<Engine.DisconnectCallback>(L, 6);
                    
                    __cl_gen_to_be_invoked.Connect( strIP, nPort, callback, receiveCallback, disconnectCallback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Engine.NetService.Connect!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckNetLink(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.CheckNetLink(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Send(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string strMsg = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.Send( strMsg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendToMe(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string strMsg = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.SendToMe( strMsg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
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
        static int _m_UnInitialize(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.UnInitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWithHttps(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string URL = LuaAPI.lua_tostring(L, 2);
                    string postString = LuaAPI.lua_tostring(L, 3);
                    Engine.SendHttpsCallback cb = translator.GetDelegate<Engine.SendHttpsCallback>(L, 4);
                    object[] extrans = translator.GetParams<object>(L, 5);
                    
                    __cl_gen_to_be_invoked.SendWithHttps( URL, postString, cb, extrans );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWithHttpImmeditor(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            Engine.NetService __cl_gen_to_be_invoked = (Engine.NetService)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string URL = LuaAPI.lua_tostring(L, 2);
                    string postString = LuaAPI.lua_tostring(L, 3);
                    Engine.SendHttpsCallback cb = translator.GetDelegate<Engine.SendHttpsCallback>(L, 4);
                    object[] extrans = translator.GetParams<object>(L, 5);
                    
                    __cl_gen_to_be_invoked.SendWithHttpImmeditor( URL, postString, cb, extrans );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
