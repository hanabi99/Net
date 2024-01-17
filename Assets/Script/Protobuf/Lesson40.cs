using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlayerTest;

public class Lesson40 : MonoBehaviour
{
    //生成协议方式
    //将protoc.exe拖入cmd命令行
    //输入指令 -I=配置路径 --csharp_out=输出路径 配置文件名   
    //路径中不要有中文
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
