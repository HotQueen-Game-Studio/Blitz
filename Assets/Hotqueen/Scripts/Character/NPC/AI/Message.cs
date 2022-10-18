using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    private Character sender;
    private NPC receiver;
    public string msg { private set; get; }
    private Object extraInfo;

    public Message(Character sender, NPC receiver, string msg, Object extraInfo)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.msg = msg;
        this.extraInfo = extraInfo;
    }
}
