using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson39 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �ع��Զ���Э�����ɹ����е������ļ�
        //�������Զ���Э�����ù������֪ʶ����
        //ʹ�õ���xml�ļ���������
        //����ֻ��Ҫ����xml�Ĺ��� 
        //����һ����������Э����Ϣ
        //֮���ȡxml�����������ɴ����ļ�

        //��Protobuf��ԭ����һ����
        //ֻ����Protobuf�����Լ������ù���
        //Ҳ�Զ����˶�Ӧ�������ļ���׺��ʽ
        #endregion

        #region ֪ʶ��� ���ú�׺
        //Protobuf�������ļ��ĺ�׺ͳһʹ��
        //.proto

        //����ͨ�������׺Ϊ.proto�������ļ���������
        #endregion

        #region ֪ʶ���� ���ù���

        #region ����1 ע�ͷ�ʽ
        //��ʽ1
        /*��ʽ2*/
        #endregion

        #region ����2 ��һ�а汾��
        //syntax = "proto3";
        //�����д Ĭ��ʹ��proto2
        #endregion

        #region ����3 �����ռ�
        //package �����ռ���;
        #endregion

        #region ����4 ��Ϣ��
        //message ����{
        //�ֶ�����
        //}
        #endregion

        #region ����5 ��Ա���ͺ� Ψһ���
        //������:
        //float��double
        //����:
        //�䳤����-int32,int64,uint32,uint64,
        //�̶��ֽ���-fixed32,fixed64,sfixed32,sfixed64
        //��������:
        //bool,string,bytes

        //Ψһ��� ���ó�Աʱ ��ҪĬ�ϸ�����һ����� ��1��ʼ
        //��Щ������ڱ�ʶ�е��ֶ���Ϣ�����Ƹ�ʽ

        #endregion

        #region ����6 �����ʶ
        //1:required ���븳ֵ���ֶ�
        //2:optional ���Բ���ֵ���ֶ�
        //3:repeated ����
        #endregion

        #region ����7 ö��
        //enum ö����{
        //    ����1 = 0;//��һ����������ӳ�䵽0
        //    ����2 = 1;
        //}
        #endregion

        #region ����8 Ĭ��ֵ
        //string-���ַ���
        //bytes-���ֽ�
        //bool-false
        //��ֵ-0
        //ö��-0
        //message-ȡ�������� C#Ϊ��
        #endregion

        #region ����9 ����Ƕ��

        #endregion

        #region ����10 �����ֶ�
        //����޸���Э����� ɾ���˲�������
        //Ϊ�˱������ʱ ����ʹ�� �Ѿ�ɾ���˵ı��
        //���ǿ������� reserved �ؼ����������ֶ�
        //��Щ���ݾͲ����ٱ�ʹ����
        //message Foo {
        //    reserved 2, 15, 9 to 11;
        //    reserved "foo", "bar";
        //}
        #endregion

        #region ����11 ���붨��
        //import "�����ļ�·��";
        //�������ĳһ�������� ʹ������һ�����õ�����
        //����Ҫ������һ�������ļ���
        #endregion

        #endregion

        #region �ܽ�
        //������Ҫ����Protobuf�����ù���
        //֮�����ʹ�ù��߽���תΪC#�ű��ļ�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
