
--定时器
local Timer                        =  class("Timer")

-- 
-- timerID 填大于0的整形 比如100， 不要带小数点
-- interval 毫秒为单位
-- handler 处理对象table table实现OnTimer 方法
-- return bool
function Timer:SetTimer(timerID,interval,handler,callTimes)
  handler.code = timerID..tostring(handler)

  if callTimes and type(callTimes) == 'number' then
	   return CS.LuaTimer.SetTimer(handler,timerID,interval,callTimes)
  else
     return CS.LuaTimer.SetTimer(handler,timerID,interval)
  end
end

--return bool
function Timer:KillTimer(timerID,handler)
  handler.code = timerID..tostring(handler)
	return CS.LuaTimer.KillTimer(timerID,handler)
end

--exmaple
function Timer:TestTimer()
   self.time = 5
   self:SetTimer(1023,1000,self,self.time)
end

--callback 方法名必须为‘OnTimer(timerId)’
--self 当前table对象
--timerId SetTimer的timerID
function Timer:OnTimer(timerId)
    
    self.time = self.time - 1
    ty.Log.Info('Timer:OnTimer '..timerId..'  '..self.time)
    self:KillTimer(timerId,self)
end

--Timer:TestTimer()

--exmaple

return Timer