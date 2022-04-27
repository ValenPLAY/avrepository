using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHero : Hero
{
    [Header("Ranged Options")]
    [SerializeField] bool isProjectileBased;
    [SerializeField] Projectile unitProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        if (isProjectileBased)
        {
            FireProjectile(GameController.Instance.playerWorldMousePos);
        } else
        {

        }

        base.Attack();
    }

    protected virtual void FireProjectile(Vector3 targetPosition)
    {
        if (unitProjectile != null)
        {
            Projectile firedProjectile = Instantiate(unitProjectile, upperBody.transform.position, Quaternion.identity);
            
            firedProjectile.transform.LookAt(GameController.Instance.playerWorldMousePos);
            firedProjectile.projectileOwner = this;
            firedProjectile.projectileDamage = damage;

            firedProjectile.gameObject.SetActive(true);


        }
    }
}
