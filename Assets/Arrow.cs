using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody mybody;
    private float lifeTimer = 2f; 
    private float timer;
    private bool hitSomething = false;
    public float damage;
    BoxCollider boxCollider;




    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        mybody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(mybody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTimer)
        {
            Destroy(gameObject);
        }


        if(!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(mybody.velocity);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
        if (collision.transform.GetComponent<Player>())
        {
            Destroy(gameObject);
            collision.transform.GetComponent<Player>().ReduceHealth(damage);
        }

        if(collision.transform.GetComponent<EnemyGenericAI>())
        {
            Destroy(gameObject);
            collision.transform.GetComponent<EnemyGenericAI>().TakeDamage(damage);
        }
    }

    private void Stick()
    {
        mybody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
