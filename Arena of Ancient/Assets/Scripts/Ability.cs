using System;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Description")]
    [SerializeField] string abilityName = "Unknown Ability";
    [TextArea] [SerializeField] string abilityDescription = "A description for an unknown ability";

    [Header("Ability Visuals")]
    [SerializeField] Sprite abilityIcon;

    [Header("Ability Stats")]
    public int abilityLevel = 1;
    public float abilityEnergyCost;

    [SerializeField] bool isActive;
    [SerializeField] bool isOnCastUp;

    [SerializeField] float cooldownDuration;
    private float cooldownDurationCurrent;
    private float CooldownDurationCurrent
    {
        get => cooldownDurationCurrent;
        set
        {
            cooldownDurationCurrent = value;
            if (cooldownDuration != 0)
            {
                cooldownDurationPercentage = cooldownDurationCurrent / cooldownDuration;
            }
            else
            {
                cooldownDurationPercentage = 0;
            }
            onCooldownPercentageChange?.Invoke(cooldownDurationPercentage);
        }
    }
    private float cooldownDurationPercentage;
    public Action onAbilityCooldownEvent;
    public Action<float> onCooldownPercentageChange;

    private Unit abilityOwner;
    private int abilityNumber;
    private UIAbilityIcon correspondingIcon;

    [Header("Ability Effects")]
    [SerializeField] List<AbilityEffect> abilityEffects = new List<AbilityEffect>();



    // Start is called before the first frame update
    protected virtual void Awake()
    {
        name = "Ability: " + abilityName;
        abilityOwner = transform.parent.GetComponent<Hero>();
        if (abilityOwner != null && PlayerUIController.Instance != null)
        {
            correspondingIcon = Instantiate(PlayerUIController.Instance.basicAbilityIcon, PlayerUIController.Instance.abilitiesContainer.transform);

            if (abilityIcon != null)
            {
                correspondingIcon.UpdateUIIcon(abilityIcon, 0);
                correspondingIcon.ConnectIconToAbility(this);
                //correspondingIcon.gameObject.SetActive(true);
            }

            CooldownDurationCurrent = cooldownDuration;
        }



    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CooldownDurationCurrent > 0) CooldownDurationCurrent -= Time.deltaTime;
    }

    public virtual void AbilityCastDown()
    {
        if (isActive && !isOnCastUp)
        {
            CastAbility();
        }
    }

    public virtual void AbilityCastUp()
    {
        if (isActive && isOnCastUp)
        {
            CastAbility();
        }
    }

    public virtual void CastAbility()
    {
        if (CooldownDurationCurrent <= 0)
        {
            if (abilityEnergyCost <= abilityOwner.Energy)
            {
                foreach (AbilityEffect appliedEffect in abilityEffects)
                {
                    appliedEffect.ApplyEffect(abilityOwner);
                    Debug.Log("Ability Casted");
                }

                CooldownDurationCurrent = cooldownDuration;
                abilityOwner.CurrentEnergy -= abilityEnergyCost;
            }
            else
            {
                Debug.Log("Not enough Energy to cast this Ability");
            }

        }
        else
        {
            Debug.Log("Ability is on Cooldown");
        }
    }

    protected virtual void OnDestroy()
    {
        if (correspondingIcon != null)
        {
            Destroy(correspondingIcon);
        }
    }

    public virtual string getAbilityName()
    {
        return abilityName;
    }

    public virtual string getAbilityDesc()
    {
        return abilityDescription;
    }
}
