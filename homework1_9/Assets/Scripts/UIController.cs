using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Character character;
    public Image charHpUI;
    private RectTransform charHpUiTransform;

    private float defaultHPSize;

    private float charHealth;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        charHpUiTransform = charHpUI.GetComponent<RectTransform>();
        defaultHPSize = charHpUiTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        charHealth = character.playerHealthCurrent;
        charHpUiTransform.sizeDelta = Vector2.Lerp(new Vector2(0, charHpUiTransform.sizeDelta.y), new Vector2(defaultHPSize, charHpUiTransform.sizeDelta.y), character.playerHealthCurrent / character.playerHealthDefault);

    }
}
