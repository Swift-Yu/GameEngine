
-- class 方法
require('LuaScripts/FrameWork/utils/Class')
require('LuaScripts/FrameWork/utils/memoryRef')
require('LuaScripts/FrameWork/test')

function Main()
	--ty = require('LuaScripts/FrameWork/ty')
	--ty.Init()
end



-- state 1 进入 0 退出到后台
function OnApplicationFocus( state )
	if  CS.Utils.IsEditor() then
		return
	end
	
	ty.EventEngine:DispatchEvent(ty.GameEvent.APPLICATION_FOCUS,state == 1)
	if state == 1 then
		if ty.NetWork and ty.NetWork.isLogin and ty.PluginManager:IsEnterGame() then
			ty.NetWork:StartReConnect()
		end		
	end
end

function OnApplicationQuit( )
	ty.Log:Info('OnApplicationQuit--------')
	ty.EventEngine:DispatchEvent(ty.GameEvent.APPLICATION_QUIT)
end

function ExportTable( ... )
	local script = require('LuaScripts/FrameWork/utils/DataTableOptimizer')
	script.ExportDatabaseLocalText(true)
end

