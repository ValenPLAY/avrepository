using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public void StartLoading(int loadID)
    {
        MainMenuController.Instance.LoadLevel(loadID);
    }
}
