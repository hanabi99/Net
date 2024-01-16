using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownLoadHandlerMsg : DownloadHandlerScript
{
    //����������Ҫ����Ϣ����
    private BaseMsg msg;
    //����װ���յ����ֽ������
    private byte[] cacheBytes;
    private int index = 0;
    public DownLoadHandlerMsg():base()
    {
    }

    public T GetMsg<T>() where T:BaseMsg
    {
        return msg as T;
    }

    protected override byte[] GetData()
    {
        return cacheBytes;
    }

    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        //���յ������� �������������� �����һ����
        data.CopyTo(cacheBytes, index);
        index += dataLength;
        return true;
    }

    protected override void ReceiveContentLengthHeader(ulong contentLength)
    {
        cacheBytes = new byte[contentLength];
    }

    protected override void CompleteContent()
    {
        //Ĭ�Ϸ������·����Ǽ̳�BaseMsg����Ϣ ��ô���������ʱ������
        index = 0;
        int msgID = BitConverter.ToInt32(cacheBytes, index);
        index += 4;
        int msgLength = BitConverter.ToInt32(cacheBytes, index);
        index += 4;
        switch (msgID)
        {
            case 1001:
                msg = new PlayerMsg();
                msg.Reading(cacheBytes, index);
                break;
        }
        if (msg == null)
            Debug.Log("��ӦID" + msgID + "û�д���");
        else
            Debug.Log("��Ϣ�������");
    }
}
