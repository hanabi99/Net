using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class httTest : MonoBehaviour
{
   
    void Start()
    {
        HttpMrg.Instance.HttpUpLoadAsync("��װ�ϴ�.jpg", Application.persistentDataPath + "/ͼƬ1.jpg", (code) =>
        {
            if (code == System.Net.HttpStatusCode.OK) {
                Debug.Log("�ϴ��ɹ�");
            }
            else
            {
                Debug.Log("ʧ��");
            }
        });
    }


}
