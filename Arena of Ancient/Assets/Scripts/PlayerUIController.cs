using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : Singleton<PlayerUIController>
{
    [Header("Panels")]
    public GameObject inGamePanel;
    public GameObject pausePanel;

    [Header("Wave Info Display")]
    public TMP_Text waveNumberText;

    [Header("Bars")]
    public Slider healthBar;
    public Slider energyBar;
    private float defaultHealthBarSize;
    private float defaultEnergyBarSize;

    [Header("Abilities")]
    public GameObject abilitiesContainer;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.selectedHero.onHealthChangedEvent += OnHealthChangedCallback;
        GameController.Instance.currentWaveChangedEvent += OnWaveChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnWaveChangedCallback(int currentWave)
    {
        waveNumberText.text = currentWave + "";
    }

    void OnHealthChangedCallback(float currentHealth, float maxHealth)
    {
        float healthValueActual = (currentHealth / maxHealth);
        healthBar.value = healthValueActual;
    }

    public void ShowPauseMenu(bool state)
    {
        inGamePanel.SetActive(!state);
        pausePanel.SetActive(state);
    }

    public void UIUpdate()
    {
        Unit selectedHero = GameController.Instance.selectedHero;
        float healthValueActual = defaultHealthBarSize * (selectedHero.GetCurrentHealth() / selectedHero.GetMaximumHealth());
        //healthBar.sizeDelta = new Vector2(healthValueActual, healthBar.sizeDelta.y);

    }
}
