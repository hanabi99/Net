using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServerEX
{
    class Program
    {
        public static ServerSocket serverSocket;
        static void Main(string[] args)
        {

            serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1", 8080);
            Console.WriteLine("服务器开启成功");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Quit")
                {
                    serverSocket.Close();
                }
                else if (input.Substring(0, 2) == "B:")
                {
                    if (input.Substring(2) == "1001")
                    {
                        PlayerMsg ms = new PlayerMsg();
                        ms.playerID = 9999;
                        ms.playerData = new PlayerData();
                        ms.playerData.name = "aaaaaaaaaaa";
                        ms.playerData.atk = 99;
                        ms.playerData.lev = 90;
                        serverSocket.BroadCast(ms);
                    }
                }
            }
        }
    }
}

