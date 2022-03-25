using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup : MonoBehaviour
{
    public GameObject pickedWeapon;
    private AudioSource snd;

    public void Start()
    {
        snd = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && pickedWeapon)
        {
            //if snd.
            snd.Play();
        }
    }
}
