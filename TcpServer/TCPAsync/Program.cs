using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSocket serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1", 8080, 1024);
            Console.WriteLine("服务器开启成功");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Substring(0, 2) == "B:")
                {
                    serverSocket.BroadCast(input.Substring(2));
                }
            }

        }
    }
}
