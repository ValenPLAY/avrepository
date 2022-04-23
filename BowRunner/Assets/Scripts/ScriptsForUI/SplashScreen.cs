using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private float loadingTime = 2f;

    private void Start()
    {
        progressBarImage.fillAmount = 0;
        StartCoroutine(LoadingRoutine());
    }

    private IEnumerator LoadingRoutine()
    {
        var loadingOperation = SceneManager.LoadSceneAsync("WorkingSceneJenya");
        loadingOperation.allowSceneActivation = false;
        var loadingProgress = 0f;
        while (progressBarImage.fillAmount<1f)
        {
            loadingProgress += Time.deltaTime/loadingTime;
            if (loadingProgress > 1f)
            {
                loadingProgress = 1;
            }
            progressBarImage.fillAmount = loadingProgress;
            yield return null;
        }

        loadingOperation.allowSceneActivation = true;
    }
}
