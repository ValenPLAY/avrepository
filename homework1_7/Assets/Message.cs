using UnityEngine;

public class Message : MonoBehaviour
{
    public int messageID = 0;


    public void MessagePickup()
    {

        MessageController.Instance.playerMessages.Add(MessageController.Instance.messagesTextList[messageID]);
        MessageController.Instance.currentMessage = MessageController.Instance.playerMessages.Count - 1;
        Destroy(gameObject);
    }
}
