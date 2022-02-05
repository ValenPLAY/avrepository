using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraMovementSpeed = 1;
    private float actualMovementSpeed;
    private Vector3 movingVector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        actualMovementSpeed = cameraMovementSpeed / 100;
        if (Input.GetKey(KeyCode.W))
        {
            movingVector = movingVector + new Vector3(0.0f, 0.0f, actualMovementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movingVector = movingVector + new Vector3(0.0f, 0.0f, -actualMovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movingVector = movingVector + new Vector3(-actualMovementSpeed, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movingVector = movingVector + new Vector3(actualMovementSpeed, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            movingVector.y -= Input.GetAxis("Mouse ScrollWheel");
            //movingVector = movingVector + new Vector3(0.0f, 0.1f, 0.0f);
        }
        transform.position = transform.position + movingVector;
        movingVector = new Vector3(0.0f,0.0f,0.0f);
    }
}
