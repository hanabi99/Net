using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class Client : MonoBehaviour
{
  
    void Start()
    {
        //1.创建套接字
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //2.确定服务端的端口 注意地址要填服务器地址 由于自己练习是本机
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        try
        {
            socket.Connect(iPEndPoint);
        }
        catch(SocketException e)
        {
            if(e.ErrorCode == 10061)
            {
                print("server拒绝连接");
            }
            else
            {
                print("连接服务器失败" + e.ErrorCode);
            }
            return;
        }
        //3.用send和receive收发数据
        byte[] recevieBytes = new byte[1024];
        int recevieBytesNum = socket.Receive(recevieBytes);

        //解析消息id
        int msgID = BitConverter.ToInt32(recevieBytes, 0);
        switch (msgID)
        {
            default:
                PlayerMsg msg = new PlayerMsg();
                msg.Reading(recevieBytes, 4);
                print(msg.playerID);
                print(msg.playerData.name);
                print(msg.playerData.atk);
                print(msg.playerData.lev);
                break;
        }


        print("收到服务器的数据" + Encoding.UTF8.GetString(recevieBytes, 0, recevieBytesNum));

        socket.Send(Encoding.UTF8.GetBytes("我是THC的客户端"));
        //4.用shutdown释放连接
        socket.Shutdown(SocketShutdown.Both);
        //5.关闭套接字
        socket.Close();
    }


}
