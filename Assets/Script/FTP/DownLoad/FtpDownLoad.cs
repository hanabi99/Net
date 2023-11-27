using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class FtpDownLoad : MonoBehaviour
{
    void Start()
    {
        try
        {
            print(Application.persistentDataPath);

            print(Application.dataPath);

            print(Application.streamingAssetsPath);

            print(Application.temporaryCachePath);
            //创建一个ftp连接 这里是获取服务器上的文件所以文件名必须一致 上传的话就是自定名字
            FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://192.168.1.2/testpng.png")) as FtpWebRequest;

            req.Credentials = new NetworkCredential("tianhaochen", "tian20010808");

            req.KeepAlive = false;

            req.UseBinary = true;

            req.Method = WebRequestMethods.Ftp.DownloadFile;

            req.Proxy = null;

            //把请求发送给FTp 返回值 就会携带我们想要的信息
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;

            //获取下载的流文件
            Stream downLoadStram = res.GetResponseStream();

            print(Application.persistentDataPath);

            print(Application.dataPath);

            print(Application.streamingAssetsPath);

            print(Application.temporaryCachePath);

            using (FileStream file = File.Create(Application.persistentDataPath + "/THC1122.png"))
            {
                byte[] bytes = new byte[1024];

                int contentLength = downLoadStram.Read(bytes, 0, bytes.Length);

                while (contentLength != 0)
                {
                    file.Write(bytes, 0, contentLength);

                    contentLength = downLoadStram.Read(bytes, 0, bytes.Length);
                }

                downLoadStram.Close();
                file.Close();
                Debug.Log("下载成功");
            }
        }catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}


