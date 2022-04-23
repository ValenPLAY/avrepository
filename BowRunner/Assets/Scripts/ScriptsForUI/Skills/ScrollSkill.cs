using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrollSkill : MonoBehaviour
{
    public GameObject[] prefabSkill;
    public Transform parentLeft;
    public Transform parentMiddle;
    public Transform parentRight;
    private GameObject obj;
    public static Action<SkillsEvents> OnSpawnSkill;
    private void Start()
    {
        SpawnSkill();
        SpawnSkill();
        SpawnSkill();
    }
   
    public void SpawnSkill()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
               SpawnSkillButton(prefabSkill[0]);
               break;
            case 1:
                SpawnSkillButton(prefabSkill[1]);
                break;
            case 2:
                SpawnSkillButton(prefabSkill[2]);
                break;
            case 3:
                SpawnSkillButton(prefabSkill[3]);
                break;
        }
        
    }

    private void SpawnSkillButton(GameObject skillButtonObject)
    {
        obj = Instantiate(skillButtonObject, GetParentForInstantiate());
        obj.transform.localScale = Vector3.one;
        var skillButton = obj.GetComponent<SkillsEvents>();
        skillButton.onButtonClickAction += DisableScrollSkill;
        OnSpawnSkill?.Invoke(skillButton);
    }
    
    private void DisableScrollSkill(GameObject obj)
    {
        Destroy(gameObject);
    }

    private Transform GetParentForInstantiate()
    {
        if (parentLeft.childCount == 0)
        {
            return parentLeft;
        }

        if (parentMiddle.childCount == 0)
        {
            return parentMiddle;
        }

        if (parentRight.childCount == 0)
        {
            return parentRight;
        }

        return parentRight;
    }
}
