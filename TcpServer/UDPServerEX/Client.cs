using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace UDPServerEX
{
    class Client
    {
        public IPEndPoint ipEndPoint;

        public string clientStrID;

        public long fountTime;

        public Client(string ip,int port)
        {
            clientStrID = ip + port.ToString();
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void HandleReceiveMsg(byte[] bytes)
        {
            //为了避免处理消息时 又接受到其他消息 覆盖数组
            byte[] cacheBytes = new byte[512];
            bytes.CopyTo(cacheBytes,0);
            fountTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            ThreadPool.QueueUserWorkItem(ReceiveMsg, cacheBytes);
        }

        public void ReceiveMsg(object obj)
        {
            try
            {
                byte[] bytes = obj as byte[];
                int nowIndex = 0;
                int msgId = BitConverter.ToInt32(bytes, nowIndex);
                nowIndex += 4;
                int msgLength = BitConverter.ToInt32(bytes, nowIndex);
                nowIndex += 4;

                switch (msgId)
                {
                    case 1001:
                        PlayerMsg playerMsg = new PlayerMsg();
                        playerMsg.Reading(bytes, nowIndex);
                        Console.WriteLine(playerMsg.playerID);
                        Console.WriteLine(playerMsg.playerData.lev);
                        Console.WriteLine(playerMsg.playerData.atk);
                        Console.WriteLine(playerMsg.playerData.name);
                        break;
                    case 1002:
                        QuitMsg quitMsg = new QuitMsg();
                        //处理退出消息
                        Program.serverSocket.RemoveClient(clientStrID);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
}
