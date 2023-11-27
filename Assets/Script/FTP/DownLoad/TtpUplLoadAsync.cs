using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class TtpUplLoadAsync : MonoBehaviour
{

    public static TtpUplLoadAsync instance => new TtpUplLoadAsync();

    public static TtpUplLoadAsync Instance => instance;

    //服务器地址
    private string FTP_PATH = "ftp://127.0.0.1/";

    private string USER_NANE = "tianhaochen";

    private string PASSWORD = "tian20010808";

    public async void UpLoadFile(string filename, string localPath, UnityAction action = null)
    {
        await Task.Run(() =>
        {

            try
            {
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + filename)) as FtpWebRequest;

                req.Credentials = new NetworkCredential(USER_NANE, PASSWORD);

                req.KeepAlive = false;

                req.UseBinary = true;

                req.Method = WebRequestMethods.Ftp.UploadFile;

                req.Proxy = null;

                //得到用于上传文件的流对象
                Stream upLoadStream = req.GetRequestStream();

                using (FileStream file = File.OpenRead(localPath))
                {
                    byte[] bytes = new byte[1024];

                    int contentCout = file.Read(bytes, 0, bytes.Length);

                    while (contentCout != 0)
                    {
                        upLoadStream.Write(bytes, 0, contentCout);

                        contentCout = file.Read(bytes, 0, bytes.Length);
                    }

                    file.Close();
                    upLoadStream.Close();
                }

                Debug.Log("上传完毕");
            }
            catch (Exception e)
            {
                Debug.Log("上传出错");
                Debug.LogError(e);

            }

            action?.Invoke();

        });

    }
    public async void DownLoadFile(string filename, string localPath, UnityAction action = null)
    {
        await Task.Run(() =>
        {

            try
            {
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + filename)) as FtpWebRequest;

                req.Credentials = new NetworkCredential(USER_NANE, PASSWORD);

                req.KeepAlive = false;

                req.UseBinary = true;

                req.Method = WebRequestMethods.Ftp.DownloadFile;

                req.Proxy = null;

                FtpWebResponse response = req.GetResponse() as FtpWebResponse;

                //得到用于上传文件的流对象
                Stream DownLoadStream = response.GetResponseStream();

                using (FileStream file = File.Create(Application.persistentDataPath + "/THC1122.png"))
                {
                    byte[] bytes = new byte[1024];

                    int contentCout = DownLoadStream.Read(bytes, 0, bytes.Length);

                    while (contentCout != 0)
                    {

                       file.Write(bytes, 0, bytes.Length);

                       contentCout =  DownLoadStream.Read(bytes, 0, contentCout);

                    
                    }

                    file.Close();
                    DownLoadStream.Close();
                }

                Debug.Log("上传完毕");
            }
            catch (Exception e)
            {
                Debug.Log("上传出错");
                Debug.LogError(e);

            }

            action?.Invoke();

        });
    }


}

