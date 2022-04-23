using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    bool isOnField;

    public float bulletSpeed = 1.0f;

    [SerializeField] float timeTillDisable = 5.0f;
    private float timeTillDisableCurrent = 5.0f;

    [SerializeField] float timeTillDestroy = 4.0f;
    private float timeTillDestroyCurrent;

    public float bulletDamage;

    public RangedEnemy sourceUnit;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameObject.name = "Spawned Bullet";
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        StateUpdate();
    }

    public void StateUpdate()
    {
        ProjectileState(true);
        timeTillDisableCurrent = timeTillDisable;
        timeTillDestroyCurrent = timeTillDestroy;

        rigidBody.AddForce(transform.forward * bulletSpeed);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        //Timer Until Disabled
        if (isOnField) timeTillDisableCurrent -= Time.deltaTime;
        if (timeTillDisableCurrent <= 0) ProjectileState(false);


        //Timer Until Destroyed
        if (!isOnField) timeTillDestroyCurrent -= Time.deltaTime;
        if (timeTillDestroyCurrent <= 0) DestroyProjectile();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != sourceUnit)
        {
            ProjectileState(false);
        }
    }

    private void DestroyProjectile()
    {
        if (sourceUnit != null) sourceUnit.recycleBullets.Remove(this);
        Destroy(gameObject);
    }

    private void ProjectileState(bool state)
    {
        if (!state)
        {
            gameObject.name = "Recyclable Bullet";
            if (sourceUnit != null)
            {
                sourceUnit.recycleBullets.Add(this);
            }
            else
            {
                DestroyProjectile();
            }
        }
        isOnField = state;
        meshRenderer.enabled = state;
        boxCollider.enabled = state;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
