using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public MeleeEnemy owner;
    private void OnTriggerEnter(Collider other)
    {
        owner.EngagementZone();
    }
}
