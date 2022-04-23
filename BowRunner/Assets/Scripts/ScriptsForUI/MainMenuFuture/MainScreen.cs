using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : ScreenView
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text energyText;
    [SerializeField] private Image energyProgressBarImage;
    [SerializeField] private Text brilliantsText;
    [SerializeField] private Text experience;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Image experienceProgressBarImage;

  //  private Action OnPlayButtonClick = () => { };
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnServerInitialized(MainScreenData data, ParticleSystemOverlapAction onPlayButtonClick)
    {
        coinsText.text = data.coinsCount.ToString();
        brilliantsText.text = data.amountBrilliants.ToString();
        energyText.text = data.currentEnergy + "/" + data.maxEnergyCount;
        energyProgressBarImage.fillAmount = (float)data.currentEnergy / data.maxEnergyCount;
        experience.text = data.amountExperience.ToString();
        experienceProgressBarImage.fillAmount = (float)data.amountExperience / data.experienceToLvlUp;

        onPlayButtonClick = onPlayButtonClick;
       // playButton.onClick.AddListener(playButtonClickHandler);
    }

    private void PlayButtonClickHandler ()
    {
       // OnPlayButtonClickHandler.Invoke();
    }

    public void UpdateCoinsCount ( int newCoinsCount)
    {
        coinsText.text = newCoinsCount.ToString();
    }

    public void UpdateBrilliantsCount (int newBrilliantsCount)
    {
        brilliantsText.text = newBrilliantsCount.ToString();
    }
    public void UpdateExperience (int level)
    {
        experience.text = level.ToString();
    }
    public void UpdateEnergy ( int newEnergyCount, int maxEnergyCount)
    {
        energyText.text = newEnergyCount + "/" + maxEnergyCount;
        energyProgressBarImage.fillAmount = (float)newEnergyCount * maxEnergyCount;
     }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }


}

