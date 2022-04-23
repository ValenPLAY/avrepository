using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChooseSkill : MonoBehaviour
{
    public GameObject imageSkill;

    public Transform parentImageSkill;

    public void ChooseSkill ()
    {
        GameObject imagSkill = Instantiate(imageSkill);
        imagSkill.transform.SetParent(parentImageSkill);
      
    }



}
