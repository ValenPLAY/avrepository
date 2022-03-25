using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update

    public float hazardDamage = 1.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Character collidedObject = collision.gameObject.GetComponent<Character>();

        if (collidedObject != null)
        {
            collidedObject.Hit(hazardDamage);
        }
    }
}

