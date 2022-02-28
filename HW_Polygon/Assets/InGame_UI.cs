using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    public GameObject mainCharacter;
    public Text wpnName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var characterScript = mainCharacter.GetComponent<PlayerCharacter>();
        if (characterScript.currentWeaponObject) { 
        var curWeapon = characterScript.currentWeaponObject.GetComponent<Weapon>();
        if (curWeapon)
        {
            wpnName.text = curWeapon.weaponName;
        }
        }
        //
    }
}
