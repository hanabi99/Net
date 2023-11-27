using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HttpMrg
{
    private static HttpMrg instance => new HttpMrg();

    public static HttpMrg Instance => instance;

    public readonly string httppath = "http://192.168.1.2:8000/HTTPRoot/";

    public readonly string USER_NAME = "THC333";

    public readonly string PASS_WORD = "123123";

    /// <summary>
    /// 异步下载HttpWeb文件
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="httppath"></param>
    /// <param name="callback"></param>
    public async void HttpDownLoadAsync(string filename, string localpath, Action<HttpStatusCode> callback)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        await Task.Run(() =>
        {
            try
            {//检测资源可用性
                HttpWebRequest req1 = HttpWebRequest.Create(new Uri(httppath + filename)) as HttpWebRequest;

                req1.Method = WebRequestMethods.Http.Head;

                //设置请求类型  设置超时
                req1.Timeout = 2000;

                HttpWebResponse response = req1.GetResponse() as HttpWebResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.Close();
                    HttpWebRequest req2 = HttpWebRequest.Create(new Uri(httppath + filename)) as HttpWebRequest;

                    req2.Method = WebRequestMethods.Http.Get;

                    //设置请求类型  设置超时
                    req2.Timeout = 2000;

                    HttpWebResponse response2 = req2.GetResponse() as HttpWebResponse;

                    if (response2.StatusCode == HttpStatusCode.OK)
                    {
                        using (FileStream fileStream = File.Create(localpath))
                        {
                            byte[] bytes = new byte[2048];

                            Stream stream = response2.GetResponseStream();

                            int contentLen = stream.Read(bytes, 0, bytes.Length);
                            while (contentLen != 0)
                            {
                                fileStream.Write(bytes, 0, contentLen);
                                contentLen = stream.Read(bytes, 0, bytes.Length);
                            }
                            fileStream.Close();
                            stream.Close();
                            response2.Close();
                        }
                        httpStatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        httpStatusCode = response.StatusCode;
                    }
                }
                else
                {
                    httpStatusCode = response.StatusCode;
                }
            }
            catch (WebException e)
            {
                Debug.Log("下载出错");
            }
        });

        callback?.Invoke(httpStatusCode);
    }


    /// <summary>
    /// 异步上传HttpWeb文件
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="localpath"></param>
    /// <param name="callback"></param>
    public async void HttpUpLoadAsync(string filename, string localpath, Action<HttpStatusCode> callback)
    {
        HttpStatusCode httpStatus = HttpStatusCode.OK;
        await Task.Run(() =>
        {
            try
            {
                HttpWebRequest req = HttpWebRequest.Create(httppath) as HttpWebRequest;
                req.Method = WebRequestMethods.Http.Post;
                req.ContentType = "multipart/form-data;boundary=MrTHC";
                req.Timeout = 500000;
                req.Credentials = new NetworkCredential(USER_NAME, PASS_WORD);
                req.PreAuthenticate = true;//先验证身份 再上传数据

                string head = "--MrTHC\r\n" +
               "Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\n" +
               "Content-Type:application/octet-stream\r\n\r\n";

                head = string.Format(head, filename);
                //头部拼接字符串规则信息的字节数组
                byte[] headBytes = Encoding.UTF8.GetBytes(head);

                //3-2.结束的边界信息
                //  --边界字符串--
                byte[] endBytes = Encoding.UTF8.GetBytes("\r\n--MrThc--\r\n");

                //4.写入上传流
                using (FileStream localFileStream = File.OpenRead(localpath))
                {
                    //4-1.设置上传长度
                    //总长度 是前部分字符串 + 文件本身有多大 + 后部分边界字符串
                    req.ContentLength = headBytes.Length + localFileStream.Length + endBytes.Length;
                    //用于上传的流
                    Stream upLoadStream = req.GetRequestStream();
                    //4-2.先写入前部分头部信息
                    upLoadStream.Write(headBytes, 0, headBytes.Length);
                    //4-3.再写入文件数据
                    byte[] bytes = new byte[4096];
                    int contentLength = localFileStream.Read(bytes, 0, bytes.Length);
                    while (contentLength != 0)
                    {
                        upLoadStream.Write(bytes, 0, contentLength);
                        contentLength = localFileStream.Read(bytes, 0, bytes.Length);
                    }
                    //4-4.在写入结束的边界信息
                    upLoadStream.Write(endBytes, 0, endBytes.Length);

                    upLoadStream.Close();
                    localFileStream.Close();
           
                }
                //5.上传数据，获取响应
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                httpStatus = res.StatusCode;

            }
            catch (WebException w)
            {
                Debug.LogError(w);
            }            
        });

        callback?.Invoke(httpStatus);

    }
}
