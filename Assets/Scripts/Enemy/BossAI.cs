using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    float health = 100;

    [SerializeField]
    float healthRegenPerSecond = 0.25f;

    [SerializeField]
    float healthRegenDelay = 0.5f;

    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float detectionDistance = 30f;
    
    [SerializeField]
    float attackRange = 2f;

    [SerializeField]
    float attackDelay = 0.5f;

    [SerializeField]
    float turnSpeed = 1.5f;

    float nextAttack = 0;

    float nextHealthRegen = 0;

    float distanceFromPlayer;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            PlayerDetectionAndAttack();
            
            if (Time.time > nextHealthRegen)
            {
                nextHealthRegen = Time.time + healthRegenDelay;
                health += healthRegenPerSecond;
            }
        }
    }

    private void PlayerDetectionAndAttack()
    {
        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation.x = 0;

        if (distanceFromPlayer < detectionDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            if (distanceFromPlayer > attackRange)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
            else
            {
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + attackDelay;
                }
            }
        }
    }
}
