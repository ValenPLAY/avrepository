using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum state
    {
        normal,
        stunned,
        paused,
        dying,
    }

    protected state unitState;

    [Header("Unit Description")]
    public string unitName = "Unnamed Unit";
    public string unitDescription = "Unnamed Desc";
    public Sprite unitIcon;

    [Header("Unit Decorations")]
    [SerializeField] SpecialEffect unitDeathEffect;
    [SerializeField] SpecialEffect unitHitEffect;

    [Header("Unit Statistics")]
    [Header("Health")]
    public float health = 10.0f;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            healthActual = health + healthBonus;
            CurrentHealth += value;
        }
    }
    public float healthBonus;
    protected float healthActual;
    protected float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;

        set
        {
            currentHealth = value;
            onHealthChangedEvent?.Invoke(currentHealth, healthActual);
        }
    }
    public Action<float, float> onHealthChangedEvent;
    public Action<Unit> onUnitDeathEvent;

    public float healthRegeneration = 0.0f;
    public float HealthRegeneration
    {
        get => healthRegeneration;
        set
        {
            healthRegeneration = value;
        }
    }
    public float healthRegenerationBonus;
    protected float healthRegenerationActual;

    [Header("Armor")]
    public float armor;
    public float Armor
    {
        get => armor;
        set
        {
            armor = value;
            armorActual = armor + armorBonus;
        }
    }
    public float armorBonus;
    protected float armorActual;

    [Header("Damage")]
    public float damage = 1.0f;
    public float damageBonus;
    protected float damageActual;
    public float Damage
    {
        get => damage;

        set
        {
            damage = value;
            damageActual = damage + damageBonus;
            onDamageChangeValue?.Invoke(damage);
        }
    }
    public Action<float> onDamageChangeValue;

    public float attackSpeed = 1.0f;
    public float AttackSpeed
    {
        get => AttackSpeed;
        set
        {
            attackSpeed = value;
        }
    }
    public float attackSpeedBonus = 1.0f;
    private float attackSpeedActual;
    protected float attackCooldownCurrent;

    [Header("Energy")]
    public float energy = 10.0f;
    public float Energy
    {
        get => energy;
        set
        {
            energy = value;
            energyActual = energy + energyBonus;
        }
    }
    public float energyBonus;
    protected float energyActual;

    public float attackRange = 5.0f;



    [Header("MovementSpeed")]
    public float movementSpeed = 5.0f;
    public float movementSpeedBonus;
    protected float movementSpeedActual;

    [Header("Buffs")]
    public List<Buff> buffs = new List<Buff>();

    [Header("Unit Components")]
    protected BoxCollider unitColliderBox;
    protected CapsuleCollider unitColliderCapsule;
    protected Animator unitAnimator;

    [Header("Unit Sounds")]
    [SerializeField] protected AudioClip soundSpawn;
    [SerializeField] protected AudioClip soundHit;
    [SerializeField] protected AudioClip soundDeath;
    [SerializeField] protected AudioClip soundAttack;


    protected virtual void Awake()
    {
        unitColliderBox = GetComponent<BoxCollider>();
        unitColliderCapsule = GetComponent<CapsuleCollider>();
        unitAnimator = GetComponent<Animator>();

        StatUpdate();

        currentHealth = healthActual;
        unitState = state.normal;

        gameObject.name = unitName;
    }

    protected virtual void Attack()
    {
        if (soundAttack != null)
        {
            SoundController.Instance.SpawnSoundEffect(soundAttack, transform.position);
        }
    }

    protected virtual void Start()
    {
        if (soundSpawn != null && GameController.Instance != null)
        {
            SoundController.Instance.SpawnSoundEffect(soundSpawn, transform.position);
        }
    }

    protected virtual void Update()
    {
        //if (currentHealth <= healthActual && healthRegenerationActual > 0) currentHealth += Time.deltaTime * healthRegenerationActual;
        if (attackCooldownCurrent > 0) attackCooldownCurrent -= Time.deltaTime;
        if (healthRegenerationActual > 0 && CurrentHealth <= healthActual && CurrentHealth > 0)
        {
            CurrentHealth += healthRegenerationActual * Time.deltaTime;
        }
    }

    public virtual void TakeDamage(float incomingDamage)
    {
        Debug.Log("Incoming Damage: " + incomingDamage);
        incomingDamage = Mathf.Clamp(incomingDamage - armorActual, 0, incomingDamage);
        CurrentHealth -= incomingDamage;

        if (incomingDamage > 0 && unitHitEffect != null)
        {
            Instantiate(unitHitEffect, transform.position, unitHitEffect.transform.rotation);

        }

        if (CurrentHealth <= 0)
        {
            Death();
        }

        if (soundHit != null && CurrentHealth > 0)
        {
            SoundController.Instance.SpawnSoundEffect(soundHit, transform.position);
        }

    }

    protected virtual void Death()
    {
        if (unitState != state.dying)
        {
            onUnitDeathEvent?.Invoke(this);
            unitState = state.dying;
            if (unitAnimator != null)
            {
                unitAnimator.SetTrigger("Death");
            }
            else
            {

                DespawnUnit();
                Debug.Log(unitName + " has fallen.");
            }

            if (soundDeath != null)
            {
                SoundController.Instance.SpawnSoundEffect(soundDeath, transform.position);
            }
        }


    }

    public void OrderAttack()
    {
        if (attackCooldownCurrent <= 0 && unitState == state.normal)
        {
            if (unitAnimator != null)
            {
                unitAnimator.SetTrigger("Attack");
            }
            else
            {
                Attack();
            }
            attackCooldownCurrent = attackSpeedActual;

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
        //currentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, healthActual);
        //StatUpdate();
    }

    public void HealPercentage(float healAmountPercentage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth * healAmountPercentage, 0, healthActual);
        //StatUpdate();
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

    protected virtual void UnitEnable()
    {
        unitState = state.normal;
    }

    protected virtual void DespawnUnit()
    {
        if (unitDeathEffect != null)
        {
            Instantiate(unitDeathEffect, transform.position, unitHitEffect.transform.rotation);
        }
        Destroy(gameObject);
    }
}
