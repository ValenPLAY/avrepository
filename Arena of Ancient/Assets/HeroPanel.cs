using TMPro;
using UnityEngine;

public class HeroPanel : MonoBehaviour
{
    [Header("Upgrade Fields")]

    [SerializeField] TMP_Text heroInfoText;


    Hero upgradingUnit;

    [Header("Upgrades and Modifiers")]
    [Header("Research Points")]
    [SerializeField] TMP_Text researchPointsTMP;
    [Header("Damage")]
    [SerializeField] TMP_Text damageCurrentTMP;
    [SerializeField] float upgradeDamagePercentageBonus;
    [SerializeField] float upgradeDamageFlatBonus;
    [Header("Health")]
    [SerializeField] TMP_Text healthCurrentTMP;
    [SerializeField] float upgradeHealthPercentageBonus;
    [SerializeField] float upgradeHealthFlatBonus;
    [Header("Energy")]
    [SerializeField] TMP_Text energyCurrentTMP;
    [SerializeField] float upgradeEnergyPercentageBonus;
    [SerializeField] float upgradeEnergyFlatBonus;
    [SerializeField] float abilityDamagePercentageBonus;
    [Header("Miscelanious")]
    [SerializeField] TMP_Text armorCurrentTMP;
    [SerializeField] float upgradeArmorFlatBonus;
    [SerializeField] TMP_Text healthRegenCurrentTMP;
    [SerializeField] float upgradeHealthRegenerationFlatBonus;


    private void Awake()
    {
        gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        RefreshInfo();
    }

    public void RefreshInfo()
    {
        if (GameController.Instance != null)
        {
            upgradingUnit = GameController.Instance.selectedHero;
            if (upgradingUnit != null)
            {
                heroInfoText.text = "<color=#ff7d19>" + upgradingUnit.unitName + "</color><br>" + upgradingUnit.unitDescription;
                researchPointsTMP.text = "Current research points: <b>" + upgradingUnit.upgradePoints + "</b>";
                damageCurrentTMP.text = upgradingUnit.damage + "";
                healthCurrentTMP.text = upgradingUnit.health + "";
                energyCurrentTMP.text = upgradingUnit.energy + "";
                armorCurrentTMP.text = upgradingUnit.armor + "";
                healthRegenCurrentTMP.text = upgradingUnit.healthRegeneration + "/sec";
            }
        }
    }

    public void UpgradeDamage()
    {
        if (upgradingUnit.upgradePoints > 0)
        {
            upgradingUnit.Damage *= 1.0f + upgradeDamagePercentageBonus;
            upgradingUnit.Damage += upgradeDamageFlatBonus;
            upgradingUnit.upgradePoints--;
        }
        RefreshInfo();
    }

    public void UpgradeHealth()
    {
        if (upgradingUnit.upgradePoints > 0)
        {
            upgradingUnit.Health *= 1.0f + upgradeHealthPercentageBonus;
            upgradingUnit.Health += upgradeHealthFlatBonus;
            upgradingUnit.upgradePoints--;
        }
        RefreshInfo();
    }

    public void UpgradeEnergy()
    {
        if (upgradingUnit.upgradePoints > 0)
        {
            //upgradingUnit. *= 1.0f + upgradeHealthPercentageBonus;
            //upgradingUnit.health += upgradeHealthFlatBonus;
        }
        RefreshInfo();
    }

    public void UpgradeHealthRegen()
    {
        if (upgradingUnit.upgradePoints > 0)
        {
            upgradingUnit.HealthRegeneration += upgradeHealthRegenerationFlatBonus;
            upgradingUnit.upgradePoints--;
        }
        RefreshInfo();
    }
}
