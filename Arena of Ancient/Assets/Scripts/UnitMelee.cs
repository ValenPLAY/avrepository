using UnityEngine;
using UnityEngine.AI;

public class UnitMelee : Unit
{
    NavMeshAgent agent;
    [SerializeField] float retargetDistance = 1.0f;

    // Start is called before the first frame update
    override protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;


    }

    // Update is called once per frame
    override protected void Update()
    {
        if (Vector3.Distance(GameController.Instance.selectedHero.transform.position, agent.destination) >= retargetDistance)
        {
            agent.SetDestination(GameController.Instance.selectedHero.transform.position);
            agent.isStopped = true;
            agent.isStopped = false;
        }

    }
}
