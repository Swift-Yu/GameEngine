using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

using Utility;
using System.Collections;
public class NetLinkSink : Engine.INetLinkSink
{
   // private readonly Queue<ProtoBuf.IExtensible> receivedQueueMessage = new Queue<ProtoBuf.IExtensible>();
    //标识重连是否走登录流程
    //bool isReConnectByLogin = false;
    public NetLinkSink()
    {

    }
    public enum NetState
    {
        NetState_Login = 1, // 登录
        NetState_Run,       // 运行
    }

    public NetState netState
    {
        get;
        set;
    }

    ConnectCallback m_connectCallback = null;
    public ConnectCallback connectCallback
    {
        set { m_connectCallback = value; }
    }

    public object CallbackOwner
    {
        get;
        set;
    }
    private ReceiveMsgCallback m_receiveMsgCallback = null;
    public ReceiveMsgCallback ReceiveMsgCallback
    {
        set { m_receiveMsgCallback = value; }
    }

    private DisconnectCallback m_disconnectCallback = null;
    public DisconnectCallback DisconnectCallback
    {
        set { m_disconnectCallback = value; }
    }

    uint msgIndex = 0;
    public uint GetLastMessageIndex()
    {
        return msgIndex;
    }

    // 连接断开
    public void OnDisConnect()
    {
        Engine.NetWork.Instance().Seed = 0;

        if (m_disconnectCallback != null)
        {
            m_disconnectCallback(CallbackOwner);
        }
    }

    // 网络关闭
    public void OnClose()
    {
        Utility.Log.Trace("网络关闭");
    }

    public void OnConnectError(NetWorkError e)
    {
        if (m_connectCallback != null)
        {
            m_connectCallback(e == NetWorkError.NetWorkError_ConnectSuccess);
        }
    }

    // 重新连接
    public void OnReConnected()
    {

    }

    public void SendToMe(string strMsg)
    {
        if (m_receiveMsgCallback != null)
        {
            m_receiveMsgCallback(CallbackOwner, strMsg);
        }
    }

    public void OnReceive(Engine.PackageIn msg)
    {
        if (m_receiveMsgCallback != null)
        {
#if PROFILER
            UnityEngine.Profiling.Profiler.BeginSample("--------------NetService->OnReceive");
#endif
            byte[] msgArray = PraseMsg(msg.ToArray());

            string strMsg = System.Text.Encoding.Default.GetString(msgArray);

            m_receiveMsgCallback(CallbackOwner,strMsg);
#if PROFILER
            UnityEngine.Profiling.Profiler.EndSample();
#endif
        }
        else
        {
            Utility.Log.Error("OnReceive callback null");
        }
    }

    byte[] PraseMsg(byte[] msgArray)
    {
#if FTCODE
    int ret = Utility.FTCode.ftcode(Engine.NetWork.Instance().Seed + msgArray.Length, 0, ref msgArray, msgArray.Length);
#else
        Utility.FTCode.xorCodeing((uint)(Engine.NetWork.Instance().Seed + msgArray.Length), ref msgArray);
#endif
        
        byte[] decompress = Common.ZlibCompress.DecompressBytes(msgArray);
        msgArray = null;
        return decompress;
    }
}
