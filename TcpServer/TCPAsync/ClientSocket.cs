using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPAsync
{
    class ClientSocket
    {
        public Socket socket;
        public int clientID;
        public static int CLIENT_BEGIN_ID = 1;
        private byte[] cacheBytes = new byte[1024];
        private int cacheNum = 0;
        public ClientSocket(Socket socket)
        {
            this.clientID = CLIENT_BEGIN_ID++;
            this.socket = socket;
            this.socket.BeginReceive(cacheBytes,cacheNum,cacheBytes.Length,SocketFlags.None, RecevieCallBack,null);
        }

        private void RecevieCallBack(IAsyncResult result)
        {

            try
            {
                cacheNum = this.socket.EndReceive(result);

                Console.WriteLine(Encoding.UTF8.GetString(cacheBytes, 0, cacheNum));

                cacheNum = 0;
                if (this.socket.Connected)
                {
                    this.socket.BeginReceive(cacheBytes, cacheNum, cacheBytes.Length, SocketFlags.None, RecevieCallBack, this.socket);
                }
                else
                {
                    Console.WriteLine("没有连接，不用再收消息了");
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("接受消息错误" + e.SocketErrorCode + e.Message);
            }
           
        }

        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="str"></param>
        public void Send(string str)
        {
            if (socket.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                this.socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallBack,null);
            }
        }

        private void SendCallBack(IAsyncResult result)
        {
            try
            {
                this.socket.EndSend(result);

            }
            catch (SocketException e)
            {
                Console.WriteLine("发送失败" + e);
            }
        }
    }
}
