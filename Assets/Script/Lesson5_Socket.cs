using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class Lesson5_Socket : MonoBehaviour
{
    //AddressFamily addressFamily = AddressFamily.InterNetworkV6; IPV6Ѱַ

    void Start()
    {
        // AddressFamily.InterNetwork ipv4Ѱַ
        //TCP ���׽���
        Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //UDP���ݱ��׽���
        Socket socketUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //1.�׽��ֵ�����״̬
        if (socketTcp.Connected)
        {

        }
        //2.�׽��ֵ�����
        print(socketTcp.SocketType);
        //3.��ȡ�׽��ֵ�Э������
        print(socketTcp.ProtocolType);
        //4.��ȡ�׽��ֵ�Ѱַ����
        print(socketTcp.AddressFamily);
        //5.�������л�ȡ׼����ȡ��������
        print(socketTcp.Available);
        //6.��ȡ����ENDPOINT����
        //socketTcp.LocalEndPoint as IPEndPoint
        //7.��ȡԶ��ENDpoint����
        //socketTcp.RemoteEndPoint as IPEndPoint

        //Socket���÷���
        //��Ҫ���ڷ����
        //��IP�˿�
        IPEndPoint ippoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        socketTcp.Bind(ippoint);
        //���ÿͻ������ӵ��������
        socketTcp.Listen(10);
        //�ȴ��ͻ��˽���
        socketTcp.Accept();

        //��Ҫ���ڿͻ���
        //����Զ�˷�����
        socketTcp.Connect(IPAddress.Parse("115.2.2.1"), 8080);
        //CS�����õ�
        //���ܺͷ�������
        //�ͷ����Ӳ��ر�
        socketTcp.Shutdown(SocketShutdown.Both);//Both����ͬʱֹͣ���պͷ���
        socketTcp.Close();


    }

    void Update()
    {
        
    }
}
