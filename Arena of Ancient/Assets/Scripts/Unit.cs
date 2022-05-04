using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum state
    {
        normal,
        stunned,
        paused,
    }

    protected state unitState;

    [Header("Unit Description")]
    public string unitName = "Unnamed Unit";
    public string unitDescription = "Unnamed Desc";

    [Header("Unit Statistics")]
    [Header("Health")]
    public float health = 10.0f;
    public float healthBonus;
    protected float healthActual;

    protected float currentHealth;
    public float CurrentHealth 
    { 
        get => currentHealth; 

        protected set 
        { 
            currentHealth = value;
            onHealthChangedEvent?.Invoke(currentHealth, healthActual);
        } 
    }

    public float healthRegeneration = 0.0f;
    public float healthRegenerationBonus;
    protected float healthRegenerationActual;

    [Header("Armor")]
    public float armor;
    public float armorBonus;
    protected float armorActual;

    [Header("Damage")]
    public float damage = 1.0f;
    public float damageBonus;
    protected float damageActual;

    public float attackSpeed = 1.0f;
    public float attackSpeedBonus = 1.0f;
    private float attackSpeedActual;
    protected float attackCooldownCurrent;

    public float attackRange = 5.0f;



    [Header("MovementSpeed")]
    public float movementSpeed = 5.0f;
    public float movementSpeedBonus;
    protected float movementSpeedActual;


    [Header("Unit Components")]
    protected BoxCollider unitColliderBox;
    protected CapsuleCollider unitColliderCapsule;
    protected Animator unitAnimator;

    public Action<float, float> onHealthChangedEvent;


    protected virtual void Awake()
    {
        unitColliderBox = GetComponent<BoxCollider>();
        unitColliderCapsule = GetComponent<CapsuleCollider>();
        unitAnimator = GetComponent<Animator>();

        StatUpdate();

        currentHealth = healthActual;
        unitState = state.normal;
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Update()
    {
        if (currentHealth <= healthActual && healthRegenerationActual > 0) currentHealth += Time.deltaTime * healthRegenerationActual;
        if (attackCooldownCurrent > 0) attackCooldownCurrent -= Time.deltaTime;
    }

    public virtual void TakeDamage(float incomingDamage)
    {
        Debug.Log("Incoming Damage: " + incomingDamage);
        incomingDamage = Mathf.Clamp(incomingDamage - armorActual, 0, incomingDamage);
        /*incomingDamage -= armorActual;
        
        if (incomingDamage < 0) incomingDamage = 0;*/

        CurrentHealth -= incomingDamage;

        if (CurrentHealth <= 0)
        {
            Death();
        }

    }

    protected virtual void Death()
    {
        if (unitAnimator != null)
        {
            unitAnimator.SetTrigger("Death");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(unitName + " has fallen.");
        }

    }

    public void OrderAttack()
    {
        if (attackCooldownCurrent <= 0 && unitState == state.normal)
        {
            attackCooldownCurrent = attackSpeedActual;
            Attack();
        }
    }

    public void DealDamage(Unit target, float damageAmount)
    {
        target.TakeDamage(damageAmount);
    }

    public void DealDamage(Unit target)
    {
        target.TakeDamage(damageActual);
        Debug.Log(name + " dealt " + damageActual + " damage to " + target);
    }

    public void HealFlat(float healAmount)
    {
        currentHealth += healAmount;
        StatUpdate();
    }

    public void HealPercentage(float healAmountPercentage)
    {
        currentHealth *= healAmountPercentage;
        StatUpdate();
    }

    public float GetMaximumHealth()
    {
        return healthActual;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    protected virtual void StatUpdate()
    {
        damageActual = damage + damageBonus;
        healthActual = health + healthBonus;
        armorActual = armor + armorBonus;
        attackSpeedActual = attackSpeed * attackSpeedBonus;
        healthRegenerationActual = healthRegeneration + healthRegenerationBonus;
        movementSpeedActual = movementSpeed + movementSpeedBonus;

        if (currentHealth > healthActual) currentHealth = healthActual;
    }
}
