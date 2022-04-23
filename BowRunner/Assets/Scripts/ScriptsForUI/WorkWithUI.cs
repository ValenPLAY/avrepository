using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkWithUI : MonoBehaviour
{
    public void OpenNextLevel(string loadedSceneName)
    {
        SceneManager.LoadScene(loadedSceneName, LoadSceneMode.Single);
    }
}
