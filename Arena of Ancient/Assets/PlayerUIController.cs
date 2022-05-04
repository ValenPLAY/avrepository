using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIController : Singleton<PlayerUIController>
{
    
    [Header("Wave Info Display")]
    public TextMeshProUGUI waveNumberText;

    [Header ("Bars")]
    public Slider healthBar;
    public RectTransform energyBar;
    private float defaultHealthBarSize;
    private float defaultEnergyBarSize;

    [Header("Abilities")]
    public GameObject abilitiesContainer;

    private void Awake()
    {
        //defaultHealthBarSize = healthBar.sizeDelta.x;
        defaultEnergyBarSize = energyBar.sizeDelta.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameController.Instance.selectedHero.GetMaximumHealth();
        GameController.Instance.selectedHero.onHealthChangedEvent += OnHealthChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.rect.width -= Time.deltaTime;
        //float valueActual = defaultHealthBarSize * ()
       //healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x - 1,healthBar.sizeDelta.y);
        //Debug.Log(healthBar.sizeDelta);
    }

    void OnHealthChangedCallback (float currentHealth, float maxHealth)
    {
        float healthValueActual = (currentHealth / maxHealth);
        healthBar.value = healthValueActual;
    }

    public void UIUpdate()
    {
        Unit selectedHero = GameController.Instance.selectedHero;
        float healthValueActual = defaultHealthBarSize * (selectedHero.GetCurrentHealth() / selectedHero.GetMaximumHealth());
        //healthBar.sizeDelta = new Vector2(healthValueActual, healthBar.sizeDelta.y);

    }
}
