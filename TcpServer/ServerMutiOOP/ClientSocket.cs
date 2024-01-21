using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace ServerMutiOOP
{
    //服务端接收的客户端socket
    class ClientSocket
    {
        private static int CLIENT_BEGIN_ID = 1;
        public int clientID;
        public Socket socket;

        //用于处理分包
        private byte[] cacheBytes = new byte[1024 * 1024];
        private int cacheNum = 0;
        public ClientSocket(Socket socket)
        {
            this.clientID = CLIENT_BEGIN_ID;
            this.socket = socket;
            ++CLIENT_BEGIN_ID;
            //ThreadPool.QueueUserWorkItem(CheckTimeOut);
        }

        public bool IsConnect => this.socket.Connected;

        //上一次收到的时间
        private long frontTime = -1;

        private static int TIME_OUT_TINE = 10;


        private void CheckTimeOut()
        {
            if (frontTime != -1 && DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime > TIME_OUT_TINE) //超过十秒没收到心跳消息 则断开连接 
            {
                ServerMutiOOP.serverSocket.AddDelSocket(this);
            }

        }


        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }

        /// <summary>
        /// 发字符串
        /// </summary>
        /// <param name="info"></param>
        public void Send(BaseMsg info)
        {
            if (IsConnect)
            {
                try
                {
                    socket.Send(info.Writing());
                }
                catch (Exception e)
                {
                    Console.WriteLine("send error" + e.Message);
                    ServerMutiOOP.serverSocket.AddDelSocket(this);
                }
            }
        }
        /// <summary>
        /// 接受
        /// </summary>
        public void Recevie()
        {
            if (!IsConnect)
            {
                ServerMutiOOP.serverSocket.AddDelSocket(this);
                return;
            }
            try
            {
                if (socket.Available > 0)
                {
                    byte[] bytes = new byte[1024 * 5];
                    int receiveNum = socket.Receive(bytes);
                    HandleReciveMsg(bytes, receiveNum);
                    //先序列化ID
                    //int msgID = BitConverter.ToInt32(bytes, 0);
                    //BaseMsg msg = null;
                    //switch (msgID)
                    //{
                    //    case 1001:
                    //        msg = new PlayerMsg();
                    //        msg.Reading(bytes, 4);
                    //        break;
                    //}
                    //if (msg == null)
                    //{
                    //    Console.WriteLine("msg is null");
                    //    return;
                    //}
                    //ThreadPool.QueueUserWorkItem(HandleMsg, msg);
                }
                //检测是否超时
                CheckTimeOut();
            }
            catch (Exception e)
            {
                Console.WriteLine("recevie error" + e.Message);
                ServerMutiOOP.serverSocket.AddDelSocket(this);
            }
        }
        /// <summary>
        /// 处理接收消息 分包粘包问题
        /// </summary>
        /// <param name="receiveBytes"></param>
        /// <param name="receiveNum"></param>
        private void HandleReciveMsg(byte[] receiveBytes, int receiveNum)
        {
            int msgID = 0;
            int msgLen = 0;
            int nowIndex = 0;

            //收到消息是 应该看看 之前有没有缓存的 如果有直接拼到后面
            receiveBytes.CopyTo(cacheBytes, cacheNum);
            cacheNum += receiveNum;

            while (true)
            {
                //每次将长度设置为-1 避免上一次的解析数据 影响这一次
                msgLen = -1;
                if (cacheNum - nowIndex >= 8)//如果小于8那么说明分包了(大于8其实也有可能分包)
                {
                    msgID = BitConverter.ToInt32(cacheBytes, nowIndex);
                    nowIndex += 4;
                    msgLen = BitConverter.ToInt32(cacheBytes, nowIndex);
                    nowIndex += 4;
                }

                if (receiveNum - nowIndex >= msgLen && msgLen != -1)
                {
                    BaseMsg baseMsg = null;
                    switch (msgID)
                    {
                        case 1001:
                            PlayerMsg msg = new PlayerMsg();
                            msg.Reading(cacheBytes, nowIndex);
                            baseMsg = msg;
                            break;
                        case 1002:
                            baseMsg = new QuitMsg();
                            break;
                        case 1003:
                            baseMsg = new HeartMsg();
                            break;
                    }
                    if (baseMsg != null)
                    {
                        ThreadPool.QueueUserWorkItem(HandleMsg, baseMsg);
                    }
                    nowIndex += msgLen;

                    if (nowIndex == cacheNum)
                    {
                        cacheNum = 0;
                        break;
                    }
                }
                else
                {
                    //不满足说明分包了
                    //receiveBytes.CopyTo(cacheBytes, 0);
                    //cacheNum = receiveNum;

                    //如果进行了 id和长度的解析 但是没有成功解析消息体 需要减去nowindex移动的8个位置
                    if (msgLen != -1)
                    {
                        nowIndex -= 8;
                    }
                    //把剩余没有解析的字节数组拷贝到前面来
                    Array.Copy(cacheBytes, nowIndex, cacheBytes, 0, cacheNum - nowIndex);
                    cacheNum = cacheNum - nowIndex;
                    break;
                }
            }
        }

        private void HandleMsg(object obj)
        {
            BaseMsg msg = obj as BaseMsg;
            if (msg is PlayerMsg)
            {
                PlayerMsg playerMsg = (msg as PlayerMsg);
                Console.WriteLine(playerMsg.playerID);
                Console.WriteLine(playerMsg.playerData.name);
                Console.WriteLine(playerMsg.playerData.atk);
                Console.WriteLine(playerMsg.playerData.lev);
            }
            else if (msg is QuitMsg)
            {
                ServerMutiOOP.serverSocket.AddDelSocket(this);
            }
            else if (msg is HeartMsg)
            {
                Console.WriteLine("HeartBeatMsg is recive");
                frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            }

        }
    }
}
