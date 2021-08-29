using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    float health = 100;

    [SerializeField]
    float healthRegenPerSecond = 0.25f;

    [SerializeField]
    float healthRegenDelay = 0.5f;

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

    NavMeshAgent navMeshAgent;

    Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        bossAnimator = FindObjectOfType<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
                bossAnimator.SetBool("isRunning", true);
                navMeshAgent.destination = player.transform.position;
            }
            else
            {
                navMeshAgent.ResetPath();
                bossAnimator.SetBool("isRunning", false);

                if (Time.time > nextAttack)
                {
                    int randomAttackID = Random.Range(1, 4);
                    nextAttack = Time.time + randomAttackID + 2.5f;
                    bossAnimator.SetTrigger("Attack" + randomAttackID);
                }
            }
        }
        else
        {
            navMeshAgent.ResetPath();
            bossAnimator.SetBool("isRunning", false);
        }
    }

    public void HitDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            bossAnimator.SetBool("isDead", true);
            Destroy(gameObject, 5f);
        }
    }
}
