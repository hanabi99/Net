using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatMsgHandler : BaseHandler
{
    public override void HandlerMsg()
    {
        HeartMsg Msg = message as HeartMsg;
        Debug.Log(Msg.GetID() + "ÐÄÌø¼ì²â");
    }

    
}
