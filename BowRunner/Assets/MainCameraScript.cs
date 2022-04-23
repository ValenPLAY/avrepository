using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] float cameraHeight;
    private Vector3 _cameraPosition;
    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        //cameraHeight = 35.0f;
        _cameraOffset = transform.position - Player.Instance.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPosition = Player.Instance.gameObject.transform.position + _cameraOffset;
        //_cameraPosition.y = cameraHeight;
        transform.position = _cameraPosition;
    }
}
