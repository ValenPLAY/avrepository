using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    private Text coinsDisplay;
    // Start is called before the first frame update

    private void Start()
    {
        coinsDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coinsDisplay.text = StatsContainer.coinsAmount.ToString();
    }
}
