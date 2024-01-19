using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个消息对应一个处理者
/// </summary>
public abstract class BaseHandler 
{
    /// <summary>
    /// 处理哪个消息
    /// </summary>
    public BaseMsg message;

    /// <summary>
    /// 处理消息方法
    /// </summary>
    public abstract void HandlerMsg();
}
