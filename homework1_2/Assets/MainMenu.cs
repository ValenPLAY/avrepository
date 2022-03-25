using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> listPanels;
    public int mainPanelID = 0;
    static public int activePanelID = 0;
    public GameObject backButton;
    //public GameObject panelNameObject;
    //public TMP_Text textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        pageUpdate(mainPanelID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pageUpdate(int selectedPanel) {
        //textMeshPro.text = "123";
        var soundeffect = GetComponent<AudioSource>();
        soundeffect.Play();
        listPanels.ForEach(x => {
            if (listPanels.IndexOf(x) == selectedPanel)
            {
                x.SetActive(true);
            }
            else
            {
                x.SetActive(false);
            }
        });
        if (selectedPanel != mainPanelID)
        {
            backButton.SetActive(true);
        } else
        {
            backButton.SetActive(false);
            
        }
    }
}
