using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public enum interactStates
    {
        Nothing,
        Pickup,
        Interact,
        Exit,
    };

    public interactStates currentState;

    [Header("UI settings")]
    public string interactMessage = "Press [E] to interact";
    public TextMeshProUGUI interactMessageField;

    [Header("Interactable Message List")]
    public List<string> interactableMessagesList = new List<string>(); 

    void Awake()
    {
        currentState = interactStates.Nothing;
        if (interactMessageField == null)
        {
            interactMessageField = GameObject.FindGameObjectWithTag("Interact Message").GetComponent<TextMeshProUGUI>();
        }
    }

    public void DisplayTip(bool check, int tipID)
    {


        interactMessageField.gameObject.SetActive(check);
        if (check) {
            if (tipID >= interactableMessagesList.Count)
            {
                interactMessage = interactableMessagesList[0];
            }
            else
            {
                interactMessage = interactableMessagesList[tipID];
            }
            interactMessageField.text = interactMessage;
        } 
    }
}
