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
            //����FTP����
            FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) as FtpWebRequest;
            //����ͨ��ƾ֤
            req.Proxy = null;
            NetworkCredential networkCredential = new NetworkCredential("tianhaochen", "tian20010808");
            req.Credentials = networkCredential;
            //������Ϻ� �Ƿ�رտ������� 
            req.KeepAlive = false;
            //���ò�������
            req.Method = WebRequestMethods.Ftp.UploadFile;
            //ָ����������
            req.UseBinary = true;

            //�õ������ϴ��ļ���������
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
                print("�ϴ�����");
            }
        }
        catch(Exception e)
        {
            print("�ϴ�ʧ��");
        }
    }
}

