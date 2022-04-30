using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMelee : Hero
{
    [SerializeField] protected HitZone hitPrefab;

    protected override void Attack()
    {
        HitZone createdHitZone = Instantiate(hitPrefab, transform);

        base.Attack();
    }
}
