using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPopupEnabler : MonoBehaviour
{
    private Button button;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UIPanelSwitch);
    }

    void UIPanelSwitch ()
    {
        panel.SetActive(!panel.activeSelf);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
