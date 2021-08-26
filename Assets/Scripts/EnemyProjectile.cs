using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    Vector3 moveDirection;

    Player player;

    Rigidbody rigidBody;

    DamageDealer damage;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        damage = GetComponent<DamageDealer>();
        player = FindObjectOfType<Player>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rigidBody.velocity = moveDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Player>())
        {
            Destroy(this.gameObject);
            collision.transform.GetComponent<Player>().ReduceHealth(damage.GetDamage());
        }
    }
}
