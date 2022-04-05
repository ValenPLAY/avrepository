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
    public string interactMessage;
    public TextMeshProUGUI interactMessageField;

    void Awake()
    {
        currentState = interactStates.Nothing;
        if (interactMessageField == null)
        {
            interactMessageField = GameObject.FindGameObjectWithTag("Interact Message").GetComponent<TextMeshProUGUI>();
        }
    }

    public void ShowInfo()
    {

    }
}
