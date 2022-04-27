using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();
        projectileRigidBody.AddForce(transform.forward * projectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
