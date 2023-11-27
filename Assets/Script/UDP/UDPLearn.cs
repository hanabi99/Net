using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class UDPLearn : MonoBehaviour
{
    private void Start()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //与本地地址进行连接（绑定）
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8080);
        socket.Bind(iPEndPoint);
        //发送指定目标
        IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
        //发送消息
        socket.SendTo(Encoding.UTF8.GetBytes("thcsasdasdasdasdasd"), remoteIpPoint);
        //接收消息
        byte[] bytes = new byte[512];
        EndPoint remoteIpPoint2 = new IPEndPoint(IPAddress.Any, 0);
        int length = socket.ReceiveFrom(bytes,ref remoteIpPoint2);//记录谁发给我的
        print((remoteIpPoint2 as IPEndPoint).Address.ToString() + "发了" + Encoding.UTF8.GetString(bytes, 0, length));

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
