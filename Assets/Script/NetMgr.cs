using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetMgr : MonoBehaviour
{
    private static NetMgr instance;

    public static NetMgr Instance => instance;

    //客户端Socket
    private Socket socket;
    //用于发送消息的队列 公共容器 主线程往里面放 发送线程从里面取
    private Queue<BaseMsg> sendMsgQueue = new Queue<BaseMsg>();
    //用于接收消息的对象 公共容器 子线程往里面放 主线程从里面取
    private Queue<BaseMsg> receiveQueue = new Queue<BaseMsg>();

    //用于收消息的水桶（容器）
    //private byte[] receiveBytes = new byte[1024 * 1024];
    //返回收到的字节数
    //private int receiveNum;

    //用于处理分包
    private byte[] cacheBytes = new byte[1024 * 1024];
    private int cacheNum = 0;

    //是否连接
    private bool isConnected = false;

    //发送心跳交间隔时间
    private int SEND_HEART_MSG_TIME = 2;

    private HeartMsg heartMsg = new HeartMsg();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //发送心跳消息
        InvokeRepeating("SendHeartBeatMsg",0, SEND_HEART_MSG_TIME);
    }
    private void SendHeartBeatMsg()
    {
        if (isConnected)
        {
            Send(heartMsg);
            print("send heartbeat msg");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (receiveQueue.Count > 0)
        {
            BaseMsg msg = receiveQueue.Dequeue();
            if (msg is PlayerMsg)
            {
                PlayerMsg playerMsg = (msg as PlayerMsg);
                print(playerMsg.playerID);
                print(playerMsg.playerData.name);
                print(playerMsg.playerData.atk);
                print(playerMsg.playerData.lev);
            }
        }
    }

    //连接服务端
    public void Connect(string ip, int port)
    {
        //如果是连接状态 直接返回
        if (isConnected)
            return;

        if (socket == null)
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //连接服务端
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            socket.Connect(ipPoint);
            isConnected = true;
            //开启发送线程
            ThreadPool.QueueUserWorkItem(SendMsg);
            //开启接收线程
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            if (e.ErrorCode == 10061)
                print("服务器拒绝连接");
            else
                print("连接失败" + e.ErrorCode + e.Message);
        }
    }

    //发送消息
    public void Send(BaseMsg info)
    {
        sendMsgQueue.Enqueue(info);
    }

    /// <summary>
    /// y用于测试 直接发字节数组的方法
    /// </summary>
    /// <param name="bytes"></param>
    public void SendTest(byte[] bytes)
    {
        socket.Send(bytes);
    }

    private void SendMsg(object obj)
    {
        while (isConnected)
        {
            if (sendMsgQueue.Count > 0)
            {
                socket.Send(sendMsgQueue.Dequeue().Writing());
            }
        }
    }

    //不停的接受消息
    private void ReceiveMsg(object obj)
    {
        while (isConnected)
        {
            if (socket.Available > 0)
            {
                byte[] receiveBytes = new byte[1024 * 1024];
                int receiveNum = socket.Receive(receiveBytes);
                HandleReciveMsg(receiveBytes, receiveNum);
            }
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
                }
                if (baseMsg != null)
                {
                    receiveQueue.Enqueue(baseMsg);
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

    public void Close()
    {
        if (socket != null)
        {
          
            //QuitMsg quitMsg = new QuitMsg();
            //socket.Send(quitMsg.Writing());
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Disconnect(false);//主动断开连接
            //socket.Close();
            socket = null;
            isConnected = false;
            print("客户端主动断开连接");
        }
    }

    private void OnDestroy()
    {
        Close();
    }
}
