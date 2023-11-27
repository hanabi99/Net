using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class httTest : MonoBehaviour
{
   
    void Start()
    {
        HttpMrg.Instance.HttpUpLoadAsync("封装上传.jpg", Application.persistentDataPath + "/图片1.jpg", (code) =>
        {
            if (code == System.Net.HttpStatusCode.OK) {
                Debug.Log("上传成功");
            }
            else
            {
                Debug.Log("失败");
            }
        });
    }


}
