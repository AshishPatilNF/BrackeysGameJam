using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField]
    bool isPatrolling = true;

    NavMeshAgent navMeshAgent;

    PatrolWayPoints wayPoint;

    Transform patrolPoint = null;

    Transform newPatrolPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wayPoint = FindObjectOfType<PatrolWayPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            if (patrolPoint != null)
            {
                navMeshAgent.destination = patrolPoint.position;
                float dist = navMeshAgent.remainingDistance;

                if (dist != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance == 0)
                {
                    newPatrolPoint = wayPoint.AssignWayPoint();

                    if (newPatrolPoint != null)
                    {
                        wayPoint.AddWayPoint(patrolPoint);
                        patrolPoint = newPatrolPoint;
                    }
                }
            }
            else
            {
                patrolPoint = wayPoint.AssignWayPoint();
            }
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    public void StartPatrolling()
    {
        isPatrolling = true;
    }

    public void StopPatrolling()
    {
        isPatrolling = false;
    }
}
