using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMsgHandler : BaseHandler
{
    public override void HandlerMsg()
    {
        PlayerMsg playerMsg = message as PlayerMsg;
        Debug.Log(playerMsg.playerID);
    }
}
