using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [Header("Upgrade Fields")]
    [SerializeField] TMP_Text damageCurrentText;
    [SerializeField] TMP_Text healthCurrentText;
    Unit upgradingUnit;

    private void OnEnable()
    {
        upgradingUnit = GameController.Instance.selectedHero;
        if (upgradingUnit != null)
        {
            damageCurrentText.text = upgradingUnit.damage + " (Bonus)";
            healthCurrentText.text = upgradingUnit.health + " (Bonus)";
        }
    }
}
