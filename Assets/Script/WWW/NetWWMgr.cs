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
