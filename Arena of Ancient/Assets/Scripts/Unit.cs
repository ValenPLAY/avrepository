using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Description")]
    public string unitName = "Unnamed Unit";
    public string unitDescription = "Unnamed Desc";

    [Header("Statistics")]
    public float health = 10.0f;
    protected float currentHealth;
    public float damage = 1.0f;
    public float attackSpeed = 1.0f;
    public float movementSpeed = 5.0f;

    [Header("Components")]
    protected BoxCollider unitColliderBox;
    protected CapsuleCollider unitColliderCapsule;
    



    protected virtual void Awake()
    {
        unitColliderBox = GetComponent<BoxCollider>();
        unitColliderCapsule = GetComponent<CapsuleCollider>();
        
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Update()
    {


    }

    public virtual void TakeDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        //Destroy(gameObject);
        Debug.Log(unitName + " has fallen.");
    }
        

}
