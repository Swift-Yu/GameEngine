using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ThreadTest {

	Func<int, string,int> m_func;

	int test (int i, string str){
		Debug.Log ("111111");
		return 1;
	}

	//socket
	void ServerSocket(){
		Socket socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPAddress ip = new IPAddress(new byte[]{192,168,1,1});
		IPEndPoint endpoint = new IPEndPoint(ip,8009);
		socket.Bind(endpoint);
		socket.Listen(100);

		Socket client = socket.Accept();
		string msg = "hello world";
		byte[] data =  UTF8Encoding.UTF8.GetBytes(msg);
		client.Send(data);
	}

	void ClientSocket(){
		Socket tcpClient = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPAddress ip = IPAddress.Parse("192.168.1.1");
		IPEndPoint endpoint = new IPEndPoint (ip, 8009);
		Byte[] buffer = new byte[1024];
		int length = tcpClient.Receive(buffer);
		Encoding.UTF8.GetString (buffer,0,length);
	}

	// thread
	void Start () {
		m_func = test;

		//委托发起线程，后两个为固定参数，第三个为callback,第四个用来给callback传递数据
		IAsyncResult ar = m_func.BeginInvoke(100,"1000",onCallback, m_func);

		//使用while等待
		while(ar.IsCompleted == false){
			Thread.Sleep (100);
		}
		int res = m_func.EndInvoke (ar);

		//1000ms waiting等待，当前主线程暂停
		bool isEnd = ar.AsyncWaitHandle.WaitOne(1000);

	}

	void onCallback(IAsyncResult ar){
		Func<int, string, int> a = ar.AsyncState as Func<int, string, int>;
		int ret = a.EndInvoke(ar);
	}

	//tcplistener
	void TcpListener(){
		TcpListener tcpListener = new TcpListener (IPAddress.Parse("192.168.1.1"),8009);
		tcpListener.Start();

		TcpClient client = tcpListener.AcceptTcpClient ();
		NetworkStream stream = client.GetStream ();
		Byte[] data = new byte[1024];
		int length = stream.Read (data, 0, 1024);

		string msg = Encoding.UTF8.GetString (data, 0, length);
		 
		stream.Close ();
		client.Close ();
		tcpListener.Stop ();
	}

	void TcpClient(){
		TcpClient client = new TcpClient ("192.168.1.1", 8009);
		NetworkStream stream = client.GetStream ();
		Byte[] data = new byte[1024];
		int length = stream.Read (data, 0, 1024);
		string msg = Encoding.UTF8.GetString (data, 0, length);
			
		stream.Write (data, 0, data.Length);
	}
}		
