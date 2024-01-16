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
    /// WWW形式下载
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
            //或者自定义类型 通过bytes数组转换
        }
        else
        {
            Debug.LogError("加载失败");
        }

        www.Dispose();
    }
    /// <summary>
    /// 通过UnityWebRequest去获取数据
    /// </summary>
    /// <typeparam name="T">byte[]、Texture、AssetBundle、AudioClip、object（自定义的 如果是object证明要保存到本地）</typeparam>
    /// <param name="path">远端或者本地数据路径 http ftp file</param>
    /// <param name="action">获取成功后的回调函数</param>
    /// <param name="localPath">如果是下载到本地 需要传第3个参数</param>
    /// <param name="type">如果是下载 音效切片文件 需要穿音效类型</param>
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
        else//如果出现没有的类型  就不用继续往下执行了
        {
            Debug.LogWarning("未知类型" + typeof(T));
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
            Debug.LogWarning("获取数据失败" + req.result + req.error + req.responseCode);
        }
    }


    /// <summary>
    /// WWW形式上传发送文件消息
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
            Debug.LogError("发消息出问题" + www.error);
        }

        www.Dispose();


    }

    /// <summary>
    /// UnityWWW形式上传文件以及发送消息
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
            Debug.Log("上传出现问题" + req.error + req.responseCode);
        }

        req.Dispose();
    }





}
