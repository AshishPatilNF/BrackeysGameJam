using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTarget : MonoBehaviour
{
    PatrolWayPoints patrolRegistration;
    // Start is called before the first frame update
    void Start()
    {
        patrolRegistration = FindObjectOfType<PatrolWayPoints>();
        patrolRegistration.AddWayPoint(gameObject.transform);
    }
}
