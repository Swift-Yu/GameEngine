-- Tencent is pleased to support the open source community by making xLua available.
-- Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
-- Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
-- http://opensource.org/licenses/MIT
-- Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

local TYPE_NAME = {'GLOBAL', 'REGISTRY', 'UPVALUE', 'LOCAL'}

local log = false
local function report_output_line(rp)
    local size = string.format("%7i", rp.size)
    if rp.name == '_ENV' then
        return string.format("%-40.40s: %-12s: %-12s: %-12s: %s\n", rp.name, size, TYPE_NAME[rp.type], rp.pointer, '')
    else
        return string.format("%-40.40s: %-12s: %-12s: %-12s: %s\n", rp.name, size, TYPE_NAME[rp.type], rp.pointer, rp.used_in)
    end
end
-- Get the format string of date time.
local function FormatDateTimeNow()
    local cDateTime = os.date("*t")
    local strDateTime = string.format("%04d%02d%02d-%02d%02d%02d", tostring(cDateTime.year), tostring(cDateTime.month), tostring(cDateTime.day),
        tostring(cDateTime.hour), tostring(cDateTime.min), tostring(cDateTime.sec))
    return strDateTime
end
local function writeFile(strFileName,strContent)
    local cFile = assert(io.open(strFileName, "w"))

    cFile:write(strContent)
    io.close(cFile)
end

local function snapshot()
    local ss = perf.snapshot()
    
    local FORMAT_HEADER_LINE       = "%-40s: %-12s: %-12s: %-12s: %s\n"
    local header = string.format( FORMAT_HEADER_LINE, "NAME", "SIZE", "TYPE", "ID", "INFO")
    table.sort(ss, 
        function(a, b) 
            if a.size ~= b.size then
                return a.size > b.size 
            end
            return a.name < b.name
        end)
    
    local output = header
    
    local memCount = 0
    for i, rp in ipairs(ss) do
        output = output .. report_output_line(rp)
        memCount = memCount + rp.size
    end

    for k, n in pairs(_ENV) do 
        --output = output .. string.format("%-40.40s: %-12s: %-12s: %-12s: %s\n", rp.name, size, TYPE_NAME[rp.type], rp.pointer, rp.used_in)
    end

    writeFile('memory-'..FormatDateTimeNow()..'.txt',output)
    return 'memCount :'..memCount
    --return 'memCount :'..memCount.."\n"..output
end

 

--returns the total memory in use by Lua (in Kbytes).
local function total()
    return collectgarbage('count')
end


return {
    snapshot = snapshot,
    total = total
}
