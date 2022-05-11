using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Description")]
    [SerializeField] string abilityName = "Unknown Ability";
    [SerializeField] string abilityDescription = "A description for an unknown ability";
    [SerializeField] Sprite abilityIcon;

    [Header("Ability Stats")]
    public int abilityLevel = 1;
    [SerializeField] bool isActive;
    [SerializeField] Buff appliedBuff;

    [Header("Aura Options")]
    [SerializeField] bool isAura;
    [SerializeField] float auraRange;

    private Unit abilityOwner;
    private int abilityNumber;
    private UIAbilityIcon correspondingIcon;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        name = "Ability: " + abilityName;
        abilityOwner = transform.parent.GetComponent<Hero>();
        if (abilityOwner != null)
        {
            correspondingIcon = Instantiate(PlayerUIController.Instance.basicAbilityIcon, PlayerUIController.Instance.abilitiesContainer.transform);
        }

        if (abilityIcon != null)
        {
            correspondingIcon.UpdateUIIcon(abilityIcon, 0);
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void AbilityCastDown()
    {
        if (isActive)
        {

        }
    }

    public virtual void AbilityCastUp()
    {
        if (isActive)
        {

        }
    }

    protected virtual void OnDestroy()
    {
        if (correspondingIcon != null)
        {
            Destroy(correspondingIcon);
        }
    }
}
