using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class FtpUpload : MonoBehaviour
{

    void Start()
    {
        try
        {
            //创建FTP连接
            FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) as FtpWebRequest;
            //设置通信凭证
            req.Proxy = null;
            NetworkCredential networkCredential = new NetworkCredential("tianhaochen", "tian20010808");
            req.Credentials = networkCredential;
            //请求完毕后 是否关闭控制连接 
            req.KeepAlive = false;
            //设置操作命令
            req.Method = WebRequestMethods.Ftp.UploadFile;
            //指定传输类型
            req.UseBinary = true;

            //得到用于上传文件的流对象
            Stream upLoadStream = req.GetRequestStream();

            using (FileStream file = File.OpenRead(Application.streamingAssetsPath + "/test.png"))
            {
                byte[] bytes = new byte[1024];

                int contentCout = file.Read(bytes, 0, bytes.Length);

                while (contentCout != 0)
                {
                    upLoadStream.Write(bytes, 0, contentCout);

                    contentCout = file.Read(bytes, 0, bytes.Length);
                }

                file.Close();
                upLoadStream.Close();
                print("上传结束");
            }
        }
        catch(Exception e)
        {
            print("上传失败");
        }
    }
}

