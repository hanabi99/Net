using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ����Ϣ��Ӧһ��������
/// </summary>
public abstract class BaseHandler 
{
    /// <summary>
    /// �����ĸ���Ϣ
    /// </summary>
    public BaseMsg message;

    /// <summary>
    /// ������Ϣ����
    /// </summary>
    public abstract void HandlerMsg();
}
