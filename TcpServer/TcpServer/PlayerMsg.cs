using System.Collections;
using System.Collections.Generic;


public class PlayerMsg : BaseMsg
{
    public int playerID;
    public PlayerData playerData;
    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        //��д��ϢID
        WriteInt(bytes, GetID(), ref index);
        //д�����Ϣ�ĳ�Ա����
        WriteInt(bytes, playerID, ref index);
        WriteData(bytes, playerData, ref index);
        return bytes;
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        //�����л�����Ҫȥ����ID ��Ϊ����һ��֮ǰ ��Ӧ�ð�ID�����л�����
        //�����жϵ���ʹ����һ���Զ�����������
        int index = beginIndex;
        playerID = ReadInt(bytes, ref index);
        playerData = ReadData<PlayerData>(bytes, ref index);
        return index - beginIndex;
    }

    public override int GetBytesNum()
    {
        return 4 + //��ϢID�ĳ���
             4 + //playerID���ֽ����鳤��
             playerData.GetBytesNum();//playerData���ֽ����鳤��
    }

    /// <summary>
    /// �Զ������ϢID ��Ҫ������������һ����Ϣ��
    /// </summary>
    /// <returns></returns>
    public override int GetID()
    {
        return 1001;
    }
}
