--日志系统
local Log                        =  class("Log")

function Log:LogGroup(strGroup,strMsg)
   CS.Utility.Log.LogGroup(strGroup,self:GetStackMsg(strMsg));
end

function Log:Info(str)
	 CS.Utility.Log.Info('LUA:'.. self:GetStackMsg(str));
end

function Log:Warning(str)
   CS.Utility.Log.Warning('LUA:'.. self:GetStackMsg(str)); 
end

function Log:Error(str)
   CS.Utility.Log.Error('LUA:'.. self:GetStackMsg(str)); 
end

function Log:Trace(str)
   CS.Utility.Log.Trace('LUA:'.. self:GetStackMsg(str)); 
end

function Log:GetStackMsg(str)
    local startLevel = 3
    local maxLevel = 20
 
    local logString = str..'\n'
    for level = startLevel, maxLevel do
        -- 打印堆栈每一层
        local info = debug.getinfo( level, "nSl") 
        if info == nil then break end
        logString = logString..string.format("at %s %s[line:%d]\n", info.source or "", info.name or "", info.currentline )
    end
    return logString 
end

function Log:OpenLog(level,writeLog)
    CS.Utility.Log.OpenLog(level,writeLog)
end

Log:OpenLog(4,true)


Log:Info('Load Log File--------')

return Log