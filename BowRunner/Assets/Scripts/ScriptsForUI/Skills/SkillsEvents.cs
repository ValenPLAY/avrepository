using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsEvents : MonoBehaviour
{
    public Action<GameObject> onButtonClickAction = delegate { };
    
    public void UIActionHandler()
    {
        onButtonClickAction.Invoke(gameObject);
    }
}
