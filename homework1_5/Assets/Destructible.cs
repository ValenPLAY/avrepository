using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float health = 1.0f;
    private AudioSource hitSound;
    // Start is called before the first frame update
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float damageInflicted)
    {
        hitSound.Play();
        health -= damageInflicted;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
