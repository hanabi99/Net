using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace GamePlayer
{
    public enum ENUM_NAME
    {
        MAIN = 1,
        OTHER,
    }

    public class PlayerData : BaseData
    {
        public int id;
        public float atk;
        public long lev;
        public int[] arrays;
        public List<int> list;
        public Dictionary<int, string> dic;

        public override int GetBytesNum()
        {
            throw new System.NotImplementedException();
        }

        public override int Reading(byte[] bytes, int beginIndex = 0)
        {
            throw new System.NotImplementedException();
        }

        public override byte[] Writing()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PlayerMsg:BaseMsg
    {
        public override int GetID()
        {
            return base.GetID();
        }
    }
}



public class Lesson35 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ѡ�����ָ�ʽ����Э�飿
        //1.xml
        //2.json
        //3.excel
        //4.�Զ���
        //�ȵ�
        //���ǿ��Ը����Լ���ϲ��ѡ��
        //ѡ�񷽱����õģ����õļ���
        //���õ���ҪĿ����ȷ��
        //��������Ա������

        //֮����ݶ�ȡ����Щ������Ϣ
        //��ͨ�����밴�չ����Զ����ɶ�Ӧ�����ļ�

        //�Ҹ���ϲ��ʹ��xml��ΪЭ�������ļ�
        //ѧ��xml���ã������ķ�ʽ���Ǵ�ͬС���
        //������Ҫ��ѧϰ����˼·������
        //�Ժ����Ŀ�У���Ҹ����Լ���ϲ��ѡ�񼴿�
        #endregion

        #region ֪ʶ��� ������xml����Ϊ�� ���� �ƶ����ù���
        //xml���֪ʶ�����������ݳ־û�֮xml�н���ѧϰ
        //1. ����xml�����ļ�
        //2. �ƶ����ù���
        //   1.ö�ٹ��� 
        //   2.���������
        //   3.��Ϣ�����
        #endregion

        #region ֪ʶ���� ��ȡ������Ϣ
        //1.��ȡxml�ļ���Ϣ
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.dataPath + "/Scripts/Lesson35_Э�飨��Ϣ������/Lesson35.xml");
        //2.��ȡ���ڵ�Ԫ��
        //2-1: ���ڵ��ȡ
        XmlNode root = xml.SelectSingleNode("messages");
        //2-2: ��ȡ������ö�ٽṹ��ڵ�
        XmlNodeList enumList = root.SelectNodes("enum");
        foreach (XmlNode enumNode in enumList)
        {
            print("**************");
            print("******ö��******");
            print("ö�����֣�" + enumNode.Attributes["name"].Value);
            print("ö�����������ռ䣺" + enumNode.Attributes["namespace"].Value);
            print("******ö�ٳ�Ա******");
            XmlNodeList fields = enumNode.SelectNodes("field");
            foreach (XmlNode field in fields)
            {
                string str = field.Attributes["name"].Value;
                if (field.InnerText != "")
                    str += " = " + field.InnerText;
                str += ",";
                print(str);
            }
        }
        //2-3: ��ȡ���������ݽṹ��ڵ�
        XmlNodeList dataList = root.SelectNodes("data");
        foreach (XmlNode data in dataList)
        {
            print("**************");
            print("******���ݽṹ��******");
            print("����������" + data.Attributes["name"].Value);
            print("���������������ռ䣺" + data.Attributes["namespace"].Value);
            print("******�������Ա******");
            XmlNodeList fields = data.SelectNodes("field");
            foreach (XmlNode field in fields)
            {
                print(field.Attributes["type"].Value + " " + field.Attributes["name"].Value + ";");
            }
        }
        //2-4: ��ȡ��������Ϣ�ڵ�
        XmlNodeList msgList = root.SelectNodes("message");
        foreach (XmlNode msg in msgList)
        {
            print("**************");
            print("******��Ϣ��******");
            print("��Ϣ������" + msg.Attributes["name"].Value);
            print("��Ϣ�����������ռ䣺" + msg.Attributes["namespace"].Value);
            print("��ϢID��" + msg.Attributes["id"].Value);
            print("******�������Ա******");
            XmlNodeList fields = msg.SelectNodes("field");
            foreach (XmlNode field in fields)
            {
                print(field.Attributes["type"].Value + " " + field.Attributes["name"].Value + ";");
            }
        }
        #endregion

        #region �ܽ�
        //���������ļ�������Ϣ�����ݽṹ��ö�ٵ�Ŀ��
        //1.���ٹ�����������һ�Σ�֮���Զ������ɸ������Զ�Ӧ�����ļ�
        //2.���ٹ�ͨ�ɱ�������ǰ������Բ�ͬʱ���ֶ�д�������ǰ��˲�ͳһ������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
