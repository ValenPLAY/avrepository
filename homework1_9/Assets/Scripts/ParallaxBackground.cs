using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraMain;
    public float parallaxEffect = 1.0f;
    private float spriteLength;
    private float startPosition;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
        spriteLength = sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float difference = cameraMain.position.x * (1 - parallaxEffect);

        float distance = (cameraMain.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + distance,transform.position.y,transform.position.z);

        if (difference > startPosition+spriteLength)
        {
            startPosition += spriteLength;
        } else if (difference < startPosition - spriteLength)
        {
            startPosition -= spriteLength;
        }
    }
}
