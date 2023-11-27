using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class Client : MonoBehaviour
{
  
    void Start()
    {
        //1.�����׽���
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //2.ȷ������˵Ķ˿� ע���ַҪ���������ַ �����Լ���ϰ�Ǳ���
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        try
        {
            socket.Connect(iPEndPoint);
        }
        catch(SocketException e)
        {
            if(e.ErrorCode == 10061)
            {
                print("server�ܾ�����");
            }
            else
            {
                print("���ӷ�����ʧ��" + e.ErrorCode);
            }
            return;
        }
        //3.��send��receive�շ�����
        byte[] recevieBytes = new byte[1024];
        int recevieBytesNum = socket.Receive(recevieBytes);

        //������Ϣid
        int msgID = BitConverter.ToInt32(recevieBytes, 0);
        switch (msgID)
        {
            default:
                PlayerMsg msg = new PlayerMsg();
                msg.Reading(recevieBytes, 4);
                print(msg.playerID);
                print(msg.playerData.name);
                print(msg.playerData.atk);
                print(msg.playerData.lev);
                break;
        }


        print("�յ�������������" + Encoding.UTF8.GetString(recevieBytes, 0, recevieBytesNum));

        socket.Send(Encoding.UTF8.GetBytes("����THC�Ŀͻ���"));
        //4.��shutdown�ͷ�����
        socket.Shutdown(SocketShutdown.Both);
        //5.�ر��׽���
        socket.Close();
    }


}
