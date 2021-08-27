using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField]
    bool isPatrolling = true;

    [SerializeField]
    Transform moveToPosition;

    NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            navMeshAgent.destination = moveToPosition.position;
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
