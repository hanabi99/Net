using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Lesson31 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �ϴ����������
        //���ӿ�
        //IMultipartFormSection
        //��������඼�̳иýӿ�
        //���ǿ����ø���װ����
        List<IMultipartFormSection> dataList = new List<IMultipartFormSection>();

        //��������
        //MultipartFormDataSection
        //1.�������ֽ�����
        dataList.Add(new MultipartFormDataSection(Encoding.UTF8.GetBytes("123123123123123")));
        //2.�ַ���
        dataList.Add(new MultipartFormDataSection("12312312312312312dsfasdf"));
        //3.������������ֵ���ֽ����飬�ַ��������������ͣ���Դ���ͣ����ã�
        dataList.Add(new MultipartFormDataSection("Name", "MrTang", Encoding.UTF8, "application/...."));
        dataList.Add(new MultipartFormDataSection("Msg", new byte[1024], "appl....."));

        //MultipartFormFileSection
        //1.�ֽ�����
        dataList.Add(new MultipartFormFileSection(File.ReadAllBytes(Application.streamingAssetsPath + "/test.png")));
        
        //2.�ļ������ֽ����飨���ã�
        dataList.Add(new MultipartFormFileSection("�ϴ����ļ�.png", File.ReadAllBytes(Application.streamingAssetsPath + "/test.png")));
        //3.�ַ������ݣ��ļ��������ã�
        dataList.Add(new MultipartFormFileSection("12312313212312", "test.txt"));
        //4.�ַ������ݣ������ʽ���ļ��������ã�
        dataList.Add(new MultipartFormFileSection("12312313212312", Encoding.UTF8, "test.txt"));
        
        //5.�������ֽ����飬�ļ������ļ�����
        dataList.Add(new MultipartFormFileSection("file", new byte[1024], "test.txt", ""));
        //6.�������ַ������ݣ������ʽ���ļ���
        dataList.Add(new MultipartFormFileSection("file", "123123123", Encoding.UTF8, "test.txt"));
        #endregion

        #region ֪ʶ��� Post�������
        StartCoroutine(Upload());
        #endregion

        #region ֪ʶ���� Put�ϴ����
        //ע�⣺Put�������Ͳ������е�web���������ϣ�����Ҫ���������������������ô��������Ӧ
        #endregion

        #region �ܽ�
        //���ǿ�������Post�ϴ����ݻ��ϴ��ļ�
        //Put��Ҫ�����ϴ��ļ������Ǳ�����Դ������֧��Put��������
        //Ϊ��ͨ���ԣ����ǿ���ͳһʹ��Post�������ͽ������ݺ���Դ���ϴ�
        //����ʹ�ú�֮ǰ��WWW���ƣ�ֻҪǰ����ƶ��ù���Ϳ����໥ͨ����
        #endregion
    }

    IEnumerator Upload()
    {
        //׼���ϴ������� 
        List<IMultipartFormSection> data = new List<IMultipartFormSection>();
        //��ֵ����ص� ��Ϣ �ֶ�����
        data.Add(new MultipartFormDataSection("Name", "MrTang"));
        //PlayerMsg msg = new PlayerMsg();
        //data.Add(new MultipartFormDataSection("Msg", msg.Writing()));
        //���һЩ�ļ��ϴ��ļ�
        //��2�����ļ�
        data.Add(new MultipartFormFileSection("TestTest123.png", File.ReadAllBytes(Application.streamingAssetsPath + "/test.png")));
        //���ı��ļ�
        data.Add(new MultipartFormFileSection("123123123123123", "Test123.txt"));

        UnityWebRequest req = UnityWebRequest.Post("http://192.168.50.109:8000/Http_Server/", data);

        req.SendWebRequest();

        while (!req.isDone)
        {
            print(req.uploadProgress);
            print(req.uploadedBytes);
            yield return null;
        }

        print(req.uploadProgress);
        print(req.uploadedBytes);

        if (req.result == UnityWebRequest.Result.Success)
        {
            print("�ϴ��ɹ�");
            //req.downloadHandler.data
        }
        else
            print("�ϴ�ʧ��" + req.error + req.responseCode + req.result);
    }


    IEnumerator UpLoadPut()
    {

        UnityWebRequest req = UnityWebRequest.Put("http://192.168.50.109:8000/Http_Server/", File.ReadAllBytes(Application.streamingAssetsPath + "/test.png"));

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            print("Put �ϴ��ɹ�");
        }
        else
        {

        }
    }

}
