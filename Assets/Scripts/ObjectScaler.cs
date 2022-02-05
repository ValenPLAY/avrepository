using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    
    public float increasedScaleFloat;
    public float increaseTime = 5.0f;
    private int isBigger = 1;

    private Vector3 increasedScale;
    private Vector3 increasedScalePerTick;
    // Start is called before the first frame update
    void Start()
    {
        if (increasedScaleFloat == 0.0f) {
            increasedScaleFloat = Random.Range(0.3f, 3.0f);
        }
        if (increasedScaleFloat < 1.0f)
        {
            isBigger = -1;
        }
        increasedScale = transform.localScale * increasedScaleFloat;
        Debug.Log("Scale Magnitude" + transform.localScale.magnitude);
        Debug.Log("Increased Scale Magnitude"+increasedScale.magnitude);
        if (increaseTime == 0.0f)
        {
            increaseTime = 1.0f;
        }
        increasedScalePerTick = increasedScale / increaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log (increasedScale.magnitude - transform.localScale.magnitude);
        if (Mathf.Abs(increasedScale.magnitude - transform.localScale.magnitude) > 0.1f)
        {
            transform.localScale += isBigger * increasedScalePerTick * Time.deltaTime;
        }
        else {
            transform.localScale = increasedScale;
        }
    }
}
