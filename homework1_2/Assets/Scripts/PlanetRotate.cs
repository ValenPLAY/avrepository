using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float planetRotate = 1.0f;
    private float rotateMultiplier = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentrotate = planetRotate * rotateMultiplier * Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, currentrotate);
    }
}
