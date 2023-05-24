using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    //Rigidbody2D rb;

    public int maxHealth = 100;
    int currentHealth;

    EnemyPatrol enemyPatrol;

    //[SerializeField] private Collider2D attackCollider;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

  

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0 )
        {
            Die();
        }

        enemyPatrol.flip = true;

    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        //rb.bodyType = RigidbodyType2D.Static;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<EnemyPatrol>().enabled = false;
    }
}
