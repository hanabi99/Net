using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Lesson7 : MonoBehaviour
{
    public Button btn;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            PlayerMsg ms = new PlayerMsg();
            ms.playerID = 1111;
            ms.playerData = new PlayerData();
            ms.playerData.name = "我给客户端发的信息哦";
            ms.playerData.atk = 1;
            ms.playerData.lev = 1;
            NetMgr.Instance.Send(ms);
        });
        //粘包
        btn1.onClick.AddListener(() => {
            PlayerMsg ms = new PlayerMsg();
            ms.playerID = 1112;
            ms.playerData = new PlayerData();
            ms.playerData.name = "粘包的第一个";
            ms.playerData.atk = 33;
            ms.playerData.lev = 20;

            PlayerMsg ms2 = new PlayerMsg();
            ms2.playerID = 1113;
            ms2.playerData = new PlayerData();
            ms2.playerData.name = "粘包的第二个";
            ms2.playerData.atk = 44;
            ms2.playerData.lev = 30;

            byte[] bytes = new byte[ms.GetBytesNum() + ms2.GetBytesNum()];
            ms.Writing().CopyTo(bytes, 0);
            ms2.Writing().CopyTo(bytes, ms.GetBytesNum());
            NetMgr.Instance.SendTest(bytes);

        });
        //分包
        btn2.onClick.AddListener(() => {
            PlayerMsg ms3 = new PlayerMsg();
            ms3.playerID = 1113;
            ms3.playerData = new PlayerData();
            ms3.playerData.name = "分包1";
            ms3.playerData.atk = 5;
            ms3.playerData.lev = 5;

            byte[] bytes = ms3.Writing();
            byte[] bytes1 = new byte[10];
            byte[] bytes2 = new byte[bytes.Length - 10];

            Array.Copy(bytes, 0, bytes1, 0, 10);
            Array.Copy(bytes, 10, bytes2, 0, bytes.Length - 10);

            NetMgr.Instance.SendTest(bytes1);
            NetMgr.Instance.SendTest(bytes2);
        });
        //both
        btn3.onClick.AddListener(async () => {
            PlayerMsg ms = new PlayerMsg();
            ms.playerID = 1112;
            ms.playerData = new PlayerData();
            ms.playerData.name = "fen粘包的第一个";
            ms.playerData.atk = 33;
            ms.playerData.lev = 20;

            PlayerMsg ms2 = new PlayerMsg();
            ms2.playerID = 1113;
            ms2.playerData = new PlayerData();
            ms2.playerData.name = "fen粘包的第二个";
            ms2.playerData.atk = 44;
            ms2.playerData.lev = 30;


            byte[] bytes1 = ms.Writing();
            byte[] bytes2 = ms2.Writing();

            byte[] bytes2_1 = new byte[10];
            byte[] bytes2_2 = new byte[bytes2.Length - 10];

            Array.Copy(bytes2, 0, bytes2_1, 0, 10);
            Array.Copy(bytes2, 10, bytes2_2, 0, bytes2.Length - 10);


            byte[] bytes = new byte[bytes1.Length + bytes2_1.Length];
            bytes1.CopyTo(bytes, 0);
            bytes2_1.CopyTo(bytes, bytes1.Length);

            NetMgr.Instance.SendTest(bytes);
            await Task.Delay(500);
            NetMgr.Instance.SendTest(bytes2_2);
        });



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
