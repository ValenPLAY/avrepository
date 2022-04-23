using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    Button pauseButton;
    // Start is called before the first frame update
    void Awake()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(BtnClick);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void BtnClick()
    {
        //Debug.Log("Test");
        //Debug.LogWarning("Test");
    }
}
