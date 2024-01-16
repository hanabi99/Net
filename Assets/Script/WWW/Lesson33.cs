using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Lesson33 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �ع˸߼������еĻ�ȡ����
        //��Ҫ��������2�����ֽ����鴦�����������ش�������н��д���
        //         ��Ҫ��������UnityWebRequest������ downloadHandler ����
        //Unityд�õ�����
        //1.DownloadHandlerBuffer ���ڼ򵥵����ݴ洢���õ���Ӧ��2�������ݡ�
        //2.DownloadHandlerFile ���������ļ������ļ����浽���̣��ڴ�ռ���٣���
        //3.DownloadHandlerTexture ��������ͼ��
        //4.DownloadHandlerAssetBundle ������ȡ AssetBundle��
        //5.DownloadHandlerAudioClip ����������Ƶ�ļ���

        //�Լ���չ����ʽ
        //�̳� DownloadHandlerScript
        //����д���еĹ̶��������Լ������ֽ�����
        #endregion

        #region ֪ʶ��� �Զ����ϴ�����UploadHandler�����
        //ע�⣺
        //����UnityWebRequest��ĳ��ò�����
        //�ϴ�������������Ѿ���װ�ĺܺ���
        //���ǿ��Ժܷ�����ϴ��������ļ�
        //����ʹ�ó��ò����Ѿ��ܹ����㳣��������
        //��������������Ҫ���˽�

        //UploadHandler�����
        //1.UploadHandlerRaw  �����ϴ��ֽ�����
        StartCoroutine(UpLoad());
        //2.UploadHandlerFile �����ϴ��ļ�

        //���бȽ���Ҫ�ı�����
        //contentType �������ͣ���������ã�ģʽ�� application/octet-stream 2����������ʽ
        #endregion

        #region �ܽ�
        //�����ϴ�������� UnityWebRequestԭ���Ѿ��ṩ�˽�Ϊ���Ƶ�
        //�����ϴ����ļ��ϴ���ع���
        //���Ը߼������е� �ϴ��������������չ���٣�ʹ��Ҳ����
        //����ʹ�ó��ò������ϴ�������ع��ܾ��㹻��
        //�߼��������ϴ�����֪ʶ����Ҫ���˽�
        #endregion
    }

    IEnumerator UpLoad()
    {
        UnityWebRequest req = new UnityWebRequest("http://192.168.50.109:8000/Http_Server/", UnityWebRequest.kHttpVerbPOST);

        //1.UploadHandlerRaw  �����ϴ��ֽ�����
        //byte[] bytes = Encoding.UTF8.GetBytes("123123123123123");
        //req.uploadHandler = new UploadHandlerRaw(bytes);
        //req.uploadHandler.contentType = "����/ϸ������";

        //2.UploadHandlerFile �����ϴ��ļ�
        req.uploadHandler = new UploadHandlerFile(Application.streamingAssetsPath + "/test.png");

        yield return req.SendWebRequest();

        print(req.result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
