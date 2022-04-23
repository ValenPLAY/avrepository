using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] protected EnemyBullet bulletPrefab;
    [SerializeField] protected Vector3 bulletPointOffset;
    [SerializeField] protected float accuarcy = 0.0f;
    [SerializeField] protected int numberOfProjectiles;
    public List<EnemyBullet> recycleBullets = new List<EnemyBullet>();



    protected override void Update()
    {
        if (GameManager.Instance.GameStatus == EGameStatus.InGame)
        {
            base.Update();
            MoveRandom();
            if (currentTarget != null && _isReachedDestination == true)
            {
                Attack();
            }
        }
    }

    protected override void Attack()
    {
        if (attackDelayCurrent <= 0.0f)
        {
            transform.LookAt(currentTarget.transform);
            SpawnProjectile();
            attackDelayCurrent = attackDelay;
        }
    }

    protected virtual void SpawnProjectile()
    {
        if (bulletPrefab != null)
        {
            Vector3 bulletPosition = transform.position + bulletPointOffset;
            Vector3 aimingPoint = currentTarget.transform.position * Random.Range(1.0f - accuarcy, 1.0f + accuarcy);
            for (int x = 0; x <= numberOfProjectiles; x++)
            {
                if (recycleBullets.Count == 0)
                {
                    EnemyBullet firedBullet = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
                    firedBullet.sourceUnit = this;
                    firedBullet.transform.LookAt(aimingPoint);
                    firedBullet.bulletDamage = damage;
                    Debug.Log("Bullet Spawned");
                }
                else
                {
                    EnemyBullet firedBullet = recycleBullets[0];
                    firedBullet.transform.position = bulletPosition;

                    firedBullet.transform.LookAt(aimingPoint);
                    firedBullet.StateUpdate();

                    recycleBullets.Remove(firedBullet);
                    Debug.Log("Bullet Recycled");
                }




            }

        }
        else
        {
            Debug.Log(gameObject.name + " does not have a bullet prefab!");
        }


    }

    public void bulletRecyclePool(EnemyBullet bullet)
    {
        recycleBullets.Add(bullet);
    }

}
