using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FtpTest : MonoBehaviour
{
    void Start()
    {
        TtpUplLoadAsync.Instance.UpLoadFile("tianhaochen.png", Application.streamingAssetsPath + "/test.png", () => { Debug.Log("ɹ"); });
    }

}
