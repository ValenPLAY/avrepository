using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Colorchange : MonoBehaviour
{
    public GameObject gameController;
    public GameObject colorImage;
    private Color btnColor;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ColorChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColorChange()
    {
        var usedColor = colorImage.GetComponent<Image>().color;
        btnColor = usedColor;
        var controller = gameController.GetComponent<GameController>();
        controller.ColorChange(btnColor);
    }
}
