using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Description")]
    
    //[Tooltip("Basic text and visual information about the inspector.")]
    [SerializeField] string abilityName = "Unknown Ability";
    [TextArea][SerializeField] string abilityDescription = "A description for an unknown ability";

    [Space]
    [Header("Ability Visuals")]
    [SerializeField] Sprite abilityIcon;

    [Space]
    [Header("Ability Stats")]
    //[Tooltip("Ability statistics, it's level and ways to cast it.")]

    public int abilityLevel = 1;
    [SerializeField] bool isActive;
    [SerializeField] bool isOnCastUp;
    [SerializeField] float cooldownDuration;
    private float cooldownDurationCurrent;
    //[SerializeField] Buff appliedBuff;

    //[Header("Aura Options")]
    //[SerializeField] bool isAura;
    //[SerializeField] float auraRange;

    private Unit abilityOwner;
    private int abilityNumber;
    private UIAbilityIcon correspondingIcon;

    [Space]
    [Header("Ability Effects")]
    [Tooltip("Effects that will play upon casting an ability")]

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
                correspondingIcon.correspondingAbility = this;
            }
        }



    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (cooldownDurationCurrent > 0) cooldownDurationCurrent -= Time.deltaTime;
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
        if (cooldownDurationCurrent <= 0)
        {
            foreach (AbilityEffect appliedEffect in abilityEffects)
            {
                appliedEffect.ApplyEffect(abilityOwner);
                Debug.Log("Ability Casted");
            }

            cooldownDurationCurrent = cooldownDuration;
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
