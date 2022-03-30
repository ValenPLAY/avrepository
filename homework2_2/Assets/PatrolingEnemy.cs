using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingEnemy : MonoBehaviour
{
    public List<Vector3> patrolPoints = new List<Vector3>();
    private int orderID = 0;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Count > 1 && agent.remainingDistance < 10)
        {
            agent.SetDestination(patrolPoints[orderID]);
            orderID++;
            if (orderID >= patrolPoints.Count) orderID = 0;
        }
    }
}
