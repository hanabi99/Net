using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using System;
using System.Reflection;

public static class NetTool 
{
    /// <summary>
    /// ���л�����Protobuf �ֽ�����
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static byte[] GetProtoBytes(IMessage msg )
    {
        return msg.ToByteArray();
    }

    /// <summary>
    /// �����л�����Protobuf ����
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static T GetProtoMsg<T>(byte[]  bytes) where T : class ,IMessage
    {
        Type type = typeof(T);
        PropertyInfo property = type.GetProperty("Parser");
        object propertyobj =property.GetValue(null, null);
        Type propertType = propertyobj.GetType();
        MethodInfo methodInfo = propertType.GetMethod("ParseFrom",new Type[] { typeof(byte[])} );
        object res = methodInfo.Invoke(propertyobj,new object[] { bytes } );
        return res as T;
    }
}
