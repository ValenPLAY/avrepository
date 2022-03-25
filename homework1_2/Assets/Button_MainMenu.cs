using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_MainMenu : MonoBehaviour
{
    public int openPanelID = 0;
    //public GameObject mainMenu;
    private Button currentBtn;
    private AudioSource soundeffect;

    // Start is called before the first frame update
    void Start()
    {
        currentBtn = GetComponent<Button>();
        soundeffect = GetComponent<AudioSource>();
        //currentBtn.onClick.AddListener(MainMenu.pageUpdate());
        currentBtn.onClick.AddListener(ButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClick()
    {
        //mainMenu.pageUpdate(1);
        //Debug.Log("Sound Effect Played!");
        //soundeffect.Play();
        //MainMenu.activePanelID = openPanelID;
        //mainMenu.pageUpdate(2);
        
        
    }
}
