using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public Camera cam;

    public float attackRange = 5f;
    
    DamageDealer damage;

    private void Start()
    {
        damage = GetComponent<DamageDealer>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            if (hit.collider.transform.GetComponent<Player>())
            {
                hit.collider.transform.GetComponent<Player>().ReduceHealth(damage.GetDamage());
            }
        }
    }
}
