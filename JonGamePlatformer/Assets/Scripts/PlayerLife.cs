using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    PlayerMovement pm;
    PalyerCombat pc;
    CharacterController2D cc;
    public GameManagerScript gameManager;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;
    public AudioClip hitClip;
    public AudioClip deathClip;

    bool onGround;
    bool wasOnGround;
    bool wasFalling;
    float startOfFall;

    public float minFall = 30f;

    AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        pc = GetComponent<PalyerCombat>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

       
    }
    void FixedUpdate()
    {
        onGround = cc.ReturnGrounded();
        if(!wasFalling && isFalling) { startOfFall = transform.position.y; }
        if (!wasOnGround && onGround)
        {
            TakeFallDamage();
        }
        if(pm.isLader)
        {
            onGround = true;
        }

        wasOnGround = onGround;
        wasFalling = isFalling;
    }

    bool isFalling { get { return (!onGround && rb.velocity.y < 0); } }

    /// 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Invoke("Disappear", 0.1f);
        }

    }
    ///
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            
            ChangeHealth(-1);
            if (currentHealth <= 0)
            {
                Die();
            }

        }
        if (other.gameObject.CompareTag("Trap2"))
        {
            ChangeHealth(-2);
            
            if (currentHealth <= 0)
            {
                Die();
            }


        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            ChangeHealth(-2);
           
            if (currentHealth <= 0)
            {
                Die();
                
            }

        }
        if (other.gameObject.CompareTag("Void"))
        {
            ChangeHealth(-5);
            
            if (currentHealth <= 0)
            {
                DieVoid();

            }

        }

    }
    ///
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        HealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if(currentHealth>0)
        {
            anim.SetTrigger("TakeHit");
            PlayClip(hitClip);

        }
        
        
    }


    void Die()
    {

        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        pm.enabled = false;
        pc.enabled = false;
        PlayClip(deathClip);

        Invoke("Disappear", 3f);
        Invoke("DisplayGameOver", 1f);

    }
    void DieVoid()
    {

        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        pm.enabled = false;
        pc.enabled = false;
        PlayClip(deathClip);

        Invoke("Disappear", 0.3f);
        Invoke("DisplayGameOver",1f);

    }
    public void DisplayGameOver()
    {
        gameManager.gameOver();
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
        
    }
    public void PlayClip(AudioClip clip)
    {
        audioSource.volume = 1f;
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(clip);
    }

    public void TakeFallDamage()
    {
        float fallDistance = startOfFall - transform.position.y;
        if (fallDistance > minFall)
        {
            ChangeHealth(-5);
            Debug.Log("Player fell " + (fallDistance) + " fall units ");
            if (currentHealth <= 0)
            {
                Die();

            }
        }
        
    }
    
}
