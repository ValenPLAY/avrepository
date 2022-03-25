using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Rotator : MonoBehaviour
{
    private float rotationSpeedAmplifier = 25.0f;
    public bool onlyY = true;
    public Vector3 rotationDirection;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(rotationDirection);
        if (rotationDirection==new Vector3 (0.0f,0.0f,0.0f)) {
            var randomDirection = Random.Range(-1.0f,1.0f);
            rotationDirection = new Vector3(randomDirection, randomDirection, randomDirection);
            if (onlyY == true) {
                rotationDirection.x = 0.0f;
                rotationDirection.y = 0.0f;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentRotation = rotationDirection * Time.deltaTime * rotationSpeedAmplifier;
        transform.Rotate(currentRotation);
    }
}
