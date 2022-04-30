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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Unit hitUnit = other.gameObject.GetComponent<Unit>();
        if (hitUnit != null && hitUnit != projectileOwner)
        {
            hitUnit.TakeDamage(projectileDamage);
            if (isDestroyOnHit) DestroyProjectile();
        }
    }
}
