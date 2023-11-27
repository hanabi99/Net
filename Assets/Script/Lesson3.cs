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
        int indexNum = sizeof(int) + sizeof(int) + sizeof(int)//代表字符串解析后数组的长度 
                    + Encoding.UTF8.GetBytes(name).Length + sizeof(float);

        byte[] playerBytes = new byte[indexNum];
        int index = 0;
        //age
        BitConverter.GetBytes(age).CopyTo(playerBytes, index);
        index += sizeof(int);
        //name 先存字符串转为字节数组的长度
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
    // 序列化
    void Start()
    {
        //其他类型转二进制
        byte[] bytes = BitConverter.GetBytes(true);
        print(bytes.Length);
        //字符串转二进制
        byte[] bytes2 = Encoding.UTF8.GetBytes("一");
        print(bytes2.Length);
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.age = 18;
        playerInfo.lev = 2;
        playerInfo.name = "田浩辰";
        playerInfo.speed = 6.5f;
        //得到的是所有变量的字节数组长度
        int indexNum = sizeof(int) + sizeof(int) + sizeof(int)//代表字符串解析后数组的长度 
                      + Encoding.UTF8.GetBytes(playerInfo.name).Length + sizeof(float);

        byte[] playerBytes = new byte[indexNum];
        int index = 0;
        //age
        BitConverter.GetBytes(playerInfo.age).CopyTo(playerBytes, index);
        index += sizeof(int);
        //name 先存字符串转为字节数组的长度
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
