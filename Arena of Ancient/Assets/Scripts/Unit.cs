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
    public float movementSpeed;




    protected virtual void Awake()
    {

    }

    void Start()
    {

    }

    protected virtual void Update()
    {


    }
}
