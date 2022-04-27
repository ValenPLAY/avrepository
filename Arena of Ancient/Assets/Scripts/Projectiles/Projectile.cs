using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header ("Projectile Info")]
    public Unit projectileOwner;


    public float projectileDamage = 0.0f;
    public float projectileSpeed = 10.0f;

    public float projectileTimeToDestroy = 3.0f;
    protected float projectileTimeToDestroyCurrent;

    protected BoxCollider projectileCollider;
    protected Rigidbody projectileRigidBody;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        projectileCollider = GetComponent<BoxCollider>();
        projectileRigidBody = GetComponent<Rigidbody>();

        projectileTimeToDestroyCurrent = projectileTimeToDestroy;
        Physics.IgnoreCollision(projectileCollider, projectileOwner.GetComponent<CharacterController>());

    }

    

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name + " - " + collision.collider);
    }

    // Update is called once per frame
    void Update()
    {
        projectileTimeToDestroyCurrent -= Time.deltaTime;
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Unit hitUnit = other.gameObject.GetComponent<Unit>();
        if (hitUnit != null && hitUnit != projectileOwner)
        {
            hitUnit.TakeDamage(projectileDamage);
        }
    }
}
