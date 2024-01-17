using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;

public class ProtoBuffTool : MonoBehaviour
{
    //Э�������ļ���·��
    private static string POROTO_PATH = "F:\\Net\\Protobuf\\Proto";
    //Э�����ɿ�ִ���ļ���·��
    private static string POROTOC_PATH = "F:\\Net\\Protobuf\\protoc.exe";
    //����C# �ļ�·��
    private static string CSHARP_PATH = "F:\\Net\\Protobuf\\Csharp";
    //����cpp �ļ�·��
    private static string CPP_PATH = "F:\\Net\\Protobuf\\cpp";
    //����java �ļ�·��
    private static string JAVA_PATH = "F:\\Net\\Protobuf\\java";

    [MenuItem("PorotoBuff/����C#����")]
    public static void GenerateCsharp()
    {
        GenerateAll(CSHARP_PATH, "csharp");
    }
    [MenuItem("PorotoBuff/����CPP����")]
    public static void GenerateCPP()
    {
        GenerateAll(CPP_PATH, "cpp");
    }
    [MenuItem("PorotoBuff/����JAVA����")]
    public static void GenerateJAVA()
    {
        GenerateAll(JAVA_PATH, "java");
    }

    public static void GenerateAll(string outPath,string outcmd)
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(POROTO_PATH);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        for (int i = 0; i < fileInfos.Length; i++)
        {
            if(fileInfos[i].Extension == ".proto")
            {
                //cmd
                Process progress = new Process();
                progress.StartInfo.FileName = POROTOC_PATH;
                progress.StartInfo.Arguments = $"-I={POROTO_PATH} --{outcmd}_out={outPath} {fileInfos[i]}";
                progress.Start();
                UnityEngine.Debug.Log(fileInfos[i] + "���ɽ���");
            }
        }
        UnityEngine.Debug.Log("����Э�����ɽ���");
    }
}
