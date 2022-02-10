using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGroundZoomIn : MonoBehaviour
{
    public float zoomInDefault = 1.0f;
    public float zoomInTime = 5.0f;
    private float zoomPerSec;
    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        zoomPerSec = (zoomInDefault / zoomInTime);
        timeCounter = zoomInTime;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(transform.localScale.magnitude);

        if (timeCounter > 0) 
        {
            timeCounter -= 1 * Time.deltaTime;
            //transform.localScale += new Vector3(zoomPerSec, zoomPerSec) * Time.deltaTime;
            //transform.localScale = transform.localScale * zoomPerSec * Time.deltaTime;
        }
        
    }
}
