using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPServerEX
{
    class ServerSocket
    {
        Socket socket;

        bool isClose;

        Dictionary<string, Client> dic = new Dictionary<string, Client>();

        public void Start(string ip, int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(iPEndPoint);
                ThreadPool.QueueUserWorkItem(ReceiveMsg);
                ThreadPool.QueueUserWorkItem(CheckTimeOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("UDP 开启出错");
            }
        }

        private void CheckTimeOut(object obj)
        {
            long nowTime = 0;
            List<string> delList = new List<string>();
            while (true)
            {
                //每30秒检测一次 移除长时间没有发消息的客户端
                Thread.Sleep(30000);
                nowTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                foreach (Client value in dic.Values)
                {
                    if(nowTime - value.fountTime > 10)
                    {
                        delList.Add(value.clientStrID);
                    }
                }
                for (int i = 0; i < delList.Count; i++)
                {
                    RemoveClient(delList[i]);
                }
                delList.Clear();
            }
        }

        private void ReceiveMsg(object obj)
        {
            byte[] bytes = new byte[512];
            //记录谁发的 记录客户端
            EndPoint ipPoint = new IPEndPoint(IPAddress.Any, 0);

            string strID; //存储EndPoint用
            string ip;
            int port;
            while (!isClose)
            {
                if (socket.Available > 0)
                {
                    lock (socket)
                    {
                        socket.ReceiveFrom(bytes, ref ipPoint);
                    }
                    //处理消息
                    ip = (ipPoint as IPEndPoint).Address.ToString();
                    port = (ipPoint as IPEndPoint).Port;
                    strID = ip + port;//拼接成唯一ID
                    if (dic.ContainsKey(strID))
                    {
                        dic[strID].HandleReceiveMsg(bytes);
                    }
                    else
                    {
                        dic.Add(strID, new Client(ip, port));
                        dic[strID].HandleReceiveMsg(bytes);
                    }
                }
            }
        }
        //指定发送那个目标
        private void SendTo(BaseMsg msg, IPEndPoint ippoint)
        {
            try
            {
                lock (socket)
                {
                    if (!isClose)
                    {
                        socket.SendTo(msg.Writing(), ippoint);
                    }
                }
            }
            catch (SocketException m)
            {
                Console.WriteLine(m);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }       
        }
        public void Close()
        {
            isClose = true;
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }

        public void BroadCast(BaseMsg msg)
        {
            foreach (Client c in dic.Values)
            {
                SendTo(msg, c.ipEndPoint);
            }
        }

        public void RemoveClient(string clientID)
        {
            if (dic.ContainsKey(clientID))
            {
                Console.WriteLine("客户端{0}被移除了",dic[clientID].ipEndPoint);
                dic.Remove(clientID);
            }
        }
    }
}


