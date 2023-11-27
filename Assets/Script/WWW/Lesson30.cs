using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Lesson30 : MonoBehaviour
{
    public RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ UnityWebRequest��ʲô��
        //UnityWebRequest��һ��Unity�ṩ��һ��ģ�黯��ϵͳ��
        //���ڹ���HTTP����ʹ���HTTP��Ӧ
        //����ҪĿ������Unity��Ϸ��Web����˽��н���
        //����֮ǰWWW����ع��ܶ�������������
        //�����°汾�ж�����ʹ��UnityWebRequest��������WWW��

        //����ʹ���Ϻ�WWW������
        //��Ҫ���������UnityWebRequest���������������ݴ�������ȡ������
        //���ǿ��Ը����Լ�������ѡ���Ӧ�����ݴ����������ȡ����

        //ע�⣺
        //1.UnityWebRequest��WWWһ������Ҫ���Эͬ����ʹ��
        //2.UnityWebRequest��WWWһ����֧��http��ftp��fileЭ�����ػ������Դ
        //3.UnityWebRequest�ܹ��ϴ��ļ���HTTP��Դ������
        #endregion

        #region ֪ʶ��� UnityWebRequest��ĳ��ò���
        //1.ʹ��Get�����ȡ�ı������������
        //2.ʹ��Get�����ȡ��������
        //3.ʹ��Get�����ȡAB������
        //4.ʹ��Post����������
        //5.ʹ��Put�����ϴ�����
        #endregion

        #region ֪ʶ���� Get��ȡ����
        //1.��ȡ�ı���2����
        StartCoroutine(LoadText());
        //2.��ȡ����
        StartCoroutine(LoadTexture());
        //3.��ȡAB��
        StartCoroutine(LoadAB());
        #endregion

        #region �ܽ� 
        //UnityWebRequestʹ���Ϻ�WWW�������
        //������Ҫע�����
        //1.��ȡ�ı������������ʱ
        //  ʹ��UnityWebRequest.Get
        //2.��ȡ����ͼƬ����ʱ
        //  ʹ��UnityWebRequestTexture.GetTexture
        //  �Լ�DownloadHandlerTexture.GetContent
        //3.��ȡAB������ʱ
        //  ʹ��UnityWebRequestAssetBundle.GetAssetBundle
        //  �Լ�DownloadHandlerAssetBundle.GetContent
        #endregion
    }

    IEnumerator LoadText()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://192.168.50.109:8000/Http_Server/test.txt");
        //�ͻ�ȴ� ����������Ӧ�� �Ͽ����Ӻ� �ټ���ִ�к��������
        yield return req.SendWebRequest();

        //�������ɹ� ������ǳɹ�ö��
        if(req.result == UnityWebRequest.Result.Success)
        {
            //�ı� �ַ���
            print(req.downloadHandler.text);
            //�ֽ�����
            byte[] bytes = req.downloadHandler.data;
            print("�ֽ����鳤��" + bytes.Length);
        }
        else
        {
            print("��ȡʧ��:" + req.result + req.error + req.responseCode);
        }
    }

    IEnumerator LoadTexture()
    {
        //UnityWebRequest req = UnityWebRequestTexture.GetTexture("http://192.168.50.109:8000/Http_Server/ʵս��ҵ·��.jpg");

        //UnityWebRequest req = UnityWebRequestTexture.GetTexture("ftp://127.0.0.1/ʵս��ҵ·��.jpg");

        UnityWebRequest req = UnityWebRequestTexture.GetTexture("file://" + Application.streamingAssetsPath + "/test.png");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            //(req.downloadHandler as DownloadHandlerTexture).texture
            //DownloadHandlerTexture.GetContent(req)
            //image.texture = (req.downloadHandler as DownloadHandlerTexture).texture;
            image.texture = DownloadHandlerTexture.GetContent(req);
        }
        else
            print("��ȡʧ��" + req.error + req.result + req.responseCode);
    }

    IEnumerator LoadAB()
    {
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle("http://192.168.50.109:8000/Http_Server/lua");

        req.SendWebRequest();

        while (!req.isDone)
        {
            print(req.downloadProgress);
            print(req.downloadedBytes);
            yield return null;
        }
        //yield return req.SendWebRequest();

        print(req.downloadProgress);
        print(req.downloadedBytes);

        if (req.result == UnityWebRequest.Result.Success)
        {
            //AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(req);
            print(ab.name);
        }
        else
            print("��ȡʧ��" + req.error + req.result + req.responseCode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
