using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Lesson32 : MonoBehaviour
{
    public RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �߼�����ָʲô��
        //�ڳ��ò���������ʹ�õ���UnityΪ���Ƿ�װ�õ�һЩ����
        //���ǿ��Է���Ľ���һЩָ�����͵����ݻ�ȡ

        //����
        //��������ʱ��
        //1.�ı���2����
        //2.ͼƬ
        //3.AB��
        //���������Ҫ��ȡ�������͵�����Ӧ����δ����أ�

        //�ϴ�����ʱ��
        //1.����ָ��������ֵ
        //2.�����ϴ��ļ�
        //�����Ҫ�ϴ�һЩ����HTTP�������������Ӧ����δ����أ�

        //�߼����������������� ���ò���������ɵ������
        //���ĺ���˼����ǣ�UnityWebRequest�п��Խ����ݴ�����뿪
        //���糣������������õ���
        //DownloadHandlerTexture �� DownloadHandlerAssetBundle������
        //����������2�����ֽ�����ת���ɶ�Ӧ���ͽ��д����

        //���Ը߼�����ʱָ ���㰴�չ�����ʵ�ָ�������ݻ�ȡ���ϴ��ȹ���
        #endregion

        #region ֪ʶ��� UnityWebRequest��ĸ�������
        //Ŀǰ��ѧ������
        //UnityWebRequest req = UnityWebRequest.Get("");
        //UnityWebRequest req = UnityWebRequestTexture.GetTexture("");
        //UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle("");
        //UnityWebRequest req = UnityWebRequest.Put()
        //UnityWebRequest req = UnityWebRequest.Post

        //req.isDone
        //req.downloadProgress;
        //req.downloadedBytes;
        //req.uploadProgress;
        //req.uploadedBytes

        //req.SendWebRequest()

        //��������
        //1.���캯��
        UnityWebRequest req = new UnityWebRequest();

        //2.�����ַ
        //req.url = "��������ַ";

        //3.��������
        //req.method = UnityWebRequest.kHttpVerbPOST;

        //4.����
        //req.downloadProgress
        //req.uploadProgress

        //5.��ʱ����
        //req.timeout = 2000;

        //6.�ϴ������ص��ֽ���
        //req.downloadedBytes
        //req.uploadedBytes

        //7.�ض������ ����Ϊ0��ʾ�������ض��� �������ô���
        //req.redirectLimit = 10;

        //8.״̬�롢�������������
        //req.result
        //req.error
        //req.responseCode

        //9.���ء��ϴ��������
        //req.downloadHandler
        //req.uploadHandler

        //��������
        //https://docs.unity.cn/cn/2020.3/ScriptReference/Networking.UnityWebRequest.html
        #endregion

        #region ֪ʶ���� �Զ����ȡ����DownloadHandler�����
        //�ؼ��ࣺ
        //1.DownloadHandlerBuffer ���ڼ򵥵����ݴ洢���õ���Ӧ��2�������ݡ�
        //2.DownloadHandlerFile ���������ļ������ļ����浽���̣��ڴ�ռ���٣���
        //3.DownloadHandlerTexture ��������ͼ��
        //4.DownloadHandlerAssetBundle ������ȡ AssetBundle��
        //5.DownloadHandlerAudioClip ����������Ƶ�ļ���

        StartCoroutine(DownLoadTex());

       // StartCoroutine(DownLoadAB());

        //���ϵ���Щ�࣬��ʵ����Unity��������ʵ�ֺõģ����ڽ����������������ݵ���
        //ʹ�ö�Ӧ���ദ���������ݣ����Ǿͻ����ڲ������ص����ݴ���Ϊ��Ӧ�����ͣ���������ʹ��

        //DownloadHandlerScript ��һ�������ࡣ���䱾����ԣ�����ִ���κβ�����
        //���ǣ���������û��������̳С������������ UnityWebRequest ϵͳ�Ļص���
        //Ȼ�����ʹ����Щ�ص������ݴ����絽��ʱִ����ȫ�Զ�������ݴ���

       // StartCoroutine(DownLoadCustomHandler());
        #endregion

        #region �ܽ�
        //���ǿ����Լ�����UnityWebRequest���е����ش������
        //�����ú��������ݺ�����ʹ�øö����ж�Ӧ�ĺ�����������
        //�����Ǹ�����Ļ�ȡ������Ҫ������
        //�������Ƕ��������ػ��ȡ������չ
        #endregion
    }

    IEnumerator DownLoadTex()
    {
        UnityWebRequest req = new UnityWebRequest("http://192.168.1.2:8000/HTTPRoot/test.png", 
                                                   UnityWebRequest.kHttpVerbGET);
        //req.method = UnityWebRequest.kHttpVerbGET;
        //1.DownloadHandlerBuffer
        //DownloadHandlerBuffer bufferHandler = new DownloadHandlerBuffer();
        //req.downloadHandler = bufferHandler;

        //2.DownloadHandlerFile
        //���ڱ���
       // print(Application.persistentDataPath);
      //  req.downloadHandler = new DownloadHandlerFile(Application.persistentDataPath + "/downloadFile.jpg");

        //3.DownloadHandlerTexture
        DownloadHandlerTexture textureHandler = new DownloadHandlerTexture();
        req.downloadHandler = textureHandler;

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            //��ȡ�ֽ�����
            //bufferHandler.data

            //textureHandler.texture
            image.texture = textureHandler.texture;
        }
        else
        {
            print("��ȡ����ʧ��" + req.result + req.error + req.responseCode);
        }
    }

    IEnumerator DownLoadAB()
    {
        UnityWebRequest req = new UnityWebRequest("http://192.168.50.109:8000/Http_Server/lua", UnityWebRequest.kHttpVerbGET);
        //�ڶ������� ��Ҫ��֪У���� ���ܽ��бȽ� ��������� �����֪���Ļ� ֻ�ܴ�0 �����������Եļ��
        //����һ�� ֻ�н���AB���ȸ���ʱ ������������ ��Ӧ�� �ļ��б��� ������ ��֤�� ���ܽ��м��
        DownloadHandlerAssetBundle handler = new DownloadHandlerAssetBundle(req.url, 0);
        req.downloadHandler = handler;

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            AssetBundle ab = handler.assetBundle;
            
            print(ab.name);
        }
        else
        {
            print("��ȡ����ʧ��" + req.result + req.error + req.responseCode);
        }
    }

    IEnumerator DownLoadAudioClip()
    {
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("http://192.168.50.109:8000/Http_Server/��Ч��.mp3",                                               
            AudioType.MPEG);

        DownloadHandlerAudioClip handlerAudioClip = new DownloadHandlerAudioClip(req.url, AudioType.MPEG);
        req.downloadHandler = handlerAudioClip;
        yield return req.SendWebRequest();
        
        if (req.result == UnityWebRequest.Result.Success)
        {
            //���ַ�ʽ������
            //AudioClip b =  handlerAudioClip.audioClip;
            AudioClip a = DownloadHandlerAudioClip.GetContent(req);
        }
        else
        {
            print("��ȡ����ʧ��" + req.result + req.error + req.responseCode);
        }
    }

    IEnumerator DownLoadCustomHandler()
    {
        UnityWebRequest req = new UnityWebRequest("http://192.168.50.109:8000/Http_Server/21.�����.mp4", UnityWebRequest.kHttpVerbGET);

        //ʹ���Զ�������ش������ �������ȡ���� 2�����ֽ�����
        print(Application.persistentDataPath);
        req.downloadHandler = new CustomDownLoadFileHandler(Application.persistentDataPath + "/CustomHandler.mp4");

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            print("�洢���سɹ�");
        }
        else
        {
            print("��ȡ����ʧ��" + req.result + req.error + req.responseCode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class CustomDownLoadFileHandler:DownloadHandlerScript
{
    //���ڱ��� ���ش洢ʱ��·��
    private string savePath;

    //���ڻ����յ������ݵ�����
    private byte[] cacheBytes;
    //���ǵ�ǰ���յ������ݳ���
    private int index = 0;

    public CustomDownLoadFileHandler():base()
    {

    }

    public CustomDownLoadFileHandler(byte[] bytes) :base(bytes)
    {

    }

    public CustomDownLoadFileHandler(string path) : base()
    {
        savePath = path;
    }

    protected override byte[] GetData()
    {
        //�����ֽ�����
        return cacheBytes;
    }

    /// <summary>
    /// �������յ����ݺ� ÿ֡����õķ���  ���Զ����õķ���
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dataLength"></param>
    /// <returns></returns>
    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        Debug.Log("�յ����ݳ��ȣ�" + data.Length);
        Debug.Log("�յ����ݳ���dataLength��" + dataLength);
        data.CopyTo(cacheBytes, index);
        index += dataLength;
        return true;
    }

    /// <summary>
    /// �ӷ������յ� COntent-Length��ͷʱ  ���Զ����õķ���
    /// </summary>
    /// <param name="contentLength"></param>
    protected override void ReceiveContentLengthHeader(ulong contentLength)
    {
        //base.ReceiveContentLengthHeader(contentLength);
        Debug.Log("�յ����ݳ��ȣ�" + contentLength);
        //�����յ��ı�ͷ �����ֽ����������Ĵ�С
        cacheBytes = new byte[contentLength];
    }

    /// <summary>
    /// ����Ϣ������ ���Զ����õķ���
    /// </summary>
    protected override void CompleteContent()
    {
        Debug.Log("��Ϣ����");
        //���յ����ֽ����� �����Զ��崦�� �������� ����� �洢������
        File.WriteAllBytes(savePath, cacheBytes);
    }

}
