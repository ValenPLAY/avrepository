using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    // Start is called before the first frame update
    private void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    private void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
