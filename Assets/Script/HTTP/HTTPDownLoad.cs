using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class HTTPDownLoad : MonoBehaviour
{

    void Start()
    {
        try
        {//�����Դ������
            HttpWebRequest req1 = WebRequest.Create(new Uri("http://192.168.1.2:8000/HTTPRoot/ͼƬ1.png")) as HttpWebRequest;
          
            req1.Method = WebRequestMethods.Http.Head;

            //������������  ���ó�ʱ
            req1.Timeout = 2000;

            HttpWebResponse response = req1.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                print("�ļ������ҿ���");
                print(response.ContentLength);
                print(response.ContentType);
                response.Close();
            }
            else
            {
                print("�ļ�������" + response.StatusCode);
            }
        }
        catch (WebException e)
        {
            print(e);
        }
        //��������
        try
        {
            HttpWebRequest req2 = WebRequest.Create(new Uri("http://192.168.1.2:8000/HTTPRoot/ͼƬ1.jpg")) as HttpWebRequest;

            req2.Method = WebRequestMethods.Http.Get;

            //������������  ���ó�ʱ
            req2.Timeout = 2000;

            HttpWebResponse response2 = req2.GetResponse() as HttpWebResponse;

            if (response2.StatusCode == HttpStatusCode.OK)
            {
                print(Application.persistentDataPath);
                using (FileStream fileStream = File.Create(Application.persistentDataPath + "/ͼƬ1.jpg"))
                {
                    byte[] bytes = new byte[2048];

                    Stream stream = response2.GetResponseStream();

                    int contentLen = stream.Read(bytes, 0, bytes.Length);
                    while (contentLen != 0)
                    {
                        fileStream.Write(bytes, 0, contentLen);
                        contentLen = stream.Read(bytes, 0, bytes.Length);
                    }
                    fileStream.Close();
                    stream.Close();
                    response2.Close();
                }
                print("���سɹ�");
            }
            else
            {
                print("����ʧ��");
            }
        }
        catch (WebException w)
        {
            print(w);
        }
       
    }


}
