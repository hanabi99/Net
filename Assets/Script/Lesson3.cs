using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class PlayerInfo
{
    public int lev;
    public int age;
    public string name;
    public float speed;
    public byte[] GetBytes()
    {
        int indexNum = sizeof(int) + sizeof(int) + sizeof(int)//�����ַ�������������ĳ��� 
                    + Encoding.UTF8.GetBytes(name).Length + sizeof(float);

        byte[] playerBytes = new byte[indexNum];
        int index = 0;
        //age
        BitConverter.GetBytes(age).CopyTo(playerBytes, index);
        index += sizeof(int);
        //name �ȴ��ַ���תΪ�ֽ�����ĳ���
        byte[] strbyte = Encoding.UTF8.GetBytes(name);
        BitConverter.GetBytes(strbyte.Length).CopyTo(playerBytes, index);
        index += sizeof(int);
        strbyte.CopyTo(playerBytes, index);
        index += strbyte.Length;
        //lev
        BitConverter.GetBytes(lev).CopyTo(playerBytes, index);
        index += sizeof(int);
        //speed
        BitConverter.GetBytes(speed).CopyTo(playerBytes, index);
        index += sizeof(float);

        return playerBytes;
    }
}
public class Lesson3 : MonoBehaviour
{
    // ���л�
    void Start()
    {
        //��������ת������
        byte[] bytes = BitConverter.GetBytes(true);
        print(bytes.Length);
        //�ַ���ת������
        byte[] bytes2 = Encoding.UTF8.GetBytes("һ");
        print(bytes2.Length);
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.age = 18;
        playerInfo.lev = 2;
        playerInfo.name = "��Ƴ�";
        playerInfo.speed = 6.5f;
        //�õ��������б������ֽ����鳤��
        int indexNum = sizeof(int) + sizeof(int) + sizeof(int)//�����ַ�������������ĳ��� 
                      + Encoding.UTF8.GetBytes(playerInfo.name).Length + sizeof(float);

        byte[] playerBytes = new byte[indexNum];
        int index = 0;
        //age
        BitConverter.GetBytes(playerInfo.age).CopyTo(playerBytes, index);
        index += sizeof(int);
        //name �ȴ��ַ���תΪ�ֽ�����ĳ���
        byte[] strbyte = Encoding.UTF8.GetBytes(playerInfo.name);
        BitConverter.GetBytes(strbyte.Length).CopyTo(playerBytes, index);
        index += sizeof(int);
        strbyte.CopyTo(playerBytes, index);
        index += strbyte.Length;
        //lev
        BitConverter.GetBytes(playerInfo.lev).CopyTo(playerBytes, index);
        index += sizeof(int);
        BitConverter.GetBytes(playerInfo.speed).CopyTo(playerBytes, index);
        index += sizeof(float);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
