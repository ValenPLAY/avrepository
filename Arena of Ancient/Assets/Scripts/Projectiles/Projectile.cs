using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Info")]
    public Unit projectileOwner;


    public float projectileDamage = 0.0f;
    public float projectileSpeed = 10.0f;

    public float projectileTimeToDestroy = 3.0f;
    protected float projectileTimeToDestroyCurrent;

    protected BoxCollider projectileCollider;
    protected Rigidbody projectileRigidBody;

    [SerializeField] protected bool isDestroyOnHit;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        projectileCollider = GetComponent<BoxCollider>();
        projectileRigidBody = GetComponent<Rigidbody>();

        projectileTimeToDestroyCurrent = projectileTimeToDestroy;
        Physics.IgnoreCollision(projectileCollider, projectileOwner.GetComponent<CharacterController>());
        //projectileRigidBody.AddForce(transform.forward * projectileSpeed);
    }



    protected virtual void OnEnable()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name + " - " + collision.collider);
        if (isDestroyOnHit) DestroyProjectile();
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

    }
}
