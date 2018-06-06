
_MG = {}
setmetatable(_MG, {__mode = 'k'})

function CheckMahjongRef()
	
	print('CheckMahjongRef')

	for k,v in pairs(_MG) do
		print(k,v)
	end
end

function GetMem( ... )
	return collectgarbage('count')
end

function Snapshot( ... )
	--ty.EventEngine:Dump()
	CheckMahjongRef()
	 local memory = require('LuaScripts/FrameWork/Utils/memory')
	 local total = memory.total()
	 memory.snapshot()
	ty.Log:Info('total memory:'..total)
	-- for k, n in pairs(_ENV) do print(k,n) end
	--CS.Utils.WriteLog(total..'\n'..memory.snapshot())	
end

function MahjongPai(  )
	local str = ''
	if Mahjong then
		if Mahjong.GameManager then
			if Mahjong.GameManager.playerViews then
				for i=1,4 do
					local p = Mahjong.GameManager.playerViews[i]
					if p and p.paiWallView then

						local num,realNum = p.paiWallView:GetMahjongNum()
						str = str..num..','..realNum..'_'
						num,realNum = p.handPaiView:GetMahjongNum()
						str = str..num..','..realNum..'_'
						num,realNum = p.paiPoolView:GetMahjongNum()
						str = str..num..','..realNum..'_'
						num,realNum = p.vicePaiView:GetMahjongNum()
						str = str..num..','..realNum..'_'
					end
					str = str..';'
				end
			end

		end
	end
	return str
end
