using TMPro;
using UnityEngine;

public class HeroPanel : MonoBehaviour
{
    [Header("Upgrade Fields")]

    [SerializeField] TMP_Text heroInfoText;
    [SerializeField] TMP_Text damageCurrentText;
    [SerializeField] TMP_Text healthCurrentText;
    Unit upgradingUnit;

    private void Awake()
    {


    }

    private void OnEnable()
    {
        upgradingUnit = GameController.Instance.selectedHero;
        if (upgradingUnit != null)
        {
            heroInfoText.text = "<color=#ff7d19>" + upgradingUnit.unitName + "</color><br>" + upgradingUnit.unitDescription;
            damageCurrentText.text = upgradingUnit.damage + " (Bonus)";
            healthCurrentText.text = upgradingUnit.health + " (Bonus)";
        }
    }
}
