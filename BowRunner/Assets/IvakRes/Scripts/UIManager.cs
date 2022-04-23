using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _pause = null;
    [SerializeField] private GameObject _pauseMenu = null;
    private void Start()
    {
        _pause.onClick.AddListener(Pause);
    }

    public void Pause()
    {
        if (_pauseMenu.activeSelf == false)
        {
            _pauseMenu.SetActive(true);

            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }
        else
        {
            _pauseMenu.SetActive(false);

            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
