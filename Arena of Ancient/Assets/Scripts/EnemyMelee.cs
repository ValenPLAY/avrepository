using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Unit
{
    Unit currentTarget;
    Vector3 currentTargetPosition;
    NavMeshAgent agent;
    [Header("Path Finding Options")]
    [SerializeField] float retargetMovingTargetDistance = 1.0f;
    [SerializeField] float retargetDuration = 2.0f;
    protected float retargetDurationCurrent;
    protected float distanceTillTarget;
    protected float targetMovedDistance;

    [Header("Difficulty Scaling Options")]
    [SerializeField] float healthIncreasePerWave;
    [SerializeField] float damageIncreasePerWave;

    private float missRangeMultiplier = 1.2f;

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

        unitState = state.paused;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (unitState != state.paused)
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
                if (AbleToHitCheck())
                {
                    agent.ResetPath();
                    OrderAttack();
                }
            }
            unitAnimator.SetBool("isWalking", agent.velocity != Vector3.zero);
        }
        base.Update();
    }

    protected virtual void Retarget()
    {
        if (GameController.Instance.selectedHero != null)
        {
            currentTarget = GameController.Instance.selectedHero;
            //currentTargetPosition = currentTarget.transform.position;
        }


    }

    protected override void Attack()
    {
        base.Attack();

        if (AbleToHitCheck()) DealDamage(currentTarget);
    }

    protected virtual bool AbleToHitCheck()
    {
        if (currentTarget != null)
        {
            distanceTillTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            if (distanceTillTarget <= attackRange * missRangeMultiplier)
            {
                return true;
            }
        }
            return false;
    }

    protected override void Death()
    {
        GameController.Instance.enemiesOnMap.Remove(this);
        base.Death();
    }
}
