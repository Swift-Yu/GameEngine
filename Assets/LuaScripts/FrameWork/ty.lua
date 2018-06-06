
-- ty = {}

-- -- require 所有框架下提供的接口

-- ty.Config = require('LuaScripts/FrameWork/Config/Config')

-- -- json插件
-- ty.rapidjson = require('rapidjson')
-- -- ty.Log系统
-- ty.Log = require('LuaScripts/FrameWork/utils/Log').New()

-- -- 全局事件引擎 用于派发事件跟派发网络消息
-- ty.GameEvent = require('LuaScripts/FrameWork/EventEngine/GameEvent')
-- ty.EventEngine = require('LuaScripts/FrameWork/EventEngine/EventEngine').New()

-- -- 定时器
-- ty.Timer = require('LuaScripts/FrameWork/utils/Timer').New()

-- -- 配置表管理
-- ty.TableManager = require('LuaScripts/FrameWork/LuaTable/LuaTable').New()

-- -- res
-- ty.ResManager = require('LuaScripts/FrameWork/ResManager/ResManager')
-- -- tcp网络
-- ty.NetCmd = require('LuaScripts/FrameWork/Net/NetCmd')
-- ty.ProtocolBase = require('LuaScripts/FrameWork/Net/ProtocolBase')
-- ty.NetWork = require('LuaScripts/FrameWork/Net/NetWork').New()
-- ty.Protocol = require('LuaScripts/FrameWork/Net/Protocol').New()
-- ty.Http = require('LuaScripts/FrameWork/Net/Http').New()
-- -- 音效
-- ty.AudioManager = require("LuaScripts/FrameWork/AudioManger/AudioManger").New()

-- --特效管理
-- ty.EffectManager = require('LuaScripts/FrameWork/EffectManager/EffectManager').New()
-- ty.EffectType = require('LuaScripts/FrameWork/EffectManager/EffectType')

-- -- 制作UI面板的基类 必须继承
-- ty.PanelBase = require('LuaScripts/FrameWork/UIManager/PanelBase')
-- ty.UIManager = require('LuaScripts/FrameWork/UIManager/UIManager').New()

-- -- 插件入口
-- ty.EntryBase = require('LuaScripts/FrameWork/PluginManager/EntryBase')
-- ty.PluginManager = require('LuaScripts/FrameWork/PluginManager/PluginManager').New()

-- -- 
-- ty.LuaCoroutine = require('LuaScripts/FrameWork/utils/LuaCoroutine')

-- ty.Helper = require('LuaScripts/FrameWork/Utils/Helper').New()
-- ty.Mathf = require('LuaScripts/FrameWork/Utils/Mathf').New()
-- ty.profiler = require('LuaScripts/FrameWork/Utils/profiler')

-- ty.AccountInfo = {userId = CS.Utils.GetPlayerPrefs("localID"),clientId = 'Android_5.1002_tyGuest,tyAccount,wechat.wechat,alipay,unionpay.0-hall9999.tuyoo.tu',intClientId = 20543}

-- --将table内容转换成字符串然后打印出来
-- --一般调用方式 ty.Inspect(table) 
-- ty.Inspect = require('LuaScripts/FrameWork/Utils/inspect')

-- ty.Init = function ()
-- 	ty.Log:Info('ty:Init :'..CS.UnityEngine.Time.realtimeSinceStartup)
-- 	local loadLuaFiles = ty.Config.runtime.misc.loadLuaFiles
-- 	if  loadLuaFiles and type(loadLuaFiles) == 'table'then
-- 		for k,v in pairs(loadLuaFiles) do
-- 			ty.LoadLuaFile(v)
-- 		end
-- 	end

-- 	ty.PluginManager:Init()
-- 	ty.Protocol:Register()
-- 	local entry = require(ty.Config.runtime.misc.startScript)	
-- 	entry:Init()
-- end

-- ty.LoadLuaFile = function( filePath )
-- 	CS.Utils.LoadLuaFile(filePath)
-- end



-- -- 设置ty的所有字段只读 赋值会出错
-- ty.__newindex = function (t, k, v) 
-- 	ty.Log:Error('Attempt to update a read-only table. key is '..k)
-- end
-- return ty