using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public delegate void ConnectCallback(bool ret);
    public delegate void DisconnectCallback(object obj);
    public delegate void ReceiveMsgCallback(object obj, string msg);
    // 网络连接回调
    public interface INetLinkSink
    {
        void OnConnectError(NetWorkError e);

        // 重新连接
        void OnReConnected();

        void OnReceive(Engine.PackageIn msg);

        // 连接断开
        void OnDisConnect();

        // 网络关闭
        void OnClose();

        ConnectCallback connectCallback
        {
            set;
        }

        DisconnectCallback DisconnectCallback
        {
            set;
        }
        ReceiveMsgCallback ReceiveMsgCallback
        {
            set;
        }

    }

    // 网络监控
    public interface INetLinkMonitor
    {
        bool IsOpen
        {
            get;
            set;
        }
        void OnReceive(Engine.PackageIn msg);

        void OnSend(Engine.PackageOut msg);

        long GetTotalReceiveBytes();

        long GetTotalSendBytes();

    }

    // 网络连接接口
    public interface INetLink
    {
        // 连接ID
        int GetLinkID();

        // 获取连接状态
        SCOKET_CONNECT_STATE GetState();

        void Connect(string strServerIP, int port);

        void Disconnect();

        bool IsConnected();

        // 发送消息
        void SendMsg(Engine.PackageOut pkg);

        void Destroy();

        //void ReSendPackOut();
    }
}
