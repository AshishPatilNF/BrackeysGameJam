using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootActivator : MonoBehaviour
{
    Shoot shoot;

    void Start()
    {
        shoot =  FindObjectOfType<Shoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shoot.StartShooting();
        }
    }
}
