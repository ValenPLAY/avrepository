using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityIcon : MonoBehaviour
{
    [SerializeField] Image abilityImage;
    [SerializeField] GameObject abilityDescPanel;
    [SerializeField] TMP_Text abilityDescText;
    public Action showDescriptionPanel;

    public Ability correspondingAbility;

    public void UpdateUIIcon(Sprite sprite, int abilityID)
    {
        abilityImage.sprite = sprite;
    }

    public void UpdateAbilityDescription(string abilityName, string abilityDescription)
    {

    }

    public void ShowDescPanel()
    {
        abilityDescPanel.SetActive(true);
        abilityDescText.text = "<color=#ff7d19>" + correspondingAbility.getAbilityName() + "</color><br><br>" + correspondingAbility.getAbilityDesc();
    }

    public void HideDescPanel()
    {
        abilityDescPanel.SetActive(false);
    }
}
