using UnityEngine;
using UnityEngine.UI;

public class UpgradesPanel : MonoBehaviour
{
    [Header("Player Health")]
    public Text healthUpgradeValue;
    public Text healthUpgradeCost;
    [Header("Player Movement Speed")]
    public Text msUpgradeValue;
    public Text msUpgradeCost;
    [Header("Player Jump Strength")]
    public Text jsUpgradeValue;
    public Text jsUpgradeCost;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthUpgradeValue.text = StatsContainer.playerHp.ToString();
        healthUpgradeCost.text = StatsContainer.playerHpCost.ToString();
        msUpgradeValue.text = StatsContainer.playerMovementSpeed.ToString();
        msUpgradeCost.text = StatsContainer.playerMsCost.ToString();
        jsUpgradeValue.text = StatsContainer.playerJumpStrength.ToString();
        jsUpgradeCost.text = StatsContainer.playerJSCost.ToString();
    }
}
