using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;

public class ProtoBuffTool : MonoBehaviour
{
    //协议配置文件的路径
    private static string POROTO_PATH = "F:\\Net\\Protobuf\\Proto";
    //协议生成可执行文件的路径
    private static string POROTOC_PATH = "F:\\Net\\Protobuf\\protoc.exe";
    //生成C# 文件路径
    private static string CSHARP_PATH = "F:\\Net\\Protobuf\\Csharp";
    //生成cpp 文件路径
    private static string CPP_PATH = "F:\\Net\\Protobuf\\cpp";
    //生成java 文件路径
    private static string JAVA_PATH = "F:\\Net\\Protobuf\\java";

    [MenuItem("PorotoBuff/生成C#代码")]
    public static void GenerateCsharp()
    {
        GenerateAll(CSHARP_PATH, "csharp");
    }
    [MenuItem("PorotoBuff/生成CPP代码")]
    public static void GenerateCPP()
    {
        GenerateAll(CPP_PATH, "cpp");
    }
    [MenuItem("PorotoBuff/生成JAVA代码")]
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
                UnityEngine.Debug.Log(fileInfos[i] + "生成结束");
            }
        }
        UnityEngine.Debug.Log("所有协议生成结束");
    }
}
