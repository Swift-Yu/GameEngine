using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 投票返回值
    /// </summary>
    public interface IVoteReason
    {

        int ErrorID
        {
            get;
            set;
        }
    }
    /// <summary>
    ///全局事件引擎
    /// </summary>
    public class EventEngine
    {
        // 单例
        private static EventEngine _inst = null;
        public static EventEngine Instance()
        {
            if (_inst == null)
            {
                _inst = new EventEngine();
            }

            return _inst;
        }

        // 事件回调
        public delegate void EventCallback(int nEventID, object param);
        // 投票事件回调
        public delegate bool VoteCallback(int nEventID, object param);
        /// <summary>
        /// 投票事件回调
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public delegate IVoteReason VoteCallBackReturnReason(int nEventID, object param);
        // 事件列表
        private Dictionary<int, List<EventCallback>> m_EventList = new Dictionary<int, List<EventCallback>>();
        // 投票事件列表
        private Dictionary<int, List<VoteCallback>> m_VoteList = new Dictionary<int, List<VoteCallback>>();

        // 返回原因的投票事件列表
        private Dictionary<int, List<VoteCallBackReturnReason>> m_dicVote = new Dictionary<int, List<VoteCallBackReturnReason>>();
        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="callback"></param>
        public void AddEventListener(int nEventID, EventCallback callback)
        {
            if (m_EventList == null)
            {
                m_EventList = new Dictionary<int, List<EventCallback>>();
            }

            List<EventCallback> lstEvent = null;
            if (!m_EventList.TryGetValue(nEventID, out lstEvent))
            {
                lstEvent = new List<EventCallback>();
                m_EventList.Add(nEventID, lstEvent);
            }
            if (!lstEvent.Contains(callback))
            {
                lstEvent.Add(callback);
            }
        }

        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除事件ID 指定的回调
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="callback"></param>
        public void RemoveEventListener(int nEventID, EventCallback callback)
        {
            List<EventCallback> lstEvent = null;
            if (m_EventList.TryGetValue(nEventID, out lstEvent))
            {
                lstEvent.Remove(callback);
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// 删除事件ID的所有回调
        /// </summary>
        /// <param name="nEventID"></param>
        public void RemoveAllEventListener(int nEventID)
        {
            List<EventCallback> lstEvent = null;
            if (m_EventList.TryGetValue(nEventID, out lstEvent))
            {
                lstEvent.Clear();
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="param"></param>
        public void DispatchEvent(int nEventID, object param = null)
        {
            List<EventCallback> lstEvent = null;
            if (m_EventList.TryGetValue(nEventID, out lstEvent))
            {
                for (int i = 0; i < lstEvent.Count; ++i)
                {
                    if (lstEvent[i] != null)
                    {
                        try
                        {
                            lstEvent[i](nEventID, param);
                        }
                        catch (System.Exception ex)
                        {
                            Utility.Log.Error("Event Exception:{0}", ex.ToString());
                        }
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="callback"></param>
        public void AddVoteListener(int nEventID, VoteCallback callback)
        {
            if (m_VoteList == null)
            {
                m_VoteList = new Dictionary<int, List<VoteCallback>>();
            }

            List<VoteCallback> lstVote = null;
            if (!m_VoteList.TryGetValue(nEventID, out lstVote))
            {
                lstVote = new List<VoteCallback>();
                m_VoteList.Add(nEventID, lstVote);
            }

            if (!lstVote.Contains(callback))
            {
                lstVote.Add(callback);
            }

        }

        //-------------------------------------------------------------------------------------------------------
 
        public void RemoveVoteListener(int nEventID, VoteCallback callback)
        {
            List<VoteCallback> lstVote = null;
            if (m_VoteList.TryGetValue(nEventID, out lstVote))
            {
                lstVote.Remove(callback);
            }
        }

        //----------------------------------------------------------------------
        public void RemoveAllVoteListener(int nEventID)
        {
            List<VoteCallback> lstVote = null;
            if (m_VoteList.TryGetValue(nEventID, out lstVote))
            {
                lstVote.Clear();
            }
        }

        //----------------------------------------------------------------------
        public bool DispatchVote(int nEventID, object param = null)
        {
            List<VoteCallback> lstVote = null;
            if (m_VoteList.TryGetValue(nEventID, out lstVote))
            {
                for (int i = 0; i < lstVote.Count; ++i)
                {
                    if (lstVote[i] != null)
                    {
                        bool bRet = false;
                        try
                        {
                            bRet = lstVote[i](nEventID, param);
                        }
                        catch (System.Exception ex)
                        {
                            Utility.Log.Error("Event Exception:{0}", ex.ToString());
                        }

                        if (!bRet)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #region 继承IVoteReason的投票实现
        /// <summary>
        /// 注册返回原因的投票事件
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="callback"></param>
        public void AddVoteListenerReturnReason(int nEventID, VoteCallBackReturnReason callback)
        {
            if (m_dicVote == null)
            {
                m_dicVote = new Dictionary<int, List<VoteCallBackReturnReason>>();
            }

            List<VoteCallBackReturnReason> lstVote = null;
            if (!m_dicVote.TryGetValue(nEventID, out lstVote))
            {
                lstVote = new List<VoteCallBackReturnReason>();
                m_dicVote.Add(nEventID, lstVote);
            }

            if (!lstVote.Contains(callback))
            {
                lstVote.Add(callback);
            }

        }
        /// <summary>
        /// 移除返回原因的投票事件
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="callback"></param>
        public void RemoveVoteListenerReturnReason(int nEventID, VoteCallBackReturnReason callback)
        {
            List<VoteCallBackReturnReason> lstVote = null;
            if (m_dicVote.TryGetValue(nEventID, out lstVote))
            {
                lstVote.Remove(callback);
            }
        }
        /// <summary>
        /// 移除所有事件
        /// </summary>
        /// <param name="nEventID"></param>
        public void RemoveAllVoteListenerRetrurnReason(int nEventID)
        {
            List<VoteCallBackReturnReason> lstVote = null;
            if (m_dicVote.TryGetValue(nEventID, out lstVote))
            {
                lstVote.Clear();
            }
        }
        /// <summary>
        /// 派发返回原因的事件
        /// </summary>
        /// <param name="nEventID"></param>
        /// <param name="param"></param>
        /// <returns>0代表成功 其余代表失败</returns>
        public IVoteReason DispatchVoteReturnReason(int nEventID, object param = null)
        {
            List<VoteCallBackReturnReason> lstVote = null;
            IVoteReason vr = null;
            if (m_dicVote.TryGetValue(nEventID, out lstVote))
            {
                for (int i = 0; i < lstVote.Count; ++i)
                {
                    if (lstVote[i] != null)
                    {
                        vr = lstVote[i](nEventID, param);
                        if (vr != null)
                        {
                            if (vr.ErrorID != 0)
                            {
                                return vr;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return vr;
        }
        #endregion
    }
}
