using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    // 状态接口
    public abstract class State
    {
        protected int m_nStateID = 0;

        // 进入状态
        public virtual void Enter(object param) { }
        
        // 退出状态
        public virtual void Leave() { }

        public virtual void Update(float dt) { }

        public virtual void OnEvent(int nEventID, object param) { }

        public virtual int GetStateID() { return m_nStateID; }
    }

    // 状态机
    public class StateMachine<T>
    {
        // 状态列表 不允许重复状态
        private Dictionary<int, State> m_dicState = new Dictionary<int,State>();

        private T m_Owner = default(T);  // 状态所有者

        // 当前状态
        private State m_curState = null;

        public StateMachine(T owner)
        {
            m_Owner = owner;
        }

        public void RegisterState(State s)
        {
            if (!m_dicState.ContainsKey(s.GetStateID()))
            {
                m_dicState.Add(s.GetStateID(), s);
            }
        }

        public void UnRegisterState(int nStateID)
        {
            if (!m_dicState.ContainsKey(nStateID))
            {
                m_dicState.Remove(nStateID);
            }
        }

        public void UnRegisterAllState()
        {
            m_dicState.Clear();
        }

        public T GetOwner() { return m_Owner; }

        public State GetCurState()
        {
            return m_curState;
        }

        public int GetCurStateID()
        {
            if( m_curState != null )
            {
                return m_curState.GetStateID();
            }

            return -1;
        }

        // 添加状态
        public void ChangeState(int nStateID, object param)
        {
            State tarState = null;
            if (m_dicState.TryGetValue(nStateID, out tarState))
            {
                if( m_curState != null )
                {
                    m_curState.Leave();
                }

                //State s;
                //m_dicState.TryGetValue(nStateID, out s);
                if (tarState != null)
                {
                    m_curState = tarState;
                    tarState.Enter(param);
                }
            }
            else
            {
                // 状态不支持
            }
        }

        public void Update(float dt)
        {
            if( m_curState != null )
            {
                m_curState.Update(dt);
            }
        }

        public void OnEvent(int nEventID, object param)
        {
            if (m_curState != null)
            {
                m_curState.OnEvent(nEventID, param);
            }
        }

    }
}
