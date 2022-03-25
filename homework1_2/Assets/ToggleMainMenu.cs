using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMainMenu : MonoBehaviour
{
    public TMP_Text textChangable;
    public string toggleString;
    private Toggle toggleComp;
    // Start is called before the first frame update
    void Start()
    {
        toggleComp = GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener(changeInfo);
    }

    void changeInfo (bool value)
    {
        if (textChangable!=null)
        {
            textChangable.text = toggleString;
        } else
        {
            Debug.LogError("ERR: Changable Text is not setup.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
