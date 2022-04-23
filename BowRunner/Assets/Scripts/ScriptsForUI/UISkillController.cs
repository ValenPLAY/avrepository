using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    //public List<SkillsEvents> skillButtons;
    public Transform parent;

    private void Awake()
    {
        ScrollSkill.OnSpawnSkill += SubscribeToSkillEvent;
        
    }

    private void OnDestroy()
    {
        ScrollSkill.OnSpawnSkill -= SubscribeToSkillEvent;
    }

    private void SubscribeToSkillEvent(SkillsEvents skillEventComponent)
    {
        skillEventComponent.onButtonClickAction += OnSkillButtonClick;
    }

    private void OnSkillButtonClick(GameObject skillObj)
    {
         var instantiatedObject =Instantiate(skillObj, parent, false);
         instantiatedObject.GetComponent<Button>().enabled = false;

    }
}

