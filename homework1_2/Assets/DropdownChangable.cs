using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownChangable : MonoBehaviour
{
    public TMP_Text textField;
    private TMP_Dropdown dropdownList;
    // Start is called before the first frame update
    void Start()
    {
        
        dropdownList = GetComponent<TMP_Dropdown>();
        dropdownList.onValueChanged.AddListener(ChangeText);
    }

    void ChangeText (int changedText)
    {
        if (dropdownList.value != 0) { 
        textField.text = dropdownList.value.ToString();
        } else
        {
            textField.text = "None";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
