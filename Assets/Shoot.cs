using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce = 20f;


    public void StartShooting()
    {
        GameObject go = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = arrowSpawn.transform.forward * shootForce;
    }

}
