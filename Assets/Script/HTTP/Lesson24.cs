using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class Lesson24 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ HttpWebRequest��
        //�����ռ䣺System.Net
        //HttpWebRequest����Ҫ���ڷ��Ϳͻ����������
        //��Ҫ���ڣ�����HTTP�ͻ�������������������Խ�����Ϣͨ�š��ϴ������صȵȲ���

        //��Ҫ����
        //1.Create �����µ�WebRequest�����ڽ���HTTP��ز���
        HttpWebRequest req = HttpWebRequest.Create(new Uri("http://192.168.50.109:8000/Http_Server/")) as HttpWebRequest;
        //2.Abort  ������ڽ����ļ����䣬�ô˷���������ֹ���� 
        //req.Abort();
        //3.GetRequestStream  ��ȡ�����ϴ�����
        Stream s = req.GetRequestStream();
        //4.GetResponse  ����HTTP��������Ӧ
        HttpWebResponse res = req.GetResponse() as HttpWebResponse;
        //5.Begin/EndGetRequestStream �첽��ȡ�����ϴ�����
        //req.BeginGetRequestStream()
        //6.Begin/EndGetResponse �첽��ȡ���ص�HTTP��������Ӧ
        //req.BeginGetResponse()

        //��Ҫ��Ա
        //1.Credentials ͨ��ƾ֤������ΪNetworkCredential����
        req.Credentials = new NetworkCredential("", "");
        //2.PreAuthenticate �Ƿ���������һ�������֤��ͷ,һ����Ҫ���������֤ʱ��Ҫ��������Ϊtrue
        req.PreAuthenticate = true;

        //3.Headers ���ɱ�ͷ������/ֵ�Եļ���
        //req.Headers
        //4.ContentLength ������Ϣ���ֽ��� �ϴ���Ϣʱ��Ҫ�����ø����ݳ���
        req.ContentLength = 100;
        //5.ContentType �ڽ���POST����ʱ����Ҫ�Է��͵����ݽ����������͵�����
        //6.Method  ������������
        //  WebRequestMethods.Http���еĲ�����������
        //  Get     ��ȡ����һ�����ڻ�ȡ����
        //  Post    �ύ����һ�������ϴ����ݣ�ͬʱ���Ի�ȡ
        //  Head    ��ȡ��Getһ�µ����ݣ�ֻ��ֻ�᷵����Ϣͷ�����᷵�ؾ�������
        //  Put     ��ָ��λ���ϴ���������
        //  Connect ��ʾ�����һ��ʹ�õ� HTTP CONNECT Э�鷽�����ô�����Զ�̬�л������
        //  MkCol   ���������� URI��ͳһ��Դ��ʶ����ָ����λ���½�����

        //�˽����ĸ�����Ϣ
        //https://docs.microsoft.com/zh-cn/dotnet/api/system.net.httpwebrequest?view=net-6.0
        #endregion

        #region ֪ʶ��� HttpWebResponse��
        //�����ռ䣺System.Net
        //����Ҫ���ڻ�ȡ������������Ϣ����
        //���ǿ���ͨ��HttpWebRequest�����е�GetResponse()������ȡ
        //��ʹ�����ʱ��Ҫʹ��Close�ͷ�

        //��Ҫ������
        //1.Close:�ͷ�������Դ
        //2.GetResponseStream�����ش�FTP�������������ݵ���

        //��Ҫ��Ա��
        //1.ContentLength:���ܵ����ݵĳ���
        //2.ContentType���������ݵ�����
        //3.StatusCode:HTTP�������·�������״̬��
        //4.StatusDescription:HTTP�������·���״̬������ı�
        //5.BannerMessage:��¼ǰ��������ʱHTTP���������͵���Ϣ
        //6.ExitMessage:HTTP�Ự����ʱ���������͵���Ϣ
        //7.LastModified:HTTP�������ϵ��ļ����ϴ��޸����ں�ʱ��

        //�˽����ĸ�����Ϣ
        //https://docs.microsoft.com/zh-cn/dotnet/api/system.net.httpwebresponse?view=net-6.0
        #endregion

        #region ֪ʶ���� NetworkCredential��Uri��Stream��FileStream��
        //��Щ��������ѧϰFtpʱ�Ѿ�ʹ�ù���
        //��HTTPͨѶʱʹ�÷�ʽ����
        #endregion

        #region �ܽ�
        //Http���ͨѶ���ʹ�ú�Ftp�ǳ�����
        //ֻ��һЩϸ���ϵ�����
        //֮��������ѧϰ�ϴ�����ʱ�������ؽ���
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
