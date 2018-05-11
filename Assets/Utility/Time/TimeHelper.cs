using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utility
{
    // 时间方法集
    public class TimeHelper
    {
        // 返回自纪元开始到现在的毫秒数
        public static long GetTime()
        {
            // System.DateTime.UtcNow.Ticks
            // 类型：System.Int64
            // 一个日期和时间，以公历 0001年1月1日 00:00:00.000 以来所经历的以100 纳秒为间隔的间隔数。
            return (long)(System.DateTime.UtcNow.Ticks / 10000);
        }

        // 返回游戏启动以来的毫秒数
        public static long GetTickCount()
        {
            // System.DateTime.UtcNow.Ticks
            // 类型：System.Int64
            // 一个日期和时间，以公历 0001年1月1日 00:00:00.000 以来所经历的以100 纳秒为间隔的间隔数。
            return (long)(Time.realtimeSinceStartup*1000);
        }
    }
}
