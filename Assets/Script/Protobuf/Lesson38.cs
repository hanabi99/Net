using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson38 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ʲô��Protobuf
        //Protobufȫ���� protocol-buffers��Э�黺������
        //�ǹȸ��ṩ�������ߵ�һ����Դ��Э�����ɹ���
        //������Ҫ����ԭ�������֮ǰ�����Զ���Э�鹤������
        //ֻ���������ӵ����ƣ����Ի���Э�������ļ�����
        //C++��Java��C#��Objective-C��PHP��Python��Ruby��Go
        //�ȵ����ԵĴ����ļ�

        //������ҵ��Ϸ�����г�����ѡ���Э�����ɹ���
        //�кܶ���Ϸ��˾ѡ������ΪЭ�鹤��������������Ϸ����
        //��Ϊ��ͨ����ǿ���ȶ��Ըߣ����Խ�Լ�������Զ���Э�鹤�ߵ�ʱ��

        //protocol-buffers����
        //https://developers.google.com/protocol-buffers
        #endregion

        #region ֪ʶ��� Protobuf��ʹ������
        //1.���ض�Ӧ����Ҫʹ��Protobuf�������
        //2.�������ù���༭Э�������ļ�
        //3.��Protobuf������������Э�������ļ����ɶ�Ӧ���ԵĴ����ļ�
        //4.�������ļ����빤���н���ʹ��
        #endregion

        #region ֪ʶ���� ����Protobuf������ݡ���׼��DLL�ļ�
        //1.�ڹ�����ǰ�����ص�ַ
        //  protocol-buffers����
        //  https://developers.google.com/protocol-buffers
        //2.����protobuf-csharp
        //3.��ѹ���csharp\src�е�Google.Protobuf.sln
        //4.ѡ��Google.Protobuf�Ҽ����� dll�ļ�
        //5.��csharp\src\Google.Protobuf\bin\Debug·�����ҵ���Ӧ.net�汾��Dll�ļ�������ʹ��4.5���ɣ�
        //6.��net45�е�dll�ļ����뵽Unity�����е�Plugins����ļ�����
        #endregion

        #region ֪ʶ���� ����Protobuf������ݡ���׼��������
        //1.�ڹ�����ǰ�����ص�ַ
        //  protocol-buffers����
        //  https://developers.google.com/protocol-buffers
        //2.����protoc-�汾-win32����64�����ݲ���ϵͳ������
        //3.��ѹ���ȡbin�ļ����е�protoc.exe��ִ���ļ���
        //  �ɽ������Unity�����У�����֮���ʹ�ã���Ҳ���Բ�����Unity���̣���ס����·�����ɣ�
        #endregion

        #region �ܽ�
        //Protobufȫ��protocol-buffers
        //�ǹȸ��ṩ�������ߵĿ�ԴЭ�����ɹ���

        //����Ҫʹ������Ҫ׼������
        //1.���ض�ӦCsharp�汾������DLL���ļ����빤���У�֮��Ļ��࣬���л������л�������DLL����д�õ����ݣ�
        //2.���ض�Ӧ����ϵͳ��protoc������������֮�����ɴ����ļ���֮����������ļ����ɴ��붼��ͨ����Ӧ�ó���
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
