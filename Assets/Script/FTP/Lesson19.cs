using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class Lesson19 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ NetworkCredential��
        //�����ռ䣺System.Net
        //NetworkCredentialͨ��ƾ֤��
        //������Ftp�ļ�����ʱ�������˺�����
        NetworkCredential n = new NetworkCredential("MrTang", "MrTang123");
        #endregion

        #region ֪ʶ��� FtpWebRequest��
        //�����ռ䣺System.Net
        //Ftp�ļ�����Э��ͻ��˲�����
        //��Ҫ���ڣ��ϴ������ء�ɾ���������ϵ��ļ�

        //��Ҫ����
        //1.Create �����µ�WebRequest�����ڽ���Ftp��ز���
        FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/Test.txt")) as FtpWebRequest;
        //2.Abort  ������ڽ����ļ����䣬�ô˷���������ֹ����
        req.Abort();
        //3.GetRequestStream  ��ȡ�����ϴ�����
        Stream s = req.GetRequestStream();
        //4.GetResponse  ����FTP��������Ӧ
        //FtpWebResponse res = req.GetResponse() as FtpWebResponse;

        //��Ҫ��Ա
        //1.Credentials ͨ��ƾ֤������ΪNetworkCredential����
        req.Credentials = n;
        //2.KeepAlive boolֵ�����������ʱ�Ƿ�رյ�FTP�������Ŀ������ӣ�Ĭ��Ϊtrue�����رգ�
        req.KeepAlive = false;
        //3.Method  ������������
        //  WebRequestMethods.Ftp���еĲ�����������
        //  DeleteFile  ɾ���ļ�
        //  DownloadFile    �����ļ�    
        //  ListDirectory   ��ȡ�ļ�����б�
        //  ListDirectoryDetails    ��ȡ�ļ���ϸ�б�
        //  MakeDirectory   ����Ŀ¼
        //  RemoveDirectory ɾ��Ŀ¼
        //  UploadFile  �ϴ��ļ�
        req.Method = WebRequestMethods.Ftp.DownloadFile;
        //4.UseBinary �Ƿ�ʹ��2���ƴ���
        req.UseBinary = true;
        //5.RenameTo    ������
        //req.RenameTo = "myTest.txt";
        #endregion

        #region ֪ʶ���� FtpWebResponse��
        //�����ռ䣺System.Net
        //�������ڷ�װFTP���������������Ӧ
        //���ṩ����״̬�Լ��ӷ�������������
        //���ǿ���ͨ��FtpWebRequest�����е�GetResponse()������ȡ
        //��ʹ�����ʱ��Ҫʹ��Close�ͷ�
        
        //ͨ�����������Ĵӷ�������ȡ����
        FtpWebResponse res = req.GetResponse() as FtpWebResponse;

        //��Ҫ������
        //1.Close:�ͷ�������Դ
        res.Close();
        //2.GetResponseStream�����ش�FTP�������������ݵ���
        Stream stream = res.GetResponseStream();

        //��Ҫ��Ա��
        //1.ContentLength:���ܵ����ݵĳ���
        print(res.ContentLength);
        //2.ContentType���������ݵ�����
        print(res.ContentType);
        //3.StatusCode:FTP�������·�������״̬��
        print(res.StatusCode);
        //4.StatusDescription:FTP�������·���״̬������ı�
        print(res.StatusDescription);
        //5.BannerMessage:��¼ǰ��������ʱFTP���������͵���Ϣ
        print(res.BannerMessage);
        //6.ExitMessage:FTP�Ự����ʱ���������͵���Ϣ
        //7.LastModified:FTP�������ϵ��ļ����ϴ��޸����ں�ʱ��
        #endregion

        #region �ܽ�
        //ͨ��C#�ṩ����3����
        //���Ǳ������ɿͻ�����FTP������
        //�����ļ������󣬱���
        //�ϴ������ء�ɾ���ļ�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
