using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character Info
    public string charName = "Empty";
    public string charDescription = "Empty";
    // Character Stats
    public float movementSpeed = 10f;
    public float health = 100f;
    public float stamina = 100f;
    public float jumpStrength = 13;
    public float sprintModifier = 1;

    public GameObject characterSuit;
    public GameObject cameraPivot;
    public Quaternion defaultCameraRotation;


    public void Start()
    {
        defaultCameraRotation = cameraPivot.transform.rotation;
    }
}
