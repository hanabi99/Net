using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Lesson29 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 WWWFrom类的作用
        //上节课学习了使用WWW类来下载数据
        //如果想要使用WWW上传数据时，就需要配合WWWFrom类进行使用了
        //而WWWFrom主要就是用于集成数据的，我们可以设置上传的参数或者2进制数据
        //当结合WWWFrom上传数据时
        //它主要用到的请求类型是Post
        //它使用Http协议进行上传处理

        //注意：
        //使用WWW结合WWWFrom上传数据一般需要配合后端程序制定上传规则
        #endregion

        #region 知识点二 WWWFrom类的常用方法和变量
        //该类当中我们主要就使用方法，相关变量很少使用，我们主要就着重讲解方法
        //1.WWWForm：构造函数
        WWWForm data = new WWWForm();
        //2.AddBinaryData：添加二进制数据
        //data.AddBinaryData()
        //3.AddField：添加字段
        //data.AddField()
        #endregion

        #region 知识点三 WWW结合WWWFrom对象来异步上传数据
        StartCoroutine(UpLoadData());
        #endregion

        #region 总结
        //WWW结合WWWFrom上传数据
        //需要配合后端服务器来指定上传规则
        //也就是说我们上传的数据，后端需要知道收到数据后应该如何处理
        //通过这种方式我们没办法像C#类当中完成文件的上传
        //但是该方式非常适合用于制作短连接游戏的前端网络层
        //我们可以对WWW进行二次封装，专门用于上传自定义消息给对应的Web服务器
        #endregion
    }

    IEnumerator UpLoadData()
    {
        WWWForm data = new WWWForm();
        //上传的数据 对应的后端程序 必须要有处理的规则 才能生效
        data.AddField("Name", "MrTang", Encoding.UTF8);
        data.AddField("Age", 99);
        data.AddBinaryData("file", File.ReadAllBytes(Application.streamingAssetsPath + "/test.png"), "testtest.png", "application/octet-stream");

        WWW www = new WWW("http://192.168.50.109:8000/Http_Server/", data);

        yield return www;

        if (www.error == null)
        {
            print("上传成功");
            //www.bytes
        }
        else
            print("上传失败" + www.error);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
