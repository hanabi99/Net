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
        #region ֪ʶ��һ WWWFrom�������
        //�Ͻڿ�ѧϰ��ʹ��WWW������������
        //�����Ҫʹ��WWW�ϴ�����ʱ������Ҫ���WWWFrom�����ʹ����
        //��WWWFrom��Ҫ�������ڼ������ݵģ����ǿ��������ϴ��Ĳ�������2��������
        //�����WWWFrom�ϴ�����ʱ
        //����Ҫ�õ�������������Post
        //��ʹ��HttpЭ������ϴ�����

        //ע�⣺
        //ʹ��WWW���WWWFrom�ϴ�����һ����Ҫ��Ϻ�˳����ƶ��ϴ�����
        #endregion

        #region ֪ʶ��� WWWFrom��ĳ��÷����ͱ���
        //���൱��������Ҫ��ʹ�÷�������ر�������ʹ�ã�������Ҫ�����ؽ��ⷽ��
        //1.WWWForm�����캯��
        WWWForm data = new WWWForm();
        //2.AddBinaryData����Ӷ���������
        //data.AddBinaryData()
        //3.AddField������ֶ�
        //data.AddField()
        #endregion

        #region ֪ʶ���� WWW���WWWFrom�������첽�ϴ�����
        StartCoroutine(UpLoadData());
        #endregion

        #region �ܽ�
        //WWW���WWWFrom�ϴ�����
        //��Ҫ��Ϻ�˷�������ָ���ϴ�����
        //Ҳ����˵�����ϴ������ݣ������Ҫ֪���յ����ݺ�Ӧ����δ���
        //ͨ�����ַ�ʽ����û�취��C#�൱������ļ����ϴ�
        //���Ǹ÷�ʽ�ǳ��ʺ�����������������Ϸ��ǰ�������
        //���ǿ��Զ�WWW���ж��η�װ��ר�������ϴ��Զ�����Ϣ����Ӧ��Web������
        #endregion
    }

    IEnumerator UpLoadData()
    {
        WWWForm data = new WWWForm();
        //�ϴ������� ��Ӧ�ĺ�˳��� ����Ҫ�д���Ĺ��� ������Ч
        data.AddField("Name", "MrTang", Encoding.UTF8);
        data.AddField("Age", 99);
        data.AddBinaryData("file", File.ReadAllBytes(Application.streamingAssetsPath + "/test.png"), "testtest.png", "application/octet-stream");

        WWW www = new WWW("http://192.168.50.109:8000/Http_Server/", data);

        yield return www;

        if (www.error == null)
        {
            print("�ϴ��ɹ�");
            //www.bytes
        }
        else
            print("�ϴ�ʧ��" + www.error);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
