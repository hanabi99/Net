using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgPool : MonoBehaviour
{
    //´æ´¢ÏûÏ¢µÄÈÝÆ÷
    private Dictionary<int, Type> messages = new Dictionary<int, Type>();
    //´æ´¢handlerµÄÈÝÆ÷
    private Dictionary<int, Type> handlers = new Dictionary<int, Type>();

    public MsgPool()
    {
        Register(1001,typeof(PlayerMsg), typeof(PlayerMsgHandler));
        Register(1003, typeof(HeartMsg), typeof(PlayerMsgHandler));
    }


    public void Register(int id, Type messageType,Type handlerType)
    {
        messages.Add(id, messageType);
        handlers.Add(id, handlerType);
    }

    public BaseMsg GetMessage(int id)
    {
        if (!messages.ContainsKey(id))
        {
            return null;
        }
        return Activator.CreateInstance(messages[id]) as BaseMsg;
    }

    public BaseHandler GetHandler(int id)
    {
        if (!handlers.ContainsKey(id))
        {
            return null;
        }
        return Activator.CreateInstance(handlers[id]) as BaseHandler;
    }

}
