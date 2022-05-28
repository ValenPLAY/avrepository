using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Info")]
    public Unit projectileOwner;

    [Header("Projectile Stats")]
    public float projectileDamage = 0.0f;
    public float projectileSpeed = 10.0f;
    public float projectileTimeToDestroy = 3.0f;
    protected float projectileTimeToDestroyCurrent;
    [Header("Projectile Additions")]
    public ProjectileAddition onDeathEffectPrefab;
    [SerializeField] protected bool isDamageOnlyOnHit;
    [SerializeField] protected float additionIncreasedScale = 1.0f;


    protected BoxCollider projectileCollider;
    protected Rigidbody projectileRigidBody;

    [SerializeField] protected bool isDestroyOnHit;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
        projectileCollider = GetComponent<BoxCollider>();
        projectileRigidBody = GetComponent<Rigidbody>();

        projectileTimeToDestroyCurrent = projectileTimeToDestroy;

        //projectileRigidBody.AddForce(transform.forward * projectileSpeed);
    }



    protected virtual void OnEnable()
    {
        Physics.IgnoreCollision(projectileCollider, projectileOwner.GetComponent<CharacterController>());
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name + " - " + collision.collider);
        if (isDestroyOnHit) DestroyProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileTimeToDestroyCurrent > 0)
        {
            projectileTimeToDestroyCurrent -= Time.deltaTime;
        }
        else
        {
            DestroyProjectile();
        }

    }

    protected virtual void DestroyProjectile()
    {
        if (onDeathEffectPrefab != null)
        {
            ProjectileAddition createdEffect = Instantiate(onDeathEffectPrefab, gameObject.transform.position, Quaternion.identity);
            createdEffect.owner = projectileOwner;
            createdEffect.damage = projectileDamage;
            createdEffect.gameObject.transform.localScale *= additionIncreasedScale;
            createdEffect.gameObject.SetActive(true);
        }

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
