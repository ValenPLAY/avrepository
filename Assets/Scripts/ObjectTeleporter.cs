using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTeleporter : MonoBehaviour
{
    public float teleportTime = 4.0f;
    public float teleportRange = 2.0f;

    private float teleportCountdown;
    // Start is called before the first frame update
    void Start()
    {
        teleportCountdown = teleportTime;
    }

    // Update is called once per frame
    void Update()
    {
        teleportCountdown -= 1 * Time.deltaTime;
        if (teleportCountdown<=0)
        {
            Vector3 teleportPosition = new Vector3(Random.Range(-1.0f, 1.0f),Random.Range(0.0f, 1.0f),Random.Range(-1.0f, 1.0f));
            teleportPosition = teleportPosition * teleportRange;
            transform.Translate(teleportPosition);
            teleportCountdown = teleportTime;
        }
    }
}
