using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class Lesson4 : MonoBehaviour
{

    private void Start()
    {
        double k = 10.9f;
        byte[] bytes0 = BitConverter.GetBytes(k);
        Debug.Log(bytes0.Length);
        // �����л� ���ַ�������
        byte[] bytes = BitConverter.GetBytes(99);
        int i = BitConverter.ToInt32(bytes, 0);
       // print(i);
        //�����л� �ַ���
        byte[] bytes2 = Encoding.UTF8.GetBytes("��Ѷʲôʱ�򵹱�");
        string str =  Encoding.UTF8.GetString(bytes2, 0, bytes2.Length);
       // print(str);
        //�����л�����
        PlayerInfo info = new PlayerInfo();
        info.age = 18;
        info.lev = 3;
        info.name = "��Ѷ����";
        info.speed = 5;
        byte[] playerInfoBytes =  info.GetBytes();

        PlayerInfo info2 = new PlayerInfo();
        int index = 0;
        info2.age = BitConverter.ToInt32(playerInfoBytes, 0);
        print(info2.age);
        index += sizeof(int);
        int len = BitConverter.ToInt32(playerInfoBytes, index);
        index += 4;
        info2.name = Encoding.UTF8.GetString(playerInfoBytes, index, len);
        print(info2.name);
        index += len;
        info2.lev = BitConverter.ToInt32(playerInfoBytes, index);
        print(info2.lev);
        index += 4;
        info2.speed = BitConverter.ToSingle(playerInfoBytes, index);
        print(info2.speed);
        index += 8;

    }
}
