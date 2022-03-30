using UnityEngine;

public class RTSCameraMovement : MonoBehaviour
{
    public float borderThickness = 20.0f;
    public float cameraMovementSpeed = 10.0f;
    public bool isDebugMode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        var view = camera.ScreenToViewportPoint(Input.mousePosition);
        var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        if (isOutside == false || isDebugMode == false)
        {

            Vector3 newPosition = transform.position;

            //UpperBorder
            if (Input.mousePosition.y >= Screen.height - borderThickness)
            {
                newPosition.z += cameraMovementSpeed * Time.deltaTime;
            }

            //LowerBorder
            if (Input.mousePosition.y <= borderThickness)
            {
                newPosition.z -= cameraMovementSpeed * Time.deltaTime;
            }


            if (Input.mousePosition.x >= Screen.width - borderThickness)
            {
                newPosition.x += cameraMovementSpeed * Time.deltaTime;
            }


            if (Input.mousePosition.x <= borderThickness)
            {
                newPosition.x -= cameraMovementSpeed * Time.deltaTime;
            }
            transform.position = newPosition;
        }
    }
}
