using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class Lesson27 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �ϴ��ļ���HTTP��Դ��������Ҫ���صĹ���
        //�ϴ��ļ�ʱ���ݵıر�����
        //  1:ContentType = "multipart/form-data; boundary=�߽��ַ���";

        //  2:�ϴ������ݱ��밴�ո�ʽд������
        //  --�߽��ַ���
        //  Content-Disposition: form-data; name="�ֶ����֣�֮��д����ļ�2�������ݺ͸��ֶ�����Ӧ";filename="������������ʹ�õ��ļ���"
        //  Content-Type:application/octet-stream���������Ǵ�2�����ļ� ��������ʹ��2���ƣ�
        //  ��һ��
        //  ������ֱ��д�봫������ݣ�
        //  --�߽��ַ���--

        //  3:��֤�����������ϴ�
        //  4:д����ǰ��Ҫ������ContentLength���ݳ���
        #endregion

        #region ֪ʶ��� �ϴ��ļ�
        //1.����HttpWebRequest����
        HttpWebRequest req = HttpWebRequest.Create("http://192.168.1.2:8000/HTTPRoot/") as HttpWebRequest;
        //2.������ã��������ͣ��������ͣ���ʱ�������֤�ȣ�
        req.Method = WebRequestMethods.Http.Post;
        req.ContentType = "multipart/form-data;boundary=MrTHC";
        req.Timeout = 500000;
        req.Credentials = new NetworkCredential("THC333", "123123");
        req.PreAuthenticate = true;//����֤��� ���ϴ�����

        //3.����ʽƴ���ַ�������תΪ�ֽ�����֮�������ϴ�
        //3-1.�ļ�����ǰ��ͷ����Ϣ
        //  --�߽��ַ���
        //  Content-Disposition: form-data; name="�ֶ����֣�֮��д����ļ�2�������ݺ͸��ֶ�����Ӧ";filename="������������ʹ�õ��ļ���"
        //  Content-Type:application/octet-stream���������Ǵ�2�����ļ� ��������ʹ��2���ƣ�
        //  ��һ��
        string head = "--MrTHC\r\n" +
            "Content-Disposition:form-data;name=\"file\";filename=\"http�ϴ����ļ�.jpg\"\r\n" +
            "Content-Type:application/octet-stream\r\n\r\n";
        //ͷ��ƴ���ַ���������Ϣ���ֽ�����
        byte[] headBytes = Encoding.UTF8.GetBytes(head);

        //3-2.�����ı߽���Ϣ
        //  --�߽��ַ���--
        byte[] endBytes = Encoding.UTF8.GetBytes("\r\n--MrThc--\r\n");

        //4.д���ϴ���
        using (FileStream localFileStream = File.OpenRead(Application.persistentDataPath + "/ͼƬ1.jpg"))
        {
            //4-1.�����ϴ�����
            //�ܳ��� ��ǰ�����ַ��� + �ļ������ж�� + �󲿷ֱ߽��ַ���
            req.ContentLength = headBytes.Length + localFileStream.Length + endBytes.Length;
            //�����ϴ�����
            Stream upLoadStream = req.GetRequestStream();
            //4-2.��д��ǰ����ͷ����Ϣ
            upLoadStream.Write(headBytes, 0, headBytes.Length);
            //4-3.��д���ļ�����
            byte[] bytes = new byte[2048];
            int contentLength = localFileStream.Read(bytes, 0, bytes.Length);
            while (contentLength != 0)
            {
                upLoadStream.Write(bytes, 0, contentLength);
                contentLength = localFileStream.Read(bytes, 0, bytes.Length);
            }
            //4-4.��д������ı߽���Ϣ
            upLoadStream.Write(endBytes, 0, endBytes.Length);

            upLoadStream.Close();
            localFileStream.Close();
        }

        //5.�ϴ����ݣ���ȡ��Ӧ
        HttpWebResponse res = req.GetResponse() as HttpWebResponse;
        if (res.StatusCode == HttpStatusCode.OK)
            print("�ϴ�ͨ�ųɹ�");
        else
            print("�ϴ�ʧ��" + res.StatusCode);
        #endregion

        #region �ܽ�
        //HTTP�ϴ��ļ���ԱȽ��鷳
        //��Ҫ����ָ���Ĺ����������ƴ�Ӵﵽ�ϴ��ļ���Ŀ��
        //���������Ҫ��֪ʶ���� 
        //�ϴ��ļ�ʱ�Ĺ���
        //  --�߽��ַ���
        //  Content-Disposition: form-data; name="file";filename="������������ʹ�õ��ļ���"
        //  Content-Type:application/octet-stream���������Ǵ�2�����ļ� ��������ʹ��2���ƣ�
        //  ����
        //  ������ֱ��д�봫������ݣ�
        //  --�߽��ַ���--

        //���������Ĺ��򣬿���ǰ�������鿴��ϸ˵��
        //����ContentType�������ݿ���ǰ��
        //https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Content-Type
        //����ý�����Ϳ���ǰ��
        //https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Basics_of_HTTP/MIME_types
        //����Content-Disposition�������ݿ���ǰ��
        //https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Content-Disposition
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
