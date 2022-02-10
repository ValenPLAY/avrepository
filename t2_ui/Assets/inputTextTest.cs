using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inputTextTest : MonoBehaviour
{
    public TMP_Text text;
    private TMP_InputField selectedInput;

    // Start is called before the first frame update
    void Start()
    {
        selectedInput = GetComponent<TMP_InputField>();
        selectedInput.onValueChanged.AddListener(changeText);
    }

    void changeText(string inputText) {
        text.text = inputText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
