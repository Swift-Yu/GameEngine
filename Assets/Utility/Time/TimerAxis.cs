using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utility
{
    public interface ITimer
    {
        // 定时器回调
        void OnTimer(uint uTimerID);
    }

    public class TimerAxis
    {
        static TimerAxis s_Inst = null;
        public static TimerAxis Instance()
        {
            if (null == s_Inst)
            {
                s_Inst = new TimerAxis();
            }

            return s_Inst;
        }

        public const uint INFINITY_CALL = 0xffffffff;

        private const int CHECK_FREQUENCY = 16;	//精确到16ms timer的最小精度
        private const int TIME_GRID = 64;       //64毫秒     时间刻度单位
        private const int TIME_AXIS_LENGTH = 720000; // 时间轴长度 720秒时间长度
        private const int TIME_AXIS_INIT_SIZE = 12;       // 时间轴上初始定时器数量

        private const uint INVALID_TIMER = 0xffffffff;

        private uint m_dwLastCheckTick;           // 最后一次检查的时间
        private uint m_dwInitializeTime;          // 时间轴初始时间

        class TimerInfo
	    {
		    public uint     dwTimerID;      // 定时器ID
            public uint     dwInterval;     // 定时器频率
            public uint     dwCallTimes;    // 总共需要回调多少次
            public uint     dwLastCallTick; // 最后一次调用的时间
            public uint     dwGridIndex;    // 所在的时间刻度
            public int      timeAixsPos;    // 在时间刻度列表中的位置
            public ITimer   handler;        // 定时器
            public string   debugInfo;      // 描述串
	    };

        Dictionary<ITimer, Dictionary<uint, TimerInfo>> m_Timers = new Dictionary<ITimer,Dictionary<uint,TimerInfo>>();   // 计时器信息

        ArrayList m_TimerAxis = null;

        public bool Create()
        {
            int nSize = ((TIME_AXIS_LENGTH + TIME_GRID - 1) / TIME_GRID);
            m_TimerAxis = new ArrayList(nSize);

            for (int i = 0; i < m_TimerAxis.Capacity; ++i)
            {
                m_TimerAxis.Add(new ArrayListEx());
            }

            m_dwInitializeTime = (uint)TimeHelper.GetTickCount();
            m_dwLastCheckTick = m_dwInitializeTime;

            Log.Trace("定时器创建成功!");

	        return true;
        }

        public void Close()
        {
            if(m_TimerAxis==null)
            {
                return;
            }

            for (int i = 0; i < m_TimerAxis.Count; ++i)
            {
                ArrayListEx timeList = m_TimerAxis[i] as ArrayListEx;
                int nSize = timeList.Count;
                for (int j = 0; j < nSize; ++j)
                {
                    TimerInfo time = timeList.Get(i) as TimerInfo;
                    if (time != null)
                    {
                        KillTimer(time.dwTimerID, time.handler);
                    }
                }

                timeList.Clear();
            }
            m_TimerAxis.Clear();
            m_TimerAxis = null;
        }

        /**
	    @purpose          : 设置一个定时器
	    @param	 timerID  : 定时器ID
	    @param   interval : 定时器调用间隔 单位是毫秒
	    @param   handler  : 处理接口
	    @param   callTimes: 调用次数,默认调用无穷次
	    @param   debugInfo: 调试信息
	    @return		      : 如果设置成功则返回true
	    */
	    public bool SetTimer(uint timerID,uint interval,ITimer handler,uint callTimes=INFINITY_CALL,string debugInfo = "" )
        {
            if( handler == null )
            {
                return false;
            }

            if (interval == 0 )
            {
                interval = 1;
            }

            Dictionary<uint, TimerInfo> timer = null;
            if(!m_Timers.TryGetValue(handler,out timer))
            {
                timer = new Dictionary<uint, TimerInfo>();
                m_Timers[handler] = timer;
            }

            if (timer == null)
            {
                return false;
            }

            TimerInfo info = null;
            if( timer.TryGetValue(timerID, out info) )
            {
                Log.Error("已经存在同样的TimerID:{0} {1}", timerID, debugInfo);
                return false;
            }

            info = new TimerInfo();
            info.handler = handler;
            info.dwInterval = interval;
            info.dwCallTimes = callTimes;
            info.dwTimerID = timerID;
            info.dwLastCallTick = m_dwLastCheckTick;
            info.debugInfo = debugInfo;

            info.dwGridIndex = (uint)(((info.dwLastCallTick + info.dwInterval - m_dwInitializeTime) / TIME_GRID) % m_TimerAxis.Count);
            timer[timerID] = info;

            ArrayListEx timeList = m_TimerAxis[(int)info.dwGridIndex] as ArrayListEx;
            if (timeList != null)
            {
                info.timeAixsPos = timeList.Add(info);
            }

            return true;
        }

	    /**
	    @purpose          : 删除定时器
	    @param	 timerID  : 定时器ID
	    @param   handler  : 处理接口
	    @return		      : 返回是否删除成功
	    */
        public bool KillTimer(uint timerID, ITimer handler)
        {
            if (handler == null)
            {
                return false;
            }

            Dictionary<uint, TimerInfo> timer = null;
            if (!m_Timers.TryGetValue(handler, out timer))
            {
                //Debuger.LogError("删除不存在的定时器");
                return false;
            }

            TimerInfo info = null;
            if (!timer.TryGetValue(timerID, out info))
            {
                //Debuger.LogError("删除不存在的定时器TimerID:{0}", timerID);
                return false;
            }

            if(m_TimerAxis==null)
            {
                return false;
            }

            ArrayListEx timeList = m_TimerAxis[(int)info.dwGridIndex] as ArrayListEx;
            if (timeList != null)
            {
                // 清空时间轴上的列表
                timeList.Remove(info.timeAixsPos);
            }
            timer.Remove(timerID);

            if( timer.Count <= 0 )
            {
                m_Timers.Remove(handler);
            }

            return true;
        }
        //-------------------------------------------------------------------------------------------------------
        public bool IsExist(uint timerID, ITimer handler)
        {
            if (handler == null)
            {
                return false;
            }

            Dictionary<uint, TimerInfo> timer = null;
            if (!m_Timers.TryGetValue(handler, out timer))
            {
                return false;
            }

            if(timer == null)
            {
                return false;
            }

            TimerInfo info = null;
            if (!timer.TryGetValue(timerID, out info))
            {
                return false;
            }

            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //private bool FindTimer(uint timerID, ITimer handler)
        //{
        //    Dictionary<uint, TimerInfo> timer = null;
        //    if (!m_Timers.TryGetValue(handler, out timer))
        //    {
        //        //Debuger.LogError("删除不存在的定时器");
        //        return false;
        //    }

        //    TimerInfo info = null;
        //    if (!timer.TryGetValue(timerID, out info))
        //    {
        //        //Debuger.LogError("删除不存在的定时器TimerID:{0}", timerID);
        //        return false;
        //    }

        //    return true;
        //}

        public void CheckTimer()
        {
            if( m_TimerAxis == null )
            {
                return;
            }

            uint now = (uint)TimeHelper.GetTickCount();
            if (now - m_dwLastCheckTick < CHECK_FREQUENCY)
            {
                return;
            }

            uint start_grid = (uint)(((m_dwLastCheckTick - m_dwInitializeTime) / TIME_GRID) % m_TimerAxis.Count);
            uint cur_grid = (uint)(((now - m_dwInitializeTime) / TIME_GRID) % m_TimerAxis.Count);
            m_dwLastCheckTick = now;

            uint i = start_grid;

	        // 遍历时间刻度
	        do
	        {
		        // 遍历当前时间刻度中的所有待触发定时器
                ArrayListEx timeList = m_TimerAxis[(int)i] as ArrayListEx;
                for( int t = 0; t < timeList.Count; ++t )
		        {
                    TimerInfo timer = timeList.Get(t) as TimerInfo;
                    if (timer == null)
                    {
                        continue;
                    }

			        // 触发定时器
			        if ( now - timer.dwLastCallTick >= timer.dwInterval )
			        {
				        //uint dwTick = (uint)TimeHelper.GetTickCount();

                        // 回调定时器
                        //Profiler.BeginSample("OnTimer");
                        try
                        {
                            timer.handler.OnTimer(timer.dwTimerID);
                        }
                        catch (System.Exception ex)
                        {
                            Utility.Log.Error(ex.ToString());
                            //continue;
                        }
				        
                        //Profiler.EndSample();
                        if (IsExist(timer.dwTimerID, timer.handler))
				        {
					        // 性能测试代码
                            //uint nCostTime = (uint)TimeHelper.GetTickCount() - dwTick;
                            //if(nCostTime > 64)
                            //{ 
                            //}

					        timer.dwLastCallTick = now;
                            if (timer.dwCallTimes != INFINITY_CALL)
                            {
                                timer.dwCallTimes -= 1;
                            }

					        if ( timer.dwCallTimes==0 )
					        {
						        // 调用次数已经够了
						        KillTimer(timer.dwTimerID,timer.handler);
					        }
					        else
					        {        
                                // 搬迁到下一次触发的位置
                                timer.dwGridIndex = (uint)(((timer.dwLastCallTick + timer.dwInterval - m_dwInitializeTime) / TIME_GRID) % m_TimerAxis.Count);
                                timeList.Remove(t);

                                ArrayListEx newTimeList = m_TimerAxis[(int)timer.dwGridIndex] as ArrayListEx;
                                timer.timeAixsPos = newTimeList.Add(timer);
					        }
				        }
                        else
                        {
                            //Engine.Utility.Log.Error("Remove Time {0} {1}", timer.dwTimerID, timer.handler.ToString());
                            timer = null;
                            timeList.Remove(t);
                        }
			        }
		        }

		        // 递进到下一个刻度
                if (i == cur_grid)
                {
                    break;
                }

                i = (uint)((i + 1) % m_TimerAxis.Count);
	        }while(i!=cur_grid);
        }
    }
}
