using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FtpMgr
{
    private static FtpMgr instance = new FtpMgr();
    public static FtpMgr Instance => instance;

    //Զ��FTP�������ĵ�ַ
    private string FTP_PATH = "ftp://192.168.50.49/";
    //�û���������
    private string USER_NAME = "MrTang";
    private string PASSWORD = "MrTang123";

    /// <summary>
    /// �ϴ��ļ���Ftp���������첽��
    /// </summary>
    /// <param name="fileName">FTP�ϵ��ļ���</param>
    /// <param name="localPath">�����ļ�·��</param>
    /// <param name="action">�ϴ���Ϻ���Ҫ��ʲô��ί�к���</param>
    public async void UpLoadFile(string fileName, string localPath, UnityAction action = null)
    {
        await Task.Run(() =>
        {
            try
            {
                //ͨ��һ���߳�ִ����������߼� ��ô�Ͳ���Ӱ�����߳���
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.UploadFile;
                //��������Ϊ��
                req.Proxy = null;
                //3.�ϴ�
                Stream upLoadStream = req.GetRequestStream();
                //��ʼ�ϴ�
                using (FileStream fileStream = File.OpenRead(localPath))
                {
                    byte[] bytes = new byte[1024];
                    //����ֵ Ϊ�����ȡ�˶��ٸ��ֽ�
                    int contentLength = fileStream.Read(bytes, 0, bytes.Length);
                    //�����ݾ��ϴ�
                    while (contentLength != 0)
                    {
                        //���˶��پ�д(�ϴ�)����
                        upLoadStream.Write(bytes, 0, contentLength);
                        //�����ӱ����ļ��ж�ȡ����
                        contentLength = fileStream.Read(bytes, 0, bytes.Length);
                    }
                    //�ϴ�����
                    fileStream.Close();
                    upLoadStream.Close();
                    
                }
                Debug.Log("�ϴ��ɹ�");
            }
            catch (Exception e)
            {
                Debug.Log("�ϴ��ļ�����" + e.Message);
            }
        });
        //�ϴ������� �������ⲿ��������
        action?.Invoke();
    }

    /// <summary>
    /// �����ļ���Ftp���������У��첽��
    /// </summary>
    /// <param name="fileName">FTP����Ҫ���ص��ļ���</param>
    /// <param name="localPath">�洢�ı����ļ�·��</param>
    /// <param name="action">������Ϻ���Ҫ��ʲô��ί�к���</param>
    public async void DownLoadFile(string fileName, string localPath, UnityAction action = null)
    {
        await Task.Run(()=> {
            try
            {
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                //��������Ϊ��
                req.Proxy = null;
                //3.����
                FtpWebResponse res = req.GetResponse() as FtpWebResponse;
                Stream downLoadStream = res.GetResponseStream();
                //д�뵽�����ļ���
                using (FileStream fileStream = File.Create(localPath))
                {
                    byte[] bytes = new byte[1024];
                    //��ȡ����
                    int contentLength = downLoadStream.Read(bytes, 0, bytes.Length);
                    //һ��һ���д��
                    while (contentLength != 0)
                    {
                        //������ д����
                        fileStream.Write(bytes, 0, contentLength);
                        //������
                        contentLength = downLoadStream.Read(bytes, 0, bytes.Length);
                    }
                    fileStream.Close();
                    downLoadStream.Close();
                }
                res.Close();

                Debug.Log("���سɹ�");
            }
            catch (Exception e)
            {
                Debug.Log("����ʧ��" + e.Message);
            }
        });

        //������ؽ��������������� ����������ⲿ�����ί�к���
        action?.Invoke();
    }


    /// <summary>
    /// �Ƴ�ָ�����ļ�
    /// </summary>
    /// <param name="fileName">�ļ���</param>
    /// <param name="action">�Ƴ���������ʲô��ί�к���</param>
    public async void DeleteFile(string fileName, UnityAction<bool> action = null)
    {
        await Task.Run(()=> {
            try
            {
                //ͨ��һ���߳�ִ����������߼� ��ô�Ͳ���Ӱ�����߳���
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.DeleteFile;
                //��������Ϊ��
                req.Proxy = null;
                //3.������ɾ��
                FtpWebResponse res = req.GetResponse() as FtpWebResponse;
                res.Close();

                action?.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.Log("�Ƴ�ʧ��" + e.Message);
                action?.Invoke(false);
            }        
        });
    }


    /// <summary>
    /// ��ȡFTP��������ĳ���ļ��Ĵ�С ����λ �� �ֽڣ�
    /// </summary>
    /// <param name="fileName">�ļ���</param>
    /// <param name="action">��ȡ�ɹ��󴫵ݸ��ⲿ ����Ĵ�С</param>
    public async void GetFileSize(string fileName, UnityAction<long> action = null)
    {
        await Task.Run(() => {
            try
            {
                //ͨ��һ���߳�ִ����������߼� ��ô�Ͳ���Ӱ�����߳���
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.GetFileSize;
                //��������Ϊ��
                req.Proxy = null;
                //3.�����Ļ�ȡ
                FtpWebResponse res = req.GetResponse() as FtpWebResponse;
                //�Ѵ�С���ݸ��ⲿ
                action?.Invoke(res.ContentLength);

                res.Close();
            }
            catch (Exception e)
            {
                Debug.Log("��ȡ��Сʧ��" + e.Message);
                action?.Invoke(0);
            }
        });
    }


    /// <summary>
    /// ����һ���ļ��� ��FTP��������
    /// </summary>
    /// <param name="directoryName">�ļ�������</param>
    /// <param name="action">������ɺ�Ļص�</param>
    public async void CreateDirectory(string directoryName, UnityAction<bool> action = null)
    {
        await Task.Run(() => {
            try
            {
                //ͨ��һ���߳�ִ����������߼� ��ô�Ͳ���Ӱ�����߳���
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + directoryName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.MakeDirectory;
                //��������Ϊ��
                req.Proxy = null;
                //3.�����Ĵ���
                FtpWebResponse res = req.GetResponse() as FtpWebResponse;
                res.Close();

                action?.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.Log("�����ļ���ʧ��" + e.Message);
                action?.Invoke(false);
            }
        });
    }

    /// <summary>
    /// ��ȥ�����ļ���
    /// </summary>
    /// <param name="directoryName">�ļ���·��</param>
    /// <param name="action">���ظ��ⲿʹ�õ� �ļ����б�</param>
    public async void GetFileList(string directoryName, UnityAction<List<string>> action = null)
    {
        await Task.Run(() => {
            try
            {
                //ͨ��һ���߳�ִ����������߼� ��ô�Ͳ���Ӱ�����߳���
                //1.����һ��Ftp����
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + directoryName)) as FtpWebRequest;
                //2.����һЩ����
                //ƾ֤
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                //�Ƿ���������� �ر� ��������
                req.KeepAlive = false;
                //��������
                req.UseBinary = true;
                //��������
                req.Method = WebRequestMethods.Ftp.ListDirectory;
                //��������Ϊ��
                req.Proxy = null;
                //3.�����Ĵ���
                FtpWebResponse res = req.GetResponse() as FtpWebResponse;
                //�����ص���Ϣ�� ת����StreamReader���� ��������һ��һ�еĶ�ȡ��Ϣ
                StreamReader streamReader = new StreamReader(res.GetResponseStream());

                //���ڴ洢�ļ������б�
                List<string> nameStrs = new List<string>();
                //һ���еĶ�ȡ
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    nameStrs.Add(line);
                    line = streamReader.ReadLine();
                }
                res.Close();

                action?.Invoke(nameStrs);
            }
            catch (Exception e)
            {
                Debug.Log("��ȡ�ļ��б�ʧ��" + e.Message);
                action?.Invoke(null);
            }
        });
    }
}
