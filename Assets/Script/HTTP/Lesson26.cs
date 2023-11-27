using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class Lesson26 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ Get��Post������
        //�����Ͻڿ�ѧϰ���������ݣ���Ҫʹ�õľ���Get��������
        //�������ϴ�����ʱ����ʹ��Post��������
        //��ô�����������������ǵ���Ҫ������ʲô�أ�

        //1.��Ҫ��;
        //  Get �� һ���ָ������Դ��������,��Ҫ���ڻ�ȡ����
        //  Post �� һ����ָ������Դ�ύ��Ҫ����������ݣ���Ҫ�����ϴ�����

        //2.��ͬ��
        //  Get��Post�����Դ���һЩ����Ĳ������ݸ������

        //3.��ͬ��
        //  3-1:�ڴ��ݲ���ʱ��Post���Get���ӵİ�ȫ����ΪPost����������
        //      Get���ݵĲ����������������У�URL��Դ��λ��ַ�����Ǳ�¶ʽ�� ?������=����ֵ&������=����ֵ
        //      Post���ݵĲ����������������У����������URL�У�������ʽ��
        //
        //  3-2:Get�ڴ�������ʱ�д�С�����ƣ���Ϊ����Ҫ����������ƴ�Ӳ�������URL�ĳ����������Ƶģ���󳤶�һ��Ϊ2048���ַ���
        //      Post�ڴ�������ʱû������
        //
        //  3-3:���������Get�����ܱ����棬Post���ܻ���

        //  3-4:����������ܲ�ͬ
        //      Get:  �������ӡ���>�����С�����ͷ����������һ�δ��䡪��>��ȡ��Ӧ����>�Ͽ�����
        //      Post: �������ӡ���>������ܷ����Ρ���>�����У�����ͷ��һ�δ��䡪��>�������ݵڶ��δ��䡪��>��ȡ��Ӧ����>�Ͽ�

        //����ǰ����˵����ʵGet��Post�����ܹ���ȡ�ʹ������ݵ�,���ֻҪ�����Ӧ�߼�������Ӧ��Ϣ����
        //�����������ǵ���Щ�ص�
        //������ʵ��ʹ��ʱ����Get���ڻ�ȡ��Post�����ϴ�
        //�����Ҫ����һЩ���뱩¶���ⲿ�Ĳ�����Ϣ������ʹ��Post�������ӵİ�ȫ
        #endregion

        #region ֪ʶ��� Post���Я���������
        //�ؼ��㣺��Content-Type����Ϊ application/x-www-form-urlencoded ��ֵ������
        HttpWebRequest req = HttpWebRequest.Create("http://192.168.50.109:8000/Http_Server/") as HttpWebRequest;
        req.Method = WebRequestMethods.Http.Post;
        req.Timeout = 2000;
        //�����ϴ������ݵ�����
        req.ContentType = "application/x-www-form-urlencoded";

        //����Ҫ�ϴ�������
        string str = "Name=MrTang&ID=2";
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        //�������ϴ�֮ǰһ��Ҫ�������ݵĳ���
        req.ContentLength = bytes.Length;
        //�ϴ�����
        Stream stream = req.GetRequestStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
        //�������� �õ���Ӧ���
        HttpWebResponse res = req.GetResponse() as HttpWebResponse;
        print(res.StatusCode);

        #endregion

        #region ֪ʶ���� ContentType�ĳ�������
        //ContentType�Ĺ��ɣ�
        //��������;charset=�����ʽ;boundary=�߽��ַ���
        //text/html;charset=utf-8;boundary=�Զ����ַ���

        //�������������У�
        //�ı�����text��
        //text/plain û���ض������;���������Ҫ��
        //text/html
        //text/css
        //text/javascript

        //ͼƬ����image��
        //image/gif
        //image/png
        //image/jpeg
        //image/bm
        //image/webp
        //image/x-icon
        //image/vnd.microsoft.icon

        //��Ƶ����audio��
        //audio/midi
        //audio/mpeg
        //audio/webm
        //audio/ogg
        //audio/wav

        //��Ƶ����video:
        //video/webm
        //video/ogg

        //����������application:
        //application/octet-stream û���ض������;���������Ҫ��
        //application/x-www-form-urlencoded ���ݲ���ʱʹ�ü�ֵ����ʽ����Ҫ��
        //application/pkcs12
        //application/xhtml+xml
        //application/xml
        //application/pdf
        //application/vnd.mspowerpoint

        //��������multipart:
        //multipart/form-data  �������ݣ��ж���������ϣ���Ҫ��
        //multipart/byteranges  ����ĸ����ļ�


        //����ContentType�������ݿ���ǰ��
        //https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Content-Type
        //����ý�����Ϳ���ǰ��
        //https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Basics_of_HTTP/MIME_types
        #endregion

        #region ֪ʶ���� ContentType�ж���������˵��Ҫ������
        //1.ͨ��2��������
        //application/octet-stream
        //2.ͨ���ı�����
        //text/plain 
        //3.��ֵ�Բ���
        //application/x-www-form-urlencoded
        //4.�������ͣ����ݵ���Ϣ�ж����������,�����м�ֵ�Բ���,���ļ���Ϣ�ȵ�,�ϴ���Դ������ʱ��Ҫ�ø����ͣ�
        //multipart/form-data
        #endregion

        #region �ܽ�
        //��ڿε��ص�֪ʶ����
        //1.Get��Post������
        //2.ContentType����Ҫ����

        //ע�⣺
        //HTTPͨѶ��
        //�ͻ��˷��͸�����˵�Get��Post������Ҫ����˺Ϳͻ���Լ��һЩ������д���
        //���紫�ݵĲ����ĺ��壬������δ���ȵȣ�������Ҫǰ��˳����ƶ���Ӧ���������д����
        //ֻ������Ŀǰû�к�˿�����HTTP���������������Ǵ��ݹ�ȥ�Ĳ���������û�еõ���Ӧ����
        //����Ŀǰֻ���HTTP��Դ�������ϴ��������ݽ���ѧϰ
        //���ǵ�ͨѶԭ����һ�µģ�����ͨ��HTTPͨѶ��������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
