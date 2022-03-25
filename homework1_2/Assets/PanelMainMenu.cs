using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainMenu : MonoBehaviour
{
    public TMP_Text panelNameObject;
    public string panelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (panelName != null)
        {
            panelNameObject.text = panelName;
            //TextMeshPro panelNameTM = panelNameObject.GetComponent<TextMeshPro>();
            //panelNameTM.SetText("25");
        }
        else { 
        
        }
    }
}
