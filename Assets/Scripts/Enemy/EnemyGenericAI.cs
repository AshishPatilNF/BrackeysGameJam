using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenericAI : MonoBehaviour
{
    [SerializeField]
    bool isRanged = false;

    [SerializeField]
    float health = 100;

    [SerializeField]
    float maxHealth = 100;

    [SerializeField]
    float healthRegenPerSecond = 0.25f;

    [SerializeField]
    float healthRegenDelay = 0.5f;

    [SerializeField]
    float startRegenAfterAttackDelay = 5f;

    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float detectionDistance = 30f;

    [SerializeField]
    float fleeDistance = 30f;

    [SerializeField]
    float attackRange = 2f;

    [SerializeField]
    float attackDelay = 0.5f;
    
    [SerializeField]
    float turnSpeed = 1.5f;

    [SerializeField]
    GameObject enemyProjectile;
    
    [SerializeField]
    GameObject projectilePoint;

    [SerializeField]
    bool underAttack = false;

    float nextHealthRegen = 0;
    
    float nextAttack = 0;
    
    float distanceFromPlayer;

    float fleeHealth;

    Player player;

    EnemyPatrolling enemyPatrolling;

    Coroutine underAttackRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyPatrolling = GetComponent<EnemyPatrolling>();
        fleeHealth = maxHealth * 0.25f;

        if (isRanged)
            attackRange = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (Mathf.Abs(player.transform.position.y - transform.position.y) < 0.5f)
            {
                if (health > fleeHealth)
                {
                    PlayerDetectionAndAttack();
                }

                if (health <= fleeHealth && distanceFromPlayer <= fleeDistance)
                {
                    FleeFromPlayer();
                }

                if (!underAttack && health < maxHealth && Time.time > nextHealthRegen)
                {
                    nextHealthRegen = Time.time + healthRegenDelay;
                    health += healthRegenPerSecond;
                }

                if (underAttack)
                {
                    if (underAttackRoutine == null)
                    {
                        underAttackRoutine = StartCoroutine(AfterUnderAttackRoutine());
                    }
                }
            }
            else if (Mathf.Abs(player.transform.position.y - transform.position.y) > 1f)
            {
                Debug.Log(distanceFromPlayer);

                if (distanceFromPlayer <= fleeDistance)
                    FleeFromPlayer();
            }

            /*if (distanceFromPlayer >= fleeDistance)
                enemyPatrolling.StartPatrolling();*/
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos * -1, Vector3.up);
        rotation.x = 0;
        transform.rotation = rotation;
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }

    private void PlayerDetectionAndAttack()
    {
        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation.x = 0;

        if (distanceFromPlayer < detectionDistance)
        {
            enemyPatrolling.StopPatrolling();
            //transform.rotation = rotation;
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
                    if (isRanged)
                        Instantiate(enemyProjectile, projectilePoint.transform.position, Quaternion.identity);
                    else
                        Debug.Log("Enemy Melee Attacking");
                    nextAttack = Time.time + attackDelay;
                }
            }
        }
        else
        {
            enemyPatrolling.StartPatrolling();
        }
    }

    //Enemy take damage and die new update
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy is taking damage");
        IamUnderAttack();
        if (health <= 0)
        {
            Debug.Log("Enemy has died");
            Destroy(gameObject);
        }
    }
    private void IamUnderAttack()
    {
        underAttack = true;
    }

    IEnumerator AfterUnderAttackRoutine()
    {
        yield return new WaitForSecondsRealtime(startRegenAfterAttackDelay);
        underAttack = false;
        underAttackRoutine = null;
    }
}
