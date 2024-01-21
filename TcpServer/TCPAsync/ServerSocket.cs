using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPAsync
{
    class ServerSocket
    {
        //服务端socket
        public Socket socket;
        //储存连接过来的所有客户端套接字
        public Dictionary<int, ClientSocket> clientDic = new Dictionary<int, ClientSocket>();

        public void Start(string ip, int port, int num)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(num);
                socket.BeginAccept(AcceptCallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("启动服务器失败" + e.Message);
            }
        }
        private void AcceptCallBack(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = socket.EndAccept(result);
                ClientSocket client = new ClientSocket(clientSocket);
                clientDic.Add(client.clientID, client);
                socket.BeginAccept(AcceptCallBack, null);

            }
            catch (Exception e)
            {
                Console.WriteLine("客户端连入失败" + e.Message);
            }
        }

        public void BroadCast(string str)
        {
            foreach (ClientSocket client in clientDic.Values)
            {
                client.Send(str);
            }
        }
    }
}
