using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject currentPanel;
    public List<GameObject> panels = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int selectedPanel = 0; selectedPanel < panels.Count; selectedPanel++)
        {
            panels[selectedPanel].SetActive(false);
            if (selectedPanel == 0) panels[selectedPanel].SetActive(true);

        }
        currentPanel = panels[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PanelChange(int openedPanelID)
    {
        if (openedPanelID < panels.Count && panels[openedPanelID] != null)
        {
            currentPanel.SetActive(false);
            panels[openedPanelID].SetActive(true);
            currentPanel = panels[openedPanelID];
        }


    }
}
