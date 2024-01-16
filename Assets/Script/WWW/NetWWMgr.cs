using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.Windows;

public class NetWWMgr : MonoBehaviour
{
    private static NetWWMgr instance;

    public static NetWWMgr Instance => instance;

    public readonly string URL_PATH = "http://192.168.1.2:8000/HTTPRoot/";

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// WWW��ʽ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="action"></param>
    public void WWWLoadRes<T>(string path, UnityAction<T> action) where T : class
    {
        StartCoroutine(WWWLoadResAsync<T>(path, action));
    }

    public IEnumerator WWWLoadResAsync<T>(string path, UnityAction<T> action) where T : class
    {
        WWW www = new WWW(path);


        yield return www;

        if (www.error == null)
        {
            if(typeof(T) == typeof(AssetBundle))
            {
                action.Invoke(www.assetBundle as T);
            }
            if (typeof(T) == typeof(Texture))
            {
                action.Invoke(www.texture as T);
            }
            if (typeof(T) == typeof(AudioClip))
            {
                action.Invoke(www.GetAudioClip() as T);
            }
            if (typeof(T) == typeof(string))
            {
                action.Invoke(www.text as T);
            }
            if (typeof(T) == typeof(byte[]))
            {
                action.Invoke(www.bytes as T);
            }
            //�����Զ������� ͨ��bytes����ת��
        }
        else
        {
            Debug.LogError("����ʧ��");
        }

        www.Dispose();
    }
    /// <summary>
    /// ͨ��UnityWebRequestȥ��ȡ����
    /// </summary>
    /// <typeparam name="T">byte[]��Texture��AssetBundle��AudioClip��object���Զ���� �����object֤��Ҫ���浽���أ�</typeparam>
    /// <param name="path">Զ�˻��߱�������·�� http ftp file</param>
    /// <param name="action">��ȡ�ɹ���Ļص�����</param>
    /// <param name="localPath">��������ص����� ��Ҫ����3������</param>
    /// <param name="type">��������� ��Ч��Ƭ�ļ� ��Ҫ����Ч����</param>
    public void UnityWebRequestLoad<T>(string path, UnityAction<T> action, string localPath = "", AudioType type = AudioType.MPEG) where T : class
    {
        StartCoroutine(UnityWebRequestLoadAsync<T>(path, action, localPath, type));
    }

    private IEnumerator UnityWebRequestLoadAsync<T>(string path, UnityAction<T> action, string localPath = "", AudioType type = AudioType.MPEG) where T : class
    {
        UnityWebRequest req = new UnityWebRequest(path, UnityWebRequest.kHttpVerbGET);

        if (typeof(T) == typeof(byte[]))
            req.downloadHandler = new DownloadHandlerBuffer();
        else if (typeof(T) == typeof(Texture))
            req.downloadHandler = new DownloadHandlerTexture();
        else if (typeof(T) == typeof(AssetBundle))
            req.downloadHandler = new DownloadHandlerAssetBundle(req.url, 0);
        else if (typeof(T) == typeof(object))
            req.downloadHandler = new DownloadHandlerFile(localPath);
        else if (typeof(T) == typeof(AudioClip))
            req = UnityWebRequestMultimedia.GetAudioClip(path, type);
        else//�������û�е�����  �Ͳ��ü�������ִ����
        {
            Debug.LogWarning("δ֪����" + typeof(T));
            yield break;
        }

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            if (typeof(T) == typeof(byte[]))
                action?.Invoke(req.downloadHandler.data as T);
            else if (typeof(T) == typeof(Texture))
                //action?.Invoke((req.downloadHandler as DownloadHandlerTexture).texture as T);
                action?.Invoke(DownloadHandlerTexture.GetContent(req) as T);
            else if (typeof(T) == typeof(AssetBundle))
                action?.Invoke((req.downloadHandler as DownloadHandlerAssetBundle).assetBundle as T);
            else if (typeof(T) == typeof(object))
                action?.Invoke(null);
            else if (typeof(T) == typeof(AudioClip))
                action?.Invoke(DownloadHandlerAudioClip.GetContent(req) as T);
        }
        else
        {
            Debug.LogWarning("��ȡ����ʧ��" + req.result + req.error + req.responseCode);
        }
    }


    /// <summary>
    /// WWW��ʽ�ϴ������ļ���Ϣ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseMsg"></param>
    /// <param name="action"></param>
    public void WWWSendMsg<T>(BaseMsg baseMsg , UnityAction<T> action) where T :BaseMsg
    {
        StartCoroutine(WWWSendMsgAsync(baseMsg,action));
    }

    public IEnumerator WWWSendMsgAsync<T>(BaseMsg baseMsg, UnityAction<T> action) where T : BaseMsg
    {
        WWWForm wwwform = new WWWForm();
        wwwform.AddBinaryData("file", baseMsg.Writing());
       

        //WWW www = new WWW(URL_PATH, baseMsg.Writing());
        WWW www = new WWW(URL_PATH, wwwform);

        yield return www;
        
        if(www.error == null)
        {
            int index = 0;
            int msgID = BitConverter.ToInt32(www.bytes, index);
            index += 4;
            int msgLen = BitConverter.ToInt32(www.bytes, index);
            index += 4;
            BaseMsg baseMsg1 = null;
            switch (msgID)
            {
                case 1001:
                    baseMsg1 = new PlayerMsg();
                    baseMsg1.Reading(www.bytes, index);
                    break;
            }
            if(baseMsg1 != null)
            {
                action?.Invoke(baseMsg1 as T);
            }
        }
        else
        {
            Debug.LogError("����Ϣ������" + www.error);
        }

        www.Dispose();


    }

    /// <summary>
    /// UnityWWW��ʽ�ϴ��ļ��Լ�������Ϣ
    /// </summary>
    public void UnityWWWUploadFile(string filename,string localpath,UnityAction<UnityWebRequest.Result> action)
    {
        StartCoroutine(UnityWWWUploadFileAsync(filename, localpath, action));
    }

    private IEnumerator UnityWWWUploadFileAsync(string filename, string localpath, UnityAction<UnityWebRequest.Result> action)
    {
        List<IMultipartFormSection> data = new List<IMultipartFormSection>();

        data.Add(new MultipartFormFileSection(filename, File.ReadAllBytes(localpath)));

        UnityWebRequest req = UnityWebRequest.Post(URL_PATH, data);

        yield return req.SendWebRequest();

        action?.Invoke(req.result);

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("�ϴ���������" + req.error + req.responseCode);
        }

        req.Dispose();
    }





}
