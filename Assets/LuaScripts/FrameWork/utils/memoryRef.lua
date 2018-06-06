-- 内存检测
local mri = require("LuaScripts/FrameWork/utils/MemoryReferenceInfo")

-- Set config.
mri.m_cConfig.m_bAllMemoryRefFileAddTime = false

function DumpBefore(  )
	collectgarbage("collect")
	mri.m_cMethods.DumpMemorySnapshot("./", "1-Before", -1)
end

function DumpAfter(  )
	collectgarbage("collect")
	mri.m_cMethods.DumpMemorySnapshot("./", "2-After", -1)
end

function ComparedFile( ... )
	mri.m_cMethods.DumpMemorySnapshotComparedFile("./", "Compared", -1, "./LuaMemRefInfo-All-[1-Before].txt", "./LuaMemRefInfo-All-[2-After].txt")
end

function DumpSingleObject( ... )
	collectgarbage("collect")
	mri.m_cMethods.DumpMemorySnapshotSingleObject("./", "SingleObjRef-Object", -1, "Author",Mahjong.GameManager.playerViews[i].handPaiView)	
end