using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Description")]
    public string unitName;
    public string unitDescription;

    [Header("Statistics")]
    public float health;
    protected float currentHealth;
    public float damage;
    public float attackSpeed;

    [Header("Movement")]
    protected Vector3 movementVector;
    public float movementSpeed;
    protected float actualMovementSpeed;

    protected float gravity = -9.8f;



    protected virtual void Awake()
    {
        movementVector.y = gravity;
    }

    void Start()
    {

    }

    protected virtual void Update()
    {


    }
}
