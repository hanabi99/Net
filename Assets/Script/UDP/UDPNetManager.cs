using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UDPNetManager : MonoBehaviour
{

    private static UDPNetManager instance;

    public static UDPNetManager Instance => instance;

    //��������EndPoint
    private EndPoint serverIpPoint;

    //�Լ���Socket
    private Socket socket;

    private bool isClose;

    byte[] bytes = new byte[512];

    //�������� ����
    private Queue<BaseMsg> sendQueue = new Queue<BaseMsg>();
    //���ܺͷ�����Ϣ�Ķ��� �ڶ��߳�
    private Queue<BaseMsg> ReceiveQueue = new Queue<BaseMsg>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        if(ReceiveQueue.Count > 0)
        {
            BaseMsg msg = ReceiveQueue.Dequeue();
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

    public void StratClient(string ip,int port)
    {
        if (!isClose)
        {
            return;
        }
        //ָ��������
        serverIpPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
             socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //�뱾�ص�ַ�������ӣ��󶨣�
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
            socket.Bind(iPEndPoint);
            isClose = false;
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
            ThreadPool.QueueUserWorkItem(SendMsg);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    private void ReceiveMsg(object obj)
    {
        EndPoint remoteIpPoint2 = new IPEndPoint(IPAddress.Any, 0);
        int msgID = 0;
        int msgLen = 0;
        int nowIndex = 0;
        while (!isClose)
        {
            if (socket.Available > 0)
            {
                try
                {
                    bytes = new byte[512];
                    socket.ReceiveFrom(bytes,ref remoteIpPoint2);
                    //Ϊ�˱��ⲻ�Ƿ�������������Ϣ ����Ҫ�ж�
                    if (!remoteIpPoint2.Equals(serverIpPoint))
                    {
                        continue;
                    }
                    //������Ϣ
                    msgID = BitConverter.ToInt32(bytes, nowIndex);
                    nowIndex += 4;
                    msgLen = BitConverter.ToInt32(bytes, nowIndex);
                    nowIndex += 4;

                    BaseMsg baseMsg = null;
                    switch (msgID)
                    {
                        case 1001:
                            PlayerMsg msg = new PlayerMsg();
                            msg.Reading(bytes, nowIndex);
                            baseMsg = msg;
                            break;
                    }
                    if (baseMsg != null)
                    {
                        ReceiveQueue.Enqueue(baseMsg);
                    }
                }
                catch (System.Exception e)
                {
                    print(e);
                }
            }
        }
    }
    private void SendMsg(object obj)
    {
        while (!isClose)
        {
            if (socket!= null && sendQueue.Count > 0)
            {
                try
                {
                    socket.SendTo(sendQueue.Dequeue().Writing(), serverIpPoint);
                }
                catch (SocketException s)
                {
                    print(s);
                }
            }
        }
     }

    public void Send(BaseMsg msg)
    {
        sendQueue.Enqueue(msg);
    }

    public void Close()
    {
        if (socket != null)
        {
            isClose = true;
            QuitMsg quitMsg = new QuitMsg();
            socket.SendTo(quitMsg.Writing(), serverIpPoint);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }

    }
}
