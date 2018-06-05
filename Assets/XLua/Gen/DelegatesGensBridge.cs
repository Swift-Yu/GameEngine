#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class DelegateBridge : DelegateBridgeBase
    {
		
		public void __Gen_Delegate_Imp0()
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                
                int __gen_error = LuaAPI.lua_pcall(L, 0, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp1(XLua.LuaTable p0, uint p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                LuaAPI.xlua_pushuint(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp2(XLua.LuaTable p0, UnityEngine.GameObject p1, UnityEngine.Vector2 p2)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.Push(L, p1);
                translator.PushUnityEngineVector2(L, p2);
                
                int __gen_error = LuaAPI.lua_pcall(L, 3, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp3(uint p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.xlua_pushuint(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp4(string p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.lua_pushstring(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp5(double p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.lua_pushnumber(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp6(bool p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.lua_pushboolean(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp7(object p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.PushAny(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp8(Engine.IGameObject p0, object p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.PushAny(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp9(object p0, string p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.PushAny(L, p0);
                LuaAPI.lua_pushstring(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp10(Engine.NetWorkError p0, string p1, object p2)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.PushEngineNetWorkError(L, p0);
                LuaAPI.lua_pushstring(L, p1);
                translator.PushAny(L, p2);
                
                int __gen_error = LuaAPI.lua_pcall(L, 3, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp11(float p0)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.lua_pushnumber(L, p0);
                
                int __gen_error = LuaAPI.lua_pcall(L, 1, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp12(Engine.IUIAtlas p0, object p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.PushAny(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp13(Engine.ITexture p0, object p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.PushAny(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp14(Engine.IUIFont p0, object p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.PushAny(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public void __Gen_Delegate_Imp15(Engine.ISprite p0, object p1)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                translator.Push(L, p0);
                translator.PushAny(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public int __Gen_Delegate_Imp16(int p0, string p1, out CSCallLua.DClass p2)
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                LuaAPI.xlua_pushinteger(L, p0);
                LuaAPI.lua_pushstring(L, p1);
                
                int __gen_error = LuaAPI.lua_pcall(L, 2, 2, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                p2 = (CSCallLua.DClass)translator.GetObject(L, err_func + 2, typeof(CSCallLua.DClass));
                
                int __gen_ret = LuaAPI.xlua_tointeger(L, err_func + 1);
                LuaAPI.lua_settop(L, err_func - 1);
                return  __gen_ret;
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
		public System.Action __Gen_Delegate_Imp17()
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                ObjectTranslator translator = luaEnv.translator;
                
                LuaAPI.lua_getref(L, luaReference);
                
                
                int __gen_error = LuaAPI.lua_pcall(L, 0, 1, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                System.Action __gen_ret = translator.GetDelegate<System.Action>(L, err_func + 1);
                LuaAPI.lua_settop(L, err_func - 1);
                return  __gen_ret;
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
        
		static DelegateBridge()
		{
		    Gen_Flag = true;
		}
		
		public override Delegate GetDelegateByType(Type type)
		{
		
		    if (type == typeof(System.Action))
			{
			    return new System.Action(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(UnityEngine.Events.UnityAction))
			{
			    return new UnityEngine.Events.UnityAction(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(DG.Tweening.TweenCallback))
			{
			    return new DG.Tweening.TweenCallback(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(System.Action<XLua.LuaTable, uint>))
			{
			    return new System.Action<XLua.LuaTable, uint>(__Gen_Delegate_Imp1);
			}
		
		    if (type == typeof(System.Action<XLua.LuaTable, UnityEngine.GameObject, UnityEngine.Vector2>))
			{
			    return new System.Action<XLua.LuaTable, UnityEngine.GameObject, UnityEngine.Vector2>(__Gen_Delegate_Imp2);
			}
		
		    if (type == typeof(System.Action<uint>))
			{
			    return new System.Action<uint>(__Gen_Delegate_Imp3);
			}
		
		    if (type == typeof(System.Action<string>))
			{
			    return new System.Action<string>(__Gen_Delegate_Imp4);
			}
		
		    if (type == typeof(System.Action<double>))
			{
			    return new System.Action<double>(__Gen_Delegate_Imp5);
			}
		
		    if (type == typeof(Engine.ConnectCallback))
			{
			    return new Engine.ConnectCallback(__Gen_Delegate_Imp6);
			}
		
		    if (type == typeof(Engine.DisconnectCallback))
			{
			    return new Engine.DisconnectCallback(__Gen_Delegate_Imp7);
			}
		
		    if (type == typeof(Engine.CreateGameObjectEvent))
			{
			    return new Engine.CreateGameObjectEvent(__Gen_Delegate_Imp8);
			}
		
		    if (type == typeof(Engine.ReceiveMsgCallback))
			{
			    return new Engine.ReceiveMsgCallback(__Gen_Delegate_Imp9);
			}
		
		    if (type == typeof(Engine.SendHttpsCallback))
			{
			    return new Engine.SendHttpsCallback(__Gen_Delegate_Imp10);
			}
		
		    if (type == typeof(DG.Tweening.Core.DOSetter<float>))
			{
			    return new DG.Tweening.Core.DOSetter<float>(__Gen_Delegate_Imp11);
			}
		
		    if (type == typeof(Engine.CreateAtlasEvent))
			{
			    return new Engine.CreateAtlasEvent(__Gen_Delegate_Imp12);
			}
		
		    if (type == typeof(Engine.CreateTextureEvent))
			{
			    return new Engine.CreateTextureEvent(__Gen_Delegate_Imp13);
			}
		
		    if (type == typeof(Engine.CreateFontEvent))
			{
			    return new Engine.CreateFontEvent(__Gen_Delegate_Imp14);
			}
		
		    if (type == typeof(Engine.CreateSpriteEvent))
			{
			    return new Engine.CreateSpriteEvent(__Gen_Delegate_Imp15);
			}
		
		    if (type == typeof(CSCallLua.FDelegate))
			{
			    return new CSCallLua.FDelegate(__Gen_Delegate_Imp16);
			}
		
		    if (type == typeof(CSCallLua.GetE))
			{
			    return new CSCallLua.GetE(__Gen_Delegate_Imp17);
			}
		
		    throw new InvalidCastException("This delegate must add to CSharpCallLua: " + type);
		}
	}
    
}