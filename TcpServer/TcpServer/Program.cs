using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务端基本逻辑 
            //1.创建套接字Socket(tcp)
            Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2.用Bind方法将套接字与本地地址（服务器）地址绑定
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
                socketTcp.Bind(ipPoint);
            }catch(Exception e)
            {
                Console.WriteLine("bind Error" + e.Message);
                return;
            }
            //3.listen监听
            socketTcp.Listen(1024);//max 1024台设备连接
            Console.WriteLine("wait client connect");
            //4.用等待客户端连接，连接成功时Accrpt返回新的套接字
            Socket socketClient = socketTcp.Accept();
            Console.WriteLine("have a client connect!");
            //5.用Send和Receive接收和发送数据
            PlayerMsg msg = new PlayerMsg();
            msg.playerID = 666;
            msg.playerData = new PlayerData();
            msg.playerData.name = "小飞棍来喽";
            msg.playerData.atk = 99;
            msg.playerData.lev = 50;

            socketClient.Send(msg.Writing());
            //接受
            byte[] result = new byte[1024];
            int resultNum =  socketClient.Receive(result);//接受长度
            Console.WriteLine("接收到了{0}发来的消息：{1}", socketClient.RemoteEndPoint.ToString(),
               Encoding.UTF8.GetString(result, 0, resultNum));
            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
            
            
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
