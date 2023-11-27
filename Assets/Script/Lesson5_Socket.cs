using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class Lesson5_Socket : MonoBehaviour
{
    //AddressFamily addressFamily = AddressFamily.InterNetworkV6; IPV6寻址

    void Start()
    {
        // AddressFamily.InterNetwork ipv4寻址
        //TCP 流套接字
        Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //UDP数据报套接字
        Socket socketUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //1.套接字的连接状态
        if (socketTcp.Connected)
        {

        }
        //2.套接字的类型
        print(socketTcp.SocketType);
        //3.获取套接字的协议类型
        print(socketTcp.ProtocolType);
        //4.获取套接字的寻址方案
        print(socketTcp.AddressFamily);
        //5.从网络中获取准备读取的数据量
        print(socketTcp.Available);
        //6.获取本机ENDPOINT对象
        //socketTcp.LocalEndPoint as IPEndPoint
        //7.获取远程ENDpoint对象
        //socketTcp.RemoteEndPoint as IPEndPoint

        //Socket常用方法
        //主要用于服务端
        //绑定IP端口
        IPEndPoint ippoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        socketTcp.Bind(ippoint);
        //设置客户端连接的最大数量
        socketTcp.Listen(10);
        //等待客户端接入
        socketTcp.Accept();

        //主要用于客户端
        //连接远端服务器
        socketTcp.Connect(IPAddress.Parse("115.2.2.1"), 8080);
        //CS都会用到
        //接受和发送数据
        //释放连接并关闭
        socketTcp.Shutdown(SocketShutdown.Both);//Both代表同时停止接收和发送
        socketTcp.Close();


    }

    void Update()
    {
        
    }
}
