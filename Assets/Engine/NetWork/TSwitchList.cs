using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    class TSwitchList<T>
    {
        private List<T> m_CommandList1 = new List<T>();		// 列表1
        private List<T> m_CommandList2 = new List<T>();		// 列表2

        public List<T> m_InList = null;		// 写入队列
        public List<T> m_OutList = null;		// 写出队列

        private object m_lock = new object();

        public TSwitchList()
        {
            m_InList = m_CommandList1;
            m_OutList = m_CommandList2;
        }

        // 添加命令
        public void Push(T cmd)
        {
            if( cmd == null )
            {
                return;
            }

            lock(m_lock)
            {
                if (m_InList != null)
                {
                    m_InList.Add(cmd);
                }
            }
        }

        // 队列反转
        public void Switch()
        {
            lock (m_lock)
            {
                if( m_OutList != null )
                {
                    if (m_OutList.Count > 0)
                    {
                        return;
                    }

                    // 队列交换
                    List<T> tmpList = m_InList;
                    m_InList = m_OutList;
                    m_OutList = tmpList;
                }
            }
        }

    }
}
