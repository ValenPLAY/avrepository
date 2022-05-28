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
    [SerializeField] protected float health = 10.0f;
    public float Health
    {
        get => health;
        set
        {
            float healthDifference = value - health;
            health = value;
            healthActual = health + healthBonus;
            CurrentHealth += healthDifference;
        }
    }
    protected float healthBonus;
    protected float healthActual;
    protected float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;

        set
        {
            currentHealth = value;
            onCurrentHealthChangedEvent?.Invoke(currentHealth);
            onHealthChangedPercentageEvent?.Invoke(currentHealth / healthActual);
        }
    }
    public Action<float> onCurrentHealthChangedEvent;
    public Action<float> onHealthChangedPercentageEvent;

    [SerializeField] protected float healthRegeneration = 0.0f;
    public float HealthRegeneration
    {
        get => healthRegeneration;
        set
        {
            healthRegeneration = value;
            healthRegenerationActual = healthRegeneration + healthRegenerationBonus;
        }
    }
    protected float healthRegenerationBonus;
    protected float healthRegenerationActual;

    [Header("Armor")]
    [SerializeField] protected float armor;
    public float Armor
    {
        get => armor;
        set
        {
            armor = value;
            armorActual = armor + armorBonus;
        }
    }
    protected float armorBonus;
    protected float armorActual;

    [Header("Damage")]
    [SerializeField] protected float damage = 1.0f;
    protected float damageBonus;
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

    [SerializeField] protected float attackSpeed = 1.0f;
    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            attackSpeed = value;
            attackSpeedActual = attackSpeed * (1 + attackSpeedBonus);
        }
    }
    protected float attackSpeedBonus = 0f;
    protected float attackSpeedActual;
    protected float attackCooldownCurrent;

    [Header("Energy")]
    [SerializeField] protected float energy = 10.0f;
    public float Energy
    {
        get => energy;
        set
        {
            energy = value;
            energyActual = energy + energyBonus;
        }
    }
    protected float energyBonus;
    protected float energyActual;

    protected float currentEnergy;
    public float CurrentEnergy
    {
        get => currentEnergy;
        set
        {
            currentEnergy = value;
            onCurrentEnergyChangeEvent?.Invoke(currentEnergy);
            onCurrentEnergyChangePercentageEvent?.Invoke(currentEnergy / energyActual);
        }
    }
    public Action<float> onCurrentEnergyChangeEvent;
    public Action<float> onCurrentEnergyChangePercentageEvent;

    [SerializeField] protected float energyRegen = 0.25f;
    public float EnergyRegen
    {
        get => energyRegen;
        set
        {
            energyRegen = value;
            energyRegenActual = energyRegen + energyRegenBonus;
        }
    }
    protected float energyRegenBonus;
    protected float energyRegenActual;

    [SerializeField] public float attackRange = 5.0f;



    [Header("MovementSpeed")]
    public float movementSpeed = 5.0f;
    public float movementSpeedBonus;
    public float MovementSpeed
    {
        get => movementSpeed;
        set
        {
            movementSpeed = value;
            movementSpeedActual = movementSpeed + movementSpeedBonus;
        }
    }
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

    public Action<Unit> onUnitDeathEvent;
    public Action<Unit> onUnitAttackEvent;
    public Action<Unit, Unit> onUnitDealDamageEvent;
    public Action<Unit> onUnitDespawnEvent;


    protected virtual void Awake()
    {
        unitColliderBox = GetComponent<BoxCollider>();
        unitColliderCapsule = GetComponent<CapsuleCollider>();
        unitAnimator = GetComponent<Animator>();

        StatUpdate();

        CurrentHealth = healthActual;
        CurrentEnergy = energyActual;

        unitState = state.normal;

        gameObject.name = unitName;
    }

    protected virtual void Attack()
    {
        onUnitAttackEvent?.Invoke(this);
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

        HealthRegenUpdate();
        EnergyRegenUpdate();
    }

    void HealthRegenUpdate()
    {
        if (healthRegenerationActual > 0 && CurrentHealth < healthActual && CurrentHealth > 0)
        {
            CurrentHealth += healthRegenerationActual * Time.deltaTime;
        }
        else if (CurrentHealth > healthActual)
        {
            CurrentHealth = healthActual;
        }
    }

    void EnergyRegenUpdate()
    {
        if (energyRegenActual > 0 && CurrentEnergy < energyActual)
        {
            CurrentEnergy += energyRegenActual * Time.deltaTime;
        }
        else if (CurrentEnergy > energyActual)
        {
            CurrentEnergy = energyActual;
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
            Debug.Log(attackSpeedActual);

            if (unitAnimator != null)
            {
                if (unitAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
                {
                    Attack();
                }

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
        onUnitDealDamageEvent?.Invoke(this, target);
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
        //Health
        Health += 0;
        HealthRegeneration += 0;

        //Damage
        Damage += 0;
        AttackSpeed += 0;

        //Energy
        Energy += 0;
        EnergyRegen += 0;

        //Misc
        Armor += 0;
        MovementSpeed += 0;

        if (currentHealth > healthActual) currentHealth = healthActual;
    }

    protected virtual void UnitEnable()
    {
        unitState = state.normal;
    }

    protected virtual void DespawnUnit()
    {
        onUnitDespawnEvent?.Invoke(this);
        if (unitDeathEffect != null)
        {
            Instantiate(unitDeathEffect, transform.position, unitHitEffect.transform.rotation);
        }
        Destroy(gameObject);
    }
}
