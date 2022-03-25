using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public bool isScaleAffectStrength = true;
    public float explosionStrength;
    public float explosionDamage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Vector3.Distance(other.transform.position, transform.position));
        if (isScaleAffectStrength)
        {
            explosionStrength += transform.localScale.x * 25f;
        }
        var colliderRigid = other.GetComponent<Rigidbody>();
        if (colliderRigid)
        {
            var explosionDirection = colliderRigid.position - transform.position;
            var explosionVector = new Vector3();
            explosionVector = ((explosionDirection) * explosionStrength)/Vector3.Distance(colliderRigid.position,transform.position);
            colliderRigid.AddForce(explosionVector);
        }
        var destructible = other.GetComponent<Destructible>();
        if (destructible)
        {
            destructible.Hit(explosionDamage);
        }
    }
}
