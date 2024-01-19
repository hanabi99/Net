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

    //�ͻ���Socket
    private Socket socket;
    //���ڷ�����Ϣ�Ķ��� �������� ���߳�������� �����̴߳�����ȡ
    private Queue<BaseMsg> sendMsgQueue = new Queue<BaseMsg>();
    //���ڽ�����Ϣ�Ķ��� �������� ���߳�������� ���̴߳�����ȡ
    private Queue<BaseMsg> receiveQueue = new Queue<BaseMsg>();

    //��������Ϣ��ˮͰ��������
    //private byte[] receiveBytes = new byte[1024 * 1024];
    //�����յ����ֽ���
    //private int receiveNum;

    //���ڴ���ְ�
    private byte[] cacheBytes = new byte[1024 * 1024];
    private int cacheNum = 0;

    //�Ƿ�����
    private bool isConnected = false;

    //�������������ʱ��
    private int SEND_HEART_MSG_TIME = 2;

    private HeartMsg heartMsg = new HeartMsg();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //����������Ϣ
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

    //���ӷ����
    public void Connect(string ip, int port)
    {
        //���������״̬ ֱ�ӷ���
        if (isConnected)
            return;

        if (socket == null)
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //���ӷ����
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            socket.Connect(ipPoint);
            isConnected = true;
            //���������߳�
            ThreadPool.QueueUserWorkItem(SendMsg);
            //���������߳�
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            if (e.ErrorCode == 10061)
                print("�������ܾ�����");
            else
                print("����ʧ��" + e.ErrorCode + e.Message);
        }
    }

    //������Ϣ
    public void Send(BaseMsg info)
    {
        sendMsgQueue.Enqueue(info);
    }

    /// <summary>
    /// y���ڲ��� ֱ�ӷ��ֽ�����ķ���
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

    //��ͣ�Ľ�����Ϣ
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
    /// ���������Ϣ �ְ�ճ������
    /// </summary>
    /// <param name="receiveBytes"></param>
    /// <param name="receiveNum"></param>
    private void HandleReciveMsg(byte[] receiveBytes, int receiveNum)
    {
        int msgID = 0;
        int msgLen = 0;
        int nowIndex = 0;

        //�յ���Ϣ�� Ӧ�ÿ��� ֮ǰ��û�л���� �����ֱ��ƴ������
        receiveBytes.CopyTo(cacheBytes, cacheNum);
        cacheNum += receiveNum;

        while (true)
        {
            //ÿ�ν���������Ϊ-1 ������һ�εĽ������� Ӱ����һ��
            msgLen = -1;
            if (cacheNum - nowIndex >= 8)//���С��8��ô˵���ְ���(����8��ʵҲ�п��ְܷ�)
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
                //������˵���ְ���
                //receiveBytes.CopyTo(cacheBytes, 0);
                //cacheNum = receiveNum;

                //��������� id�ͳ��ȵĽ��� ����û�гɹ�������Ϣ�� ��Ҫ��ȥnowindex�ƶ���8��λ��
                if (msgLen != -1)
                {
                    nowIndex -= 8;
                }
                //��ʣ��û�н������ֽ����鿽����ǰ����
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
            //socket.Disconnect(false);//�����Ͽ�����
            //socket.Close();
            socket = null;
            isConnected = false;
            print("�ͻ��������Ͽ�����");
        }
    }

    private void OnDestroy()
    {
        Close();
    }
}
