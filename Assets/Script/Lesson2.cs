using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Threading.Tasks;

public class Lesson2 : MonoBehaviour
{

    private void Start()
    {
        #region ֪ʶ��һ ʲô������������
        //��������Ҳ������ָ�򡢷��������á����������Լ�����IP�Ǽǵȵ�
        //˵�ü򵥵���ǽ��üǵ�����������IP
        //IP��ַ�������ϱ�ʶվ������ֵ�ַ������IP��ַ�����˵��������
        //����Ϊ�˷�����䣬��������������IP��ַ��ʶվ���ַ��
        //���� ����Ҫ��¼һ����ҳ www.baidu.com ����������� ���ǿ���ͨ����������������һ��Զ�˷������ĵ�ַ�������Ǽ�¼һ�����ӵ�IP��ַ

        //������������������IP��ַ��ת�����̡������Ľ���������DNS���������
        //�����ڽ���ͨ��ʱ��ʱ��������ͨ��������ȡIP
        //������ڿ����Ǿ���ѧϰC#�ṩ������������ص���

        //����ϵͳ��Ӣ�ģ�Domain Name System����д��DNS���ǻ�������һ�����
        //����Ϊ��������IP��ַ�໥ӳ���һ���ֲ�ʽ���ݿ⣬�ܹ�ʹ�˸�����ط��ʻ�����
        //���������Ͻ�����ϻ���������һ��ϵͳ����ΪIP��ַ���䲻���㣬�Ͳ���������ϵͳ���������ֺ�IP�Ķ�Ӧ��ϵ
        #endregion

        #region ֪ʶ��� IPHostEntry��
        //�����ռ䣺System.Net
        //������IPHostEntry
        //��Ҫ���ã�����������ķ���ֵ ����ͨ���ö����ȡIP��ַ���������ȵ���Ϣ
        //���಻���Լ�������������ΪĳЩ�����ķ���ֵ������Ϣ��������Ҫͨ����������ȡ���ص���Ϣ

        //��ȡ����IP       ��Ա����:AddressList
        //��ȡ���������б�  ��Ա����:Aliases
        //��ȡDNS����      ��Ա����:HostName
        #endregion
        #region ֪ʶ���� Dns��
        //�����ռ䣺System.Net
        //������Dns
        //��Ҫ���ã�Dns��һ����̬�࣬�ṩ�˺ܶྲ̬����������ʹ����������������ȡIP��ַ

        //���÷���
        //1.��ȡ����ϵͳ��������
        print(Dns.GetHostName());

        //2.��ȡָ��������IP��Ϣ
        //����������ȡ
        //ͬ����ȡ
        //ע�⣺���ڻ�ȡԶ��������Ϣ����Ҫ������·ͨ�ţ����Կ��ܻ��������߳�
        //IPHostEntry entry = Dns.GetHostEntry("www.baidu.com");
        //for (int i = 0; i < entry.AddressList.Length; i++)
        //{
        //    print("IP��ַ��" + entry.AddressList[i]);
        //}
        //for (int i = 0; i < entry.Aliases.Length; i++)
        //{
        //    print("��������" + entry.Aliases[i]);
        //}
        //print("DNS����������" + entry.HostName);


        //�첽��ȡ
        GetHostEntry();
        #endregion

        #region �ܽ�
        //����㲻֪���Է���IP��ַ����ͨ�������ͶԷ�����ͨ��
        //���ǿ���ͨ��Dns��ͨ�������õ�IP��ַ���ٺͶԷ��������Ӳ�ͨ��
        #endregion
    }


    private async void GetHostEntry()
    {
        Task<IPHostEntry> task = Dns.GetHostEntryAsync("www.baidu.com");
        await task;
        for (int i = 0; i < task.Result.AddressList.Length; i++)
        {
            print("IP��ַ��" + task.Result.AddressList[i]);
        }
        for (int i = 0; i < task.Result.Aliases.Length; i++)
        {
            print("��������" + task.Result.Aliases[i]);
        }
        print("DNS����������" + task.Result.HostName);
    }
}

