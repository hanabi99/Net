using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TcpServerMuti
{
    class SocketMuti
    {
        static Socket socket;
        static List<Socket> clientSockets = new List<Socket>();
        public  static bool isClose = false;
        static void Main(string[] args)//多个客户端连接 多个客户端收发消息
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            socket.Bind(iPEndPoint);
            socket.Listen(1024);
            //等到客户端的链接
            Thread acceptThread = new Thread(ClientConnectAccept);
            acceptThread.Start();
            //收发消息
            Thread receiveThread = new Thread(ReceiveMsg);
            receiveThread.Start();
            //关闭
            while (true)
            {
                string input = Console.ReadLine();
                if(input == "Quit")
                {
                    isClose = true;
                    for (int i = 0; i < clientSockets.Count; i++)
                    {
                        clientSockets[i].Shutdown(SocketShutdown.Both);
                        clientSockets[i].Close();
                    }
                    clientSockets.Clear();
                    break;
                }else if(input.Substring(0,2) == "B:")
                {
                    for (int i = 0; i < clientSockets.Count; i++)
                    {
                        clientSockets[i].Send(Encoding.UTF8.GetBytes(input.Substring(2)));
                    }
                }
            }
        }

        static void ClientConnectAccept()
        {
            while (!isClose)
            {
                Socket clientSocket = socket.Accept();
                clientSockets.Add(clientSocket);
                clientSocket.Send(Encoding.UTF8.GetBytes("欢迎连入服务器"));
            }
        }

        static void ReceiveMsg()
        {
            Socket clientSocet;
            byte[] bytes = new byte[1024 * 1024];
            int receiveNum;
            int i;
            while (true)
            {
                for (i = 0; i < clientSockets.Count; i++)
                {
                    clientSocet = clientSockets[i];
                    if (clientSocet.Available > 0)//接受字节数大于零
                    {
                        receiveNum = clientSocet.Receive(bytes);
                        //如果在这处理消息会出现阻塞所以用threadpool
                        ThreadPool.QueueUserWorkItem(HandleMsg, (clientSocet, Encoding.UTF8.GetString(bytes, 0, receiveNum)));
                    }
                }
            }
        }
        static void HandleMsg(object obj)
        {
            (Socket s, string str) info = ((Socket s, string str))obj;
            Console.WriteLine("接收到了{0}发来的消息：{1}", info.s.RemoteEndPoint.ToString(), info.str);
        }
    }
}
