using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace GamePlayer//1
{
    public enum E_PLAYER_TYPE//2
    {
        
        //3
        MAIN = 1,
        OTHER,
    }
}

public class GenerateCSharp
{
    //协议保存路径
    private string SAVE_PATH = Application.dataPath + "/Scripts/Protocol/";

    //生成枚举
    public void GenerateEnum(XmlNodeList nodes)
    {
        //生成枚举脚本的逻辑
        string namespaceStr = "";
        string enumNameStr = "";
        string fieldStr = "";

        foreach (XmlNode enumNode in nodes)
        {
            //获取命名空间配置信息
            namespaceStr = enumNode.Attributes["namespace"].Value;
            //获取枚举名配置信息
            enumNameStr = enumNode.Attributes["name"].Value;
            //获取所有的字段节点 然后进行字符串拼接
            XmlNodeList enumFields = enumNode.SelectNodes("field");
            //一个新的枚举 需要清空一次上一次拼接的字段字符串
            fieldStr = "";
            foreach (XmlNode enumField in enumFields)
            {
                fieldStr += "\t\t" + enumField.Attributes["name"].Value;
                if (enumField.InnerText != "")
                    fieldStr += " = " + enumField.InnerText;
                fieldStr += ",\r\n";
            }
            //对所有可变的内容进行拼接
            string enumStr = $"namespace {namespaceStr}\r\n" +
                             "{\r\n" +
                                $"\tpublic enum {enumNameStr}\r\n" +
                                "\t{\r\n" +
                                    $"{fieldStr}" +
                                "\t}\r\n" +
                             "}";
            //保存文件的路径
            string path = SAVE_PATH + namespaceStr + "/Enum/";
            //如果不存在这个文件夹 则创建
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //字符串保存 存储为枚举脚本文件
            File.WriteAllText(path + enumNameStr + ".cs", enumStr);
        }

        Debug.Log("枚举生成结束");
    }

    //生成数据结构类

    //生成消息类
}
