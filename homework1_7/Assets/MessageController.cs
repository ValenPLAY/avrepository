using System.Collections.Generic;
using UnityEngine;

public class MessageController : Singleton<MessageController>
{
    public static int currentlyDisplayedMessageID;
    public int currentMessage;
    [Header("Messages Archive")]
    [TextArea(10, 20)]
    public List<string> messagesTextList = new List<string>();
    [TextArea(10, 20)]
    public List<GameObject> specialMessagesList = new List<GameObject>();
    [Header("Picked up Messages")]
    public List<string> playerMessages = new List<string>();
}
