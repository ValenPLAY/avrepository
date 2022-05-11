using UnityEngine;
using UnityEngine.UI;

public class UIAbilityIcon : MonoBehaviour
{
    [SerializeField] Image abilityImage;
    public void UpdateUIIcon(Sprite sprite, int abilityID)
    {
        abilityImage.sprite = sprite;
    }
}
