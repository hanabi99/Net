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
            //����һ��ftp���� �����ǻ�ȡ�������ϵ��ļ������ļ�������һ�� �ϴ��Ļ������Զ�����
            FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://192.168.1.2/testpng.png")) as FtpWebRequest;

            req.Credentials = new NetworkCredential("tianhaochen", "tian20010808");

            req.KeepAlive = false;

            req.UseBinary = true;

            req.Method = WebRequestMethods.Ftp.DownloadFile;

            req.Proxy = null;

            //�������͸�FTp ����ֵ �ͻ�Я��������Ҫ����Ϣ
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;

            //��ȡ���ص����ļ�
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
                Debug.Log("���سɹ�");
            }
        }catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}


