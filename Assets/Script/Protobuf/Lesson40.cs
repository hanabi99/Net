using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlayerTest;

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
        testMsg.TestMsg3 = new TestMsg.Types.TestMsg3();
        testMsg.TestMsg3.TestInt32 = 2;
        testMsg.TestMsg2 = new TestMsg2();
        testMsg.TestMsg2.TestInt32 = 99;
    }
}
