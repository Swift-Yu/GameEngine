using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utility;
namespace Engine
{
    // 任务状态
    public enum TaskState
    {
        TaskState_Waiting = 0,
        TaskState_Execute,
        TaskState_End,
    }

    // 任务优先级
    public enum TaskPriority
    {
        TaskPriority_Low = 0,
        TaskPriority_Normal,
        TaskPriority_Hight,
        TaskPriority_Immediate,
    }

    public interface ITask
    {
        // 任务执行
        void Execute();

        // 任务完成后处理
        void EndDo();

        // 获取任务优先级
        TaskPriority GetPriority();

        // 获取任务状态
        TaskState GetState();

        // 获取任务状态
        void SetState(TaskState state);
    }

    public class BaseTask : ITask
    {
        private TaskState m_State = TaskState.TaskState_Waiting;
        private TaskPriority m_Priority = TaskPriority.TaskPriority_Normal;
        private int m_nTaskID = 0;

        public BaseTask(int nTaskID, TaskPriority ePriority = TaskPriority.TaskPriority_Normal)
        {
            m_nTaskID = nTaskID;
            m_Priority = ePriority;
        }

        public virtual void Execute() { }

        // 任务完成后处理
        public virtual void EndDo() { }

        // 获取任务优先级
        public void SetPriority(TaskPriority ePriority) { m_Priority = ePriority; }

        // 获取任务优先级
        public TaskPriority GetPriority() { return m_Priority; }

        // 获取任务状态
        public TaskState GetState() { return m_State; }

        // 获取任务状态
        public void SetState(TaskState state) { m_State = state; }
    }

    // 后台任务管理器
    class DaemonManager
    {
        private static DaemonManager _inst = null;

        // 单次投递给线程的最大任务数
        private const int ThreadTaskMax = 5;
        // 单次处理已经完成的最大任务数
        private const int ProcessTaskMax = 2;

        public static DaemonManager Instance()
        {
            if (_inst == null)
            {
                _inst = new DaemonManager();
            }

            return _inst;
        }

        // 后台线程
        private Thread m_DaemonTaskThread = null;

        // 等待处理任务列表
        private List<ITask> m_WaitTask = new List<ITask>();

        // 正在执行任务列表
        private List<ITask> m_ExecuteTask = new List<ITask>();

        // 任务执行完成
        private List<ITask> m_EndTask = new List<ITask>();

        // 线程事件
        private AutoResetEvent m_ThreadEvent = null;
        // 线程处理完毕标识
        private bool m_nThreadWorking = true;

        // 初始化
        public void Init()
        {
            m_ThreadEvent = new AutoResetEvent(false);
            m_nThreadWorking = false;

            m_DaemonTaskThread = new Thread(ThreadProc);
            if (m_DaemonTaskThread != null)
            {
                m_DaemonTaskThread.Start(this);
            }
        }

        public void Release()
        {
            m_ThreadEvent = null;
            m_nThreadWorking = false;

            if (m_DaemonTaskThread != null)
            {
                m_DaemonTaskThread.Abort();
                m_DaemonTaskThread = null;
            }

            if (m_WaitTask != null)
            {
                m_WaitTask.Clear();
                m_WaitTask = null;
            }

            if (m_ExecuteTask != null)
            {
                m_ExecuteTask.Clear();
                m_ExecuteTask = null;
            }

            if (m_EndTask != null)
            {
                m_EndTask.Clear();
                m_EndTask = null;
            }
        }

        public void AddTask(ITask task)
        {
            if (task == null)
            {
                return;
            }

            if (task.GetPriority() == TaskPriority.TaskPriority_Immediate)
            {
                task.Execute();
                task.EndDo();
            }
            else
            {
                m_WaitTask.Add(task);
            }
        }

        public void Run()
        {
            //Profiler.BeginSample("DaemonManager posttask");
            if (!m_nThreadWorking && m_WaitTask.Count > 0)
            {
                int nWorkNum = 0;
                for (int i = 0; i < m_WaitTask.Count; i++)
                {
                    if (nWorkNum < ThreadTaskMax)
                    {
                        m_ExecuteTask.Add(m_WaitTask[i]);
                        m_WaitTask[i] = null;
                        m_WaitTask.RemoveAt(i--);
                        nWorkNum++;
                    }
                    else
                    {
                        break;
                    }
                }

                m_nThreadWorking = true;
                // 通知线程开始工作
                m_ThreadEvent.Set();
            }
            //Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("DaemonManager EndDo");
            // 处理执行完成任务
            lock (m_EndTask)
            {
                int nProcessNum = 0;
                for (int i = 0; i < m_EndTask.Count; ++i)
                {
                    if (nProcessNum < ProcessTaskMax)
                    {
                        try
                        {
                            m_EndTask[i].EndDo();
                        }
                        catch (SystemException e)
                        {
                            Log.Error("资源{0}加载回调出错：{1}", ((IResource)m_EndTask[i]).m_strResName, e.ToString());
                            m_EndTask[i] = null;
                            m_EndTask.RemoveAt(i--);
                            nProcessNum++;
                            continue;
                        }

                        m_EndTask[i] = null;
                        m_EndTask.RemoveAt(i--);
                        nProcessNum++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        /// //////////////////////////////////////////////////////////////
        public static void ThreadProc(object obj)
        {
            DaemonManager daemonTask = (DaemonManager)obj;
            if (daemonTask != null)
            {
                daemonTask.DaemonProc();
            }
        }

        private void DaemonProc()
        {
            while (true)
            {
                if (m_ThreadEvent != null)
                {
                    m_ThreadEvent.WaitOne();

                    for (int i = 0; i < m_ExecuteTask.Count; ++i)
                    {
                        if (m_ExecuteTask[i].GetState() == TaskState.TaskState_Waiting)
                        {
                            m_ExecuteTask[i].Execute();
                            m_ExecuteTask[i].SetState(TaskState.TaskState_Execute);
                        }
                    }

                    lock (m_EndTask)
                    {
                        for (int i = 0; i < m_ExecuteTask.Count; ++i)
                        {
                            m_EndTask.Add(m_ExecuteTask[i]);
                            m_ExecuteTask[i] = null;
                        }

                        m_ExecuteTask.Clear();

                    }

                    m_nThreadWorking = false;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
