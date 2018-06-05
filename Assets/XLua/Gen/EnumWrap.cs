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
    
    public class DGTweeningAutoPlayWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.AutoPlay), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.AutoPlay), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.AutoPlay), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.AutoPlay.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoPlaySequences", DG.Tweening.AutoPlay.AutoPlaySequences);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoPlayTweeners", DG.Tweening.AutoPlay.AutoPlayTweeners);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", DG.Tweening.AutoPlay.All);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.AutoPlay));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.AutoPlay), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningAutoPlay(L, (DG.Tweening.AutoPlay)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoPlaySequences"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.AutoPlaySequences);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoPlayTweeners"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.AutoPlayTweeners);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.All);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.AutoPlay!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.AutoPlay! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningAxisConstraintWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.AxisConstraint), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.AxisConstraint), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.AxisConstraint), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.AxisConstraint.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "X", DG.Tweening.AxisConstraint.X);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Y", DG.Tweening.AxisConstraint.Y);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Z", DG.Tweening.AxisConstraint.Z);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "W", DG.Tweening.AxisConstraint.W);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.AxisConstraint));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.AxisConstraint), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningAxisConstraint(L, (DG.Tweening.AxisConstraint)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "X"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.X);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Y"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.Y);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Z"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.Z);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "W"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.W);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.AxisConstraint!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.AxisConstraint! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningEaseWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.Ease), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.Ease), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.Ease), L, null, 40, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unset", DG.Tweening.Ease.Unset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", DG.Tweening.Ease.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InSine", DG.Tweening.Ease.InSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutSine", DG.Tweening.Ease.OutSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutSine", DG.Tweening.Ease.InOutSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuad", DG.Tweening.Ease.InQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuad", DG.Tweening.Ease.OutQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuad", DG.Tweening.Ease.InOutQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InCubic", DG.Tweening.Ease.InCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutCubic", DG.Tweening.Ease.OutCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutCubic", DG.Tweening.Ease.InOutCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuart", DG.Tweening.Ease.InQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuart", DG.Tweening.Ease.OutQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuart", DG.Tweening.Ease.InOutQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuint", DG.Tweening.Ease.InQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuint", DG.Tweening.Ease.OutQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuint", DG.Tweening.Ease.InOutQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InExpo", DG.Tweening.Ease.InExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutExpo", DG.Tweening.Ease.OutExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutExpo", DG.Tweening.Ease.InOutExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InCirc", DG.Tweening.Ease.InCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutCirc", DG.Tweening.Ease.OutCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutCirc", DG.Tweening.Ease.InOutCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InElastic", DG.Tweening.Ease.InElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutElastic", DG.Tweening.Ease.OutElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutElastic", DG.Tweening.Ease.InOutElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InBack", DG.Tweening.Ease.InBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutBack", DG.Tweening.Ease.OutBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutBack", DG.Tweening.Ease.InOutBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InBounce", DG.Tweening.Ease.InBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutBounce", DG.Tweening.Ease.OutBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutBounce", DG.Tweening.Ease.InOutBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Flash", DG.Tweening.Ease.Flash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InFlash", DG.Tweening.Ease.InFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutFlash", DG.Tweening.Ease.OutFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutFlash", DG.Tweening.Ease.InOutFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "INTERNAL_Zero", DG.Tweening.Ease.INTERNAL_Zero);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "INTERNAL_Custom", DG.Tweening.Ease.INTERNAL_Custom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.Ease));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.Ease), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningEase(L, (DG.Tweening.Ease)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unset"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Unset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Flash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Flash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "INTERNAL_Zero"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.INTERNAL_Zero);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "INTERNAL_Custom"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.INTERNAL_Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.Ease!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.Ease! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningLogBehaviourWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.LogBehaviour), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.LogBehaviour), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.LogBehaviour), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", DG.Tweening.LogBehaviour.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Verbose", DG.Tweening.LogBehaviour.Verbose);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ErrorsOnly", DG.Tweening.LogBehaviour.ErrorsOnly);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.LogBehaviour));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.LogBehaviour), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningLogBehaviour(L, (DG.Tweening.LogBehaviour)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Verbose"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.Verbose);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ErrorsOnly"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.ErrorsOnly);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.LogBehaviour!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.LogBehaviour! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningLoopTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.LoopType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.LoopType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.LoopType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Restart", DG.Tweening.LoopType.Restart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Yoyo", DG.Tweening.LoopType.Yoyo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Incremental", DG.Tweening.LoopType.Incremental);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.LoopType));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.LoopType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningLoopType(L, (DG.Tweening.LoopType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Restart"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Restart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Yoyo"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Yoyo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Incremental"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Incremental);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.LoopType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.LoopType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningPathModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.PathMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.PathMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.PathMode), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ignore", DG.Tweening.PathMode.Ignore);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Full3D", DG.Tweening.PathMode.Full3D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopDown2D", DG.Tweening.PathMode.TopDown2D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sidescroller2D", DG.Tweening.PathMode.Sidescroller2D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.PathMode));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.PathMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningPathMode(L, (DG.Tweening.PathMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Ignore"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Ignore);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Full3D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Full3D);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopDown2D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.TopDown2D);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sidescroller2D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Sidescroller2D);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.PathMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.PathMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningPathTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.PathType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.PathType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.PathType), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", DG.Tweening.PathType.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CatmullRom", DG.Tweening.PathType.CatmullRom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.PathType));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.PathType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningPathType(L, (DG.Tweening.PathType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushDGTweeningPathType(L, DG.Tweening.PathType.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CatmullRom"))
                {
                    translator.PushDGTweeningPathType(L, DG.Tweening.PathType.CatmullRom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.PathType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.PathType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningRotateModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.RotateMode), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fast", DG.Tweening.RotateMode.Fast);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FastBeyond360", DG.Tweening.RotateMode.FastBeyond360);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WorldAxisAdd", DG.Tweening.RotateMode.WorldAxisAdd);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalAxisAdd", DG.Tweening.RotateMode.LocalAxisAdd);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.RotateMode));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.RotateMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningRotateMode(L, (DG.Tweening.RotateMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Fast"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.Fast);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FastBeyond360"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.FastBeyond360);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WorldAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.WorldAxisAdd);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.LocalAxisAdd);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.RotateMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.RotateMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningScrambleModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.ScrambleMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.ScrambleMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.ScrambleMode), L, null, 8, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.ScrambleMode.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", DG.Tweening.ScrambleMode.All);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Uppercase", DG.Tweening.ScrambleMode.Uppercase);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Lowercase", DG.Tweening.ScrambleMode.Lowercase);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Numerals", DG.Tweening.ScrambleMode.Numerals);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", DG.Tweening.ScrambleMode.Custom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.ScrambleMode));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.ScrambleMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningScrambleMode(L, (DG.Tweening.ScrambleMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.All);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Uppercase"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Uppercase);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Lowercase"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Lowercase);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Numerals"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Numerals);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.ScrambleMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.ScrambleMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningTweenTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.TweenType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.TweenType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.TweenType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tweener", DG.Tweening.TweenType.Tweener);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sequence", DG.Tweening.TweenType.Sequence);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Callback", DG.Tweening.TweenType.Callback);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.TweenType));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.TweenType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningTweenType(L, (DG.Tweening.TweenType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Tweener"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Tweener);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sequence"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Sequence);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Callback"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Callback);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.TweenType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.TweenType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningUpdateTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.UpdateType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.UpdateType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.UpdateType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", DG.Tweening.UpdateType.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Late", DG.Tweening.UpdateType.Late);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fixed", DG.Tweening.UpdateType.Fixed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(DG.Tweening.UpdateType));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.UpdateType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningUpdateType(L, (DG.Tweening.UpdateType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Late"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Late);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Fixed"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Fixed);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.UpdateType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.UpdateType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineTextAnchorWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.TextAnchor), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.TextAnchor), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.TextAnchor), L, null, 11, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperLeft", UnityEngine.TextAnchor.UpperLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperCenter", UnityEngine.TextAnchor.UpperCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperRight", UnityEngine.TextAnchor.UpperRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleLeft", UnityEngine.TextAnchor.MiddleLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleCenter", UnityEngine.TextAnchor.MiddleCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleRight", UnityEngine.TextAnchor.MiddleRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerLeft", UnityEngine.TextAnchor.LowerLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerCenter", UnityEngine.TextAnchor.LowerCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerRight", UnityEngine.TextAnchor.LowerRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UnityEngine.TextAnchor));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.TextAnchor), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineTextAnchor(L, (UnityEngine.TextAnchor)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "UpperLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpperCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpperRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerRight);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.TextAnchor!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.TextAnchor! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSpriteAlignmentWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.SpriteAlignment), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.SpriteAlignment), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.SpriteAlignment), L, null, 12, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Center", UnityEngine.SpriteAlignment.Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopLeft", UnityEngine.SpriteAlignment.TopLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopCenter", UnityEngine.SpriteAlignment.TopCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopRight", UnityEngine.SpriteAlignment.TopRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftCenter", UnityEngine.SpriteAlignment.LeftCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightCenter", UnityEngine.SpriteAlignment.RightCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomLeft", UnityEngine.SpriteAlignment.BottomLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomCenter", UnityEngine.SpriteAlignment.BottomCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomRight", UnityEngine.SpriteAlignment.BottomRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", UnityEngine.SpriteAlignment.Custom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UnityEngine.SpriteAlignment));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.SpriteAlignment), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSpriteAlignment(L, (UnityEngine.SpriteAlignment)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Center"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopLeft"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.TopLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopCenter"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.TopCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopRight"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.TopRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftCenter"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.LeftCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightCenter"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.RightCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomLeft"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.BottomLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomCenter"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.BottomCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomRight"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.BottomRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushUnityEngineSpriteAlignment(L, UnityEngine.SpriteAlignment.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.SpriteAlignment!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.SpriteAlignment! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EngineNetWorkErrorWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Engine.NetWorkError), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Engine.NetWorkError), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Engine.NetWorkError), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetWorkError_null", Engine.NetWorkError.NetWorkError_null);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetWorkError_UnConnect", Engine.NetWorkError.NetWorkError_UnConnect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetWorkError_ConnectSuccess", Engine.NetWorkError.NetWorkError_ConnectSuccess);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetWorkError_DisConnect", Engine.NetWorkError.NetWorkError_DisConnect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetWorkError_ConnectFailed", Engine.NetWorkError.NetWorkError_ConnectFailed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.NetWorkError));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Engine.NetWorkError), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEngineNetWorkError(L, (Engine.NetWorkError)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "NetWorkError_null"))
                {
                    translator.PushEngineNetWorkError(L, Engine.NetWorkError.NetWorkError_null);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NetWorkError_UnConnect"))
                {
                    translator.PushEngineNetWorkError(L, Engine.NetWorkError.NetWorkError_UnConnect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NetWorkError_ConnectSuccess"))
                {
                    translator.PushEngineNetWorkError(L, Engine.NetWorkError.NetWorkError_ConnectSuccess);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NetWorkError_DisConnect"))
                {
                    translator.PushEngineNetWorkError(L, Engine.NetWorkError.NetWorkError_DisConnect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NetWorkError_ConnectFailed"))
                {
                    translator.PushEngineNetWorkError(L, Engine.NetWorkError.NetWorkError_ConnectFailed);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Engine.NetWorkError!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Engine.NetWorkError! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EngineTaskPriorityWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Engine.TaskPriority), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Engine.TaskPriority), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Engine.TaskPriority), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskPriority_Low", Engine.TaskPriority.TaskPriority_Low);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskPriority_Normal", Engine.TaskPriority.TaskPriority_Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskPriority_Hight", Engine.TaskPriority.TaskPriority_Hight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskPriority_Immediate", Engine.TaskPriority.TaskPriority_Immediate);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Engine.TaskPriority));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Engine.TaskPriority), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEngineTaskPriority(L, (Engine.TaskPriority)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "TaskPriority_Low"))
                {
                    translator.PushEngineTaskPriority(L, Engine.TaskPriority.TaskPriority_Low);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TaskPriority_Normal"))
                {
                    translator.PushEngineTaskPriority(L, Engine.TaskPriority.TaskPriority_Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TaskPriority_Hight"))
                {
                    translator.PushEngineTaskPriority(L, Engine.TaskPriority.TaskPriority_Hight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TaskPriority_Immediate"))
                {
                    translator.PushEngineTaskPriority(L, Engine.TaskPriority.TaskPriority_Immediate);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Engine.TaskPriority!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Engine.TaskPriority! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSpaceWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Space), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Space), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Space), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "World", UnityEngine.Space.World);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Self", UnityEngine.Space.Self);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UnityEngine.Space));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Space), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSpace(L, (UnityEngine.Space)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "World"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.World);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Self"))
                {
                    translator.PushUnityEngineSpace(L, UnityEngine.Space.Self);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Space!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Space! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineShadowQualityWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.ShadowQuality), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.ShadowQuality), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.ShadowQuality), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Disable", UnityEngine.ShadowQuality.Disable);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HardOnly", UnityEngine.ShadowQuality.HardOnly);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", UnityEngine.ShadowQuality.All);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UnityEngine.ShadowQuality));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.ShadowQuality), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineShadowQuality(L, (UnityEngine.ShadowQuality)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Disable"))
                {
                    translator.PushUnityEngineShadowQuality(L, UnityEngine.ShadowQuality.Disable);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "HardOnly"))
                {
                    translator.PushUnityEngineShadowQuality(L, UnityEngine.ShadowQuality.HardOnly);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushUnityEngineShadowQuality(L, UnityEngine.ShadowQuality.All);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.ShadowQuality!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.ShadowQuality! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRenderTextureFormatWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RenderTextureFormat), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RenderTextureFormat), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RenderTextureFormat), L, null, 25, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGB32", UnityEngine.RenderTextureFormat.ARGB32);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Depth", UnityEngine.RenderTextureFormat.Depth);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGBHalf", UnityEngine.RenderTextureFormat.ARGBHalf);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Shadowmap", UnityEngine.RenderTextureFormat.Shadowmap);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RGB565", UnityEngine.RenderTextureFormat.RGB565);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGB4444", UnityEngine.RenderTextureFormat.ARGB4444);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGB1555", UnityEngine.RenderTextureFormat.ARGB1555);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", UnityEngine.RenderTextureFormat.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGB2101010", UnityEngine.RenderTextureFormat.ARGB2101010);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DefaultHDR", UnityEngine.RenderTextureFormat.DefaultHDR);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGB64", UnityEngine.RenderTextureFormat.ARGB64);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGBFloat", UnityEngine.RenderTextureFormat.ARGBFloat);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RGFloat", UnityEngine.RenderTextureFormat.RGFloat);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RGHalf", UnityEngine.RenderTextureFormat.RGHalf);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RFloat", UnityEngine.RenderTextureFormat.RFloat);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RHalf", UnityEngine.RenderTextureFormat.RHalf);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "R8", UnityEngine.RenderTextureFormat.R8);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ARGBInt", UnityEngine.RenderTextureFormat.ARGBInt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RGInt", UnityEngine.RenderTextureFormat.RGInt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RInt", UnityEngine.RenderTextureFormat.RInt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BGRA32", UnityEngine.RenderTextureFormat.BGRA32);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RGB111110Float", UnityEngine.RenderTextureFormat.RGB111110Float);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RG32", UnityEngine.RenderTextureFormat.RG32);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(UnityEngine.RenderTextureFormat));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RenderTextureFormat), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRenderTextureFormat(L, (UnityEngine.RenderTextureFormat)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "ARGB32"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGB32);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Depth"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.Depth);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGBHalf"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGBHalf);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Shadowmap"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.Shadowmap);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RGB565"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RGB565);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGB4444"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGB4444);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGB1555"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGB1555);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGB2101010"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGB2101010);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DefaultHDR"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.DefaultHDR);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGB64"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGB64);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGBFloat"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGBFloat);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RGFloat"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RGFloat);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RGHalf"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RGHalf);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RFloat"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RFloat);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RHalf"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RHalf);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "R8"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.R8);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ARGBInt"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.ARGBInt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RGInt"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RGInt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RInt"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RInt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BGRA32"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.BGRA32);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RGB111110Float"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RGB111110Float);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RG32"))
                {
                    translator.PushUnityEngineRenderTextureFormat(L, UnityEngine.RenderTextureFormat.RG32);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RenderTextureFormat!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RenderTextureFormat! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialTestEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.TestEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.TestEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.TestEnum), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", Tutorial.TestEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", Tutorial.TestEnum.E2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Tutorial.TestEnum));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.TestEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialTestEnum(L, (Tutorial.TestEnum)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.TestEnum!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.TestEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialDrivenClassTestEnumInnerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.DrivenClass.TestEnumInner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.DrivenClass.TestEnumInner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.DrivenClass.TestEnumInner), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E3", Tutorial.DrivenClass.TestEnumInner.E3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E4", Tutorial.DrivenClass.TestEnumInner.E4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Tutorial.DrivenClass.TestEnumInner));
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.DrivenClass.TestEnumInner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialDrivenClassTestEnumInner(L, (Tutorial.DrivenClass.TestEnumInner)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "E3"))
                {
                    translator.PushTutorialDrivenClassTestEnumInner(L, Tutorial.DrivenClass.TestEnumInner.E3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E4"))
                {
                    translator.PushTutorialDrivenClassTestEnumInner(L, Tutorial.DrivenClass.TestEnumInner.E4);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.DrivenClass.TestEnumInner!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.DrivenClass.TestEnumInner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}