using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainWWW : MonoBehaviour
{
    public RawImage image;
    private void Start()
    {
        if (NetWWMgr.Instance == null)
        {
            GameObject gameObject = new GameObject("WWW");
            gameObject.AddComponent<NetWWMgr>();
        }

        //NetWWMgr.Instance.WWWLoadRes<Texture>("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fi2.hdslb.com%2Fbfs%2Farchive%2F8cc2b9a7868b266800f98d42fc5d257021e75103.jpg&refer=http%3A%2F%2Fi2.hdslb.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1654745686&t=b7691b6e546367610e4331039d1a10ec", (obj) => {

        //    image.texture = obj;

        //    //获取图片去本地
        //    //File.WriteAllBytes();
        //});


        //Debug.Log(Application.persistentDataPath);

        //NetWWMgr.Instance.UnityWWWUploadFile("UnityWWW.jpg", Application.persistentDataPath +"/图片1.jpg", (result) =>
        //{
        //    if(result == UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log("yes");

        //    }
        //    else
        //    {
        //        Debug.Log("no");
        //    }

        //});
        NetWWMgr.Instance.UnityWebRequestLoad<Texture>("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fi2.hdslb.com%2Fbfs%2Farchive%2F8cc2b9a7868b266800f98d42fc5d257021e75103.jpg&refer=http%3A%2F%2Fi2.hdslb.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1654745686&t=b7691b6e546367610e4331039d1a10ec", (texture) => {
            image.texture = texture;
        });
        NetWWMgr.Instance.UnityWebRequestLoad<byte[]>("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fi2.hdslb.com%2Fbfs%2Farchive%2F8cc2b9a7868b266800f98d42fc5d257021e75103.jpg&refer=http%3A%2F%2Fi2.hdslb.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1654745686&t=b7691b6e546367610e4331039d1a10ec", (bytes) => {
            Debug.Log(bytes.Length);
        });


    }

}
