using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tip = "";
    private RectTransform selectedButtonUI;

    public GameObject tipUI;
    private GameObject currentTip;
    private Vector3 tipOffset;

    // Start is called before the first frame update
    void Start()
    {
        selectedButtonUI = GetComponent<RectTransform>();
        tipOffset = new Vector3(0.0f,0.0f,0.0f);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("test");
        
        var tipPosition = selectedButtonUI.anchoredPosition;

        currentTip = Instantiate(tipUI);
        var currentTipPos = currentTip.GetComponent<RectTransform>();
        //currentTipPos.position = new Vector2(1000.0f,0.0f);
        //currentTipPos.
        //currentTipPos.localPosition = tipPosition;
        //currentTipPos.anchoredPosition = new Vector2(100.0f, 100.0f);

        //selectedButtonUI.localPosition
        //currentTipPos.anchoredPosition = new Vector2(100.0f,100.0f);
        //currentTip.transform.localPosition = tipPosition;
        //currentTip.transform = new Vector2(100.0f, 100.0f);
        //currentTipPos
        currentTip.transform.SetParent(gameObject.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("testOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
