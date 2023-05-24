using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalyerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;


    public float attackRate = 2f;
    float nextAttackTime = 0f;


    int coins = 0;

    [SerializeField] private Text Money;
    [SerializeField] private Text MoneyEnd;

    public AudioClip fightClip;
    public AudioClip coinClip;


    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                audioSource.volume = Random.Range(0.2f, 0.07f);
                audioSource.pitch = Random.Range(0.7f, 1.3f);
                audioSource.PlayOneShot(fightClip);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Treasure"))
        {
            Destroy(collision.gameObject);
            coins++;
            Money.text = "Money = " + coins;
            MoneyEnd.text = "Level complete!\r\nYour score: " + coins + " / 6";
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(coinClip);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
   
    
   
}
