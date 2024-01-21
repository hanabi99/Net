using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //与本地地址进行连接（绑定）
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
            socket.Bind(iPEndPoint);
            Console.WriteLine("服务器开启");
            //接受消息
            byte[] bytes = new byte[512];
            EndPoint remoteIpPoint2 = new IPEndPoint(IPAddress.Any, 0);
            int length = socket.ReceiveFrom(bytes, ref remoteIpPoint2);//记录谁发给我的
            Console.WriteLine((remoteIpPoint2 as IPEndPoint).Address.ToString() + "发了" + Encoding.UTF8.GetString(bytes, 0, length));

      
            //发送消息 我已经知道是谁发来的消息 所以我直接用上面的ENDPOINT 就行
            socket.SendTo(Encoding.UTF8.GetBytes("hhhhhhhhhhhh"), remoteIpPoint2);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Console.ReadLine();
        }
    }
}
