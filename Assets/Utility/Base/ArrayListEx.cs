using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ArrayListEx 
    {
        private ArrayList m_Container = null;
        private List<int> m_lstIdlePos = new List<int>();

        public ArrayListEx()
        {
            m_Container = new ArrayList();
        }

        public ArrayListEx(int nCapacity)
        {
            m_Container = new ArrayList(nCapacity);
            for (int i = 0; i < m_Container.Capacity; ++i)
            {
                m_Container.Add(null);
                m_lstIdlePos.Add(i);
            }
        }

        // 添加元素
        public int Add(object value)
        {
            if( m_Container == null )
            {
                m_Container = new ArrayList();
            }

            if( m_lstIdlePos.Count <= 0 )
            {
                m_Container.Add(value);
                return m_Container.Count - 1;
            }

            int pos = m_lstIdlePos[0];
            m_Container[pos] = value;
            m_lstIdlePos.RemoveAt(0);
            return pos;
        }

        // 获取元素
        public object Get(int index)
        {
            if (index < 0 || index >= m_Container.Count)
            {
                return null;
            }
            return m_Container[index];
        }

        // 删除元素
        public void Remove(int index)
        {
            if (index < 0 || index >= m_Container.Count)
            {
                return;
            }

            m_Container[index] = null;
            if (!m_lstIdlePos.Contains(index))
            {
                m_lstIdlePos.Insert(0,index); // 添加在头部
            }
        }

        public void Clear()
        {
            if( m_Container != null )
            {
                m_Container.Clear();
            }

            if( m_lstIdlePos != null )
            {
                m_lstIdlePos.Clear();
            }

        }

        public int Count
        {
            get { return m_Container.Count; }
        }
    }
}
