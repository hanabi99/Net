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
        {//检测资源可用性
            HttpWebRequest req1 = WebRequest.Create(new Uri("http://192.168.1.2:8000/HTTPRoot/图片1.png")) as HttpWebRequest;
          
            req1.Method = WebRequestMethods.Http.Head;

            //设置请求类型  设置超时
            req1.Timeout = 2000;

            HttpWebResponse response = req1.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                print("文件存在且可用");
                print(response.ContentLength);
                print(response.ContentType);
                response.Close();
            }
            else
            {
                print("文件不可用" + response.StatusCode);
            }
        }
        catch (WebException e)
        {
            print(e);
        }
        //下载数据
        try
        {
            HttpWebRequest req2 = WebRequest.Create(new Uri("http://192.168.1.2:8000/HTTPRoot/图片1.jpg")) as HttpWebRequest;

            req2.Method = WebRequestMethods.Http.Get;

            //设置请求类型  设置超时
            req2.Timeout = 2000;

            HttpWebResponse response2 = req2.GetResponse() as HttpWebResponse;

            if (response2.StatusCode == HttpStatusCode.OK)
            {
                print(Application.persistentDataPath);
                using (FileStream fileStream = File.Create(Application.persistentDataPath + "/图片1.jpg"))
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
                print("下载成功");
            }
            else
            {
                print("下载失败");
            }
        }
        catch (WebException w)
        {
            print(w);
        }
       
    }


}
