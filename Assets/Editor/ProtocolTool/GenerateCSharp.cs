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
    //Э�鱣��·��
    private string SAVE_PATH = Application.dataPath + "/Scripts/Protocol/";

    //����ö��
    public void GenerateEnum(XmlNodeList nodes)
    {
        //����ö�ٽű����߼�
        string namespaceStr = "";
        string enumNameStr = "";
        string fieldStr = "";

        foreach (XmlNode enumNode in nodes)
        {
            //��ȡ�����ռ�������Ϣ
            namespaceStr = enumNode.Attributes["namespace"].Value;
            //��ȡö����������Ϣ
            enumNameStr = enumNode.Attributes["name"].Value;
            //��ȡ���е��ֶνڵ� Ȼ������ַ���ƴ��
            XmlNodeList enumFields = enumNode.SelectNodes("field");
            //һ���µ�ö�� ��Ҫ���һ����һ��ƴ�ӵ��ֶ��ַ���
            fieldStr = "";
            foreach (XmlNode enumField in enumFields)
            {
                fieldStr += "\t\t" + enumField.Attributes["name"].Value;
                if (enumField.InnerText != "")
                    fieldStr += " = " + enumField.InnerText;
                fieldStr += ",\r\n";
            }
            //�����пɱ�����ݽ���ƴ��
            string enumStr = $"namespace {namespaceStr}\r\n" +
                             "{\r\n" +
                                $"\tpublic enum {enumNameStr}\r\n" +
                                "\t{\r\n" +
                                    $"{fieldStr}" +
                                "\t}\r\n" +
                             "}";
            //�����ļ���·��
            string path = SAVE_PATH + namespaceStr + "/Enum/";
            //�������������ļ��� �򴴽�
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //�ַ������� �洢Ϊö�ٽű��ļ�
            File.WriteAllText(path + enumNameStr + ".cs", enumStr);
        }

        Debug.Log("ö�����ɽ���");
    }

    //�������ݽṹ��

    //������Ϣ��
}
