using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Unit
{
    GameObject currentTarget;
    Vector3 currentTargetPosition;
    NavMeshAgent agent;
    [SerializeField] float retargetMovingTargetDistance = 1.0f;
    [SerializeField] float retargetDuration = 2.0f;
    protected float retargetDurationCurrent;
    protected float distanceTillTarget;
    protected float targetMovedDistance;

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();

        health *= GameController.Instance.difficulty;
        currentHealth = health;

        damage *= GameController.Instance.difficulty;
        movementSpeed *= GameController.Instance.difficulty;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (currentTarget == null)
        {
            if (retargetDurationCurrent <= 0)
            {
                Retarget();
                retargetDurationCurrent = retargetDuration;
            }

            retargetDurationCurrent -= Time.deltaTime;
        }
        if (currentTarget != null)
        {
            targetMovedDistance = Vector3.Distance(currentTarget.transform.position, agent.destination);

            if (targetMovedDistance >= retargetMovingTargetDistance)
            {
                agent.SetDestination(currentTarget.transform.position);
            }

            distanceTillTarget = Vector3.Distance(currentTargetPosition, transform.position);

            if (distanceTillTarget <= attackRange)
            {
                agent.ResetPath();
                OrderAttack();
            }
        }
    }

    protected virtual void Retarget()
    {
        if (GameController.Instance.selectedHero != null)
        {
            currentTarget = GameController.Instance.selectedHero.gameObject;
            currentTargetPosition = currentTarget.transform.position;
        }
        
        
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Death()
    {
        GameController.Instance.enemiesOnMap.Remove(this);
        base.Death();
    }
}
