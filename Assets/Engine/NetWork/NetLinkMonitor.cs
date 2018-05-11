using System;
using System.Collections.Generic;
using Utility;
using Engine;

/// <summary>
/// 网络流量监控
/// </summary>
class NetLinkMonitor : INetLinkMonitor
{
    bool m_bOpen;
    long m_ReceiveBytes;
    long m_SendBytes;

    public NetLinkMonitor()
    {
        m_bOpen = false;
        m_ReceiveBytes = 0;
        m_SendBytes = 0;
    }
    public bool IsOpen
    {
        get
        {
            return m_bOpen;
        }
        set
        {
            m_bOpen = value;
            if (m_bOpen == false)
            {
                m_ReceiveBytes = 0;
                m_SendBytes = 0;
            }
        }
    }

    public void OnReceive(PackageIn msg)
    {
        if (!m_bOpen)
        {
            return;
        }
        m_ReceiveBytes += msg.Length;
    }

    public void OnSend(PackageOut msg)
    {
        if (!m_bOpen)
        {
            return;
        }
        m_SendBytes += msg.Length;
    }

    public long GetTotalReceiveBytes()
    {
        return m_ReceiveBytes;
    }

    public long GetTotalSendBytes()
    {
        return m_SendBytes;
    }



}
