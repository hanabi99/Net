using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlayerTest;
using System.IO;
using Google.Protobuf;

public class Lesson40 : MonoBehaviour
{
    //����Э�鷽ʽ
    //��protoc.exe����cmd������
    //����ָ�� -I=����·�� --csharp_out=���·�� �����ļ���   
    //·���в�Ҫ������
    public void Start()
    {
        TestMsg testMsg = new TestMsg();
        testMsg.TestBool = true;
        testMsg.TestStr = "222";
        testMsg.ListInt.Add(2);
        testMsg.TestMap.Add(1, "thc");
        testMsg.TestEnum = TestEnum.Boss;
        testMsg.TestEnum2 = TestMsg.Types.TestEnum2.Boss;
        testMsg.TestHeart = new GameSystemTest.HeartMsg();
        testMsg.TestHeart.Time = 2;
        testMsg.TestMsg3 = new TestMsg.Types.TestMsg3();
        testMsg.TestMsg3.TestInt32 = 2;
        testMsg.TestMsg2 = new TestMsg2();
        testMsg.TestMsg2.TestInt32 = 99;


        //���л�
        using (FileStream fs = File.Create(Application.persistentDataPath + "/TestMsg.thc"))
        {
            testMsg.WriteTo(fs);
        }
        print(Application.persistentDataPath + "TestMsg.thc");
        using (FileStream fs = File.OpenRead(Application.persistentDataPath + "/TestMsg.thc"))
        {
            TestMsg msg = null;
            msg= TestMsg.Parser.ParseFrom(fs);
            print(msg.TestBool);
            print(msg.ListInt[0]);
            print(msg.TestMap[1]);
            print(msg.TestEnum);
            print(msg.TestHeart.Time);
            print(msg.TestMsg2.TestInt32);
        }
        byte[] bytes = null;
        using (MemoryStream ms =  new MemoryStream())
        {
            testMsg.WriteTo(ms);
            bytes = ms.ToArray();
            print("�ֽ����鳤��" + bytes.Length);
        }
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            TestMsg msg = TestMsg.Parser.ParseFrom(ms);
            print(msg.TestBool);
            print(msg.ListInt[0]);
            print(msg.TestMap[1]);
            print(msg.TestEnum);
            print(msg.TestHeart.Time);
            print(msg.TestMsg2.TestInt32);
        }
        //ע�⻹��Ҫ������д��ϢID�ͳ��ȵ� 

        
        print("*************************");

        byte[] bytes2 =NetTool.GetProtoBytes(testMsg);
        Debug.Log(bytes2.Length);
        TestMsg temp = NetTool.GetProtoMsg<TestMsg>(bytes2);
        Debug.Log(temp.TestBool);
        print(temp.TestBool);
        print(temp.ListInt[0]);
        print(temp.TestMap[1]);
        print(temp.TestEnum);
        print(temp.TestHeart.Time);
        print(temp.TestMsg2.TestInt32);

    }
}
