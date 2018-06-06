
local util = require 'LuaScripts/FrameWork/utils/util' 
--携程
local LuaCoroutine = {
    YieldReturn = util.async_to_sync(function (to_yield, cb,go)
        if go then
            local temp = go
            go = cb
            cb = temp
            CS.LuaCoroutine.YieldAndCallback(to_yield, cb,go)
        else
            CS.LuaCoroutine.YieldAndCallback(to_yield, cb,go)
        end
    end),
}

function TestCoroutine( ... )
    -- 不带gameobject
    local co = coroutine.create(function()
        yield_return = ty.LuaCoroutine.YieldReturn
        
        yield_return(CS.UnityEngine.WaitForSeconds(1.5))
        
        ty.Log:Error('UnityEngine.WaitForSeconds.......')
        yield_return(CS.UnityEngine.WaitForSeconds(0.5))

        ty.Log:Error('UnityEngine.WaitForSeconds(1.5')
        
    end)
    coroutine.resume(co) 
end

--TestCoroutine()
return LuaCoroutine