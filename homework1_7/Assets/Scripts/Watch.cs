using TMPro;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public Camera playerCamera;
    [SerializeField] TextMeshPro coordinatesText;
    [SerializeField] int roundNumber = 100;
    [SerializeField] TextMeshPro noteDisplayTMP;

    private void OnEnable()
    {
        Debug.Log("ENABLED");
        if (noteDisplayTMP == null) noteDisplayTMP = GameObject.FindGameObjectWithTag("Note Display").GetComponent<TextMeshPro>();
        if (noteDisplayTMP != null)
        {
            MessageUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        coordinatesText.text = "x:" + Mathf.Round(transform.position.x * roundNumber) + " y:" + Mathf.Round(transform.position.y * roundNumber) + " z:" + Mathf.Round(transform.position.z * roundNumber);

        if (Input.mouseScrollDelta.y != 0)
        {

            MessageController.Instance.currentMessage += (int)Input.mouseScrollDelta.y;
            if (MessageController.Instance.currentMessage >= MessageController.Instance.playerMessages.Count)
            {
                MessageController.Instance.currentMessage = 0;

            }
            else if (MessageController.Instance.currentMessage < 0)
            {
                MessageController.Instance.currentMessage = MessageController.Instance.playerMessages.Count - 1;
            }
            MessageUpdate();
        }
    }

    void MessageUpdate()
    {
        if (MessageController.Instance.playerMessages.Count != 0)
        {
            noteDisplayTMP.text = MessageController.Instance.playerMessages[MessageController.Instance.currentMessage];
        }
        else
        {
            noteDisplayTMP.text = MessageController.Instance.messagesTextList[0];
        }

    }
}
