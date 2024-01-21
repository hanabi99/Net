using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerMutiOOP
{
    class ServerSocket
    {
        //服务端socket
        public Socket socket;
        //储存连接过来的所有客户端套接字
        public Dictionary<int, ClientSocket> clientDic = new Dictionary<int, ClientSocket>();

        //储存的是待移除的客户端套接字
        private List<ClientSocket> delClientSockets = new List<ClientSocket>();

        public bool isClose;
        //开启服务器
        public void Start(string ip, int port, int num)
        {
            isClose = false;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket.Bind(iPEndPoint);
            socket.Listen(num);
            ThreadPool.QueueUserWorkItem(Accept);
            ThreadPool.QueueUserWorkItem(Receive);
        }
        public void Close()
        {
            isClose = true;
            foreach (ClientSocket client in clientDic.Values)
            {
                client.Close();
            }
            clientDic.Clear();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
        private void Accept(object obj)
        {
            while (!isClose)
            {
                try
                {
                    Socket cliientSocket = socket.Accept();
                    ClientSocket clientSocket = new ClientSocket(cliientSocket);
                    // clientSocket.Send("欢迎连入服务器");
                    lock (clientDic)
                    {
                        clientDic.Add(clientSocket.clientID, clientSocket);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e + "连接错误");
                }
            }
        }
        public void Receive(object obj)
        {
            while (!isClose)
            {
                if(clientDic.Count > 0)
                {
                    lock (clientDic)
                    {
                        foreach (ClientSocket client in clientDic.Values)
                        {
                            client.Recevie();
                        }
                        CloseDelListSockets();
                    }
                }
            }
        }

        public void CloseDelListSockets()
        {
            for (int i = 0; i < delClientSockets.Count; i++)
            {
                CloseClientSocket(delClientSockets[i]);
            }
            delClientSockets.Clear();
        }

        public void Broadcast(BaseMsg info)
        {
            lock (clientDic)
            {
                foreach (ClientSocket client in clientDic.Values)
                {
                    client.Send(info);
                }
            }
        }

        public void CloseClientSocket(ClientSocket socket)
        {
            lock (clientDic)
            {
                socket.Close();
                if (clientDic.ContainsKey(socket.clientID))
                {
                    clientDic.Remove(socket.clientID);
                    Console.WriteLine("客户端{0}主动断开连接了", socket.clientID);
                }
            }
        }

        public void AddDelSocket(ClientSocket socket)
        {
            if (!delClientSockets.Contains(socket))
            {
                delClientSockets.Add(socket);
            }
        }

    }
}
