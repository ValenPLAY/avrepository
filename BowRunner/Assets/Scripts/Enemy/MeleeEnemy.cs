using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemy : Enemy
{
    [SerializeField] DangerZone dangerZone;
    [SerializeField] float attackRange;
    private float _distanceTillEnemy;
    protected override void Awake()
    {
        base.Awake();

        if (dangerZone == null) dangerZone = gameObject.GetComponentInChildren<DangerZone>();
        if (dangerZone != null)
        {
            dangerZone.owner = this;
        }

    }

    protected override void Update()
    {
        if (GameManager.Instance.GameStatus == EGameStatus.InGame)
        {
            base.Update();
            if (currentTarget != null)
            {
                _distanceTillEnemy = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (_distanceTillEnemy <= attackRange)
                {
                    Attack();
                }
                else
                {
                    StartMoving(currentTarget.transform.position);
                }

            }
        }
        
    }

    protected override void Attack()
    {
        
        if (attackDelayCurrent<=0)
        {
            Debug.Log("Attack");
            //currentTarget.GetComponent<Player>()
            attackDelayCurrent = attackDelay;
        }
        
    }

    public void EngagementZone()
    {
        //Attack();
    }

}
