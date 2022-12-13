using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 50f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Zombie is dead");
        //animator.SetTrigger("FallBack");
        //GetComponent<Animator>().enabled = false;
        //GetComponent<NavMeshAgent>().enabled = false;
        //Destroy(gameObject, 0.5f);
        Destroy(gameObject);
    }
}
