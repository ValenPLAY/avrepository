using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInstantiatePrefab : MonoBehaviour
{
    public GameObject panelWithSkill;
    public Transform canvas;
    
    
    public void InstantiatePrefab()
    {
        RectTransform pan = Instantiate(panelWithSkill, canvas,false).GetComponent<RectTransform>();
        pan.gameObject.SetActive(true);
    }
    
}
