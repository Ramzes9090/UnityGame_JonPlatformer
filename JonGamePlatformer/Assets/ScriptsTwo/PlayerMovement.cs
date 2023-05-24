using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{


    CharacterController2D controller;

    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    bool onGround = true;
    bool isDescent = false;

    public AudioClip jumpClip;
    Rigidbody2D rigidbody2d;
    
    public bool isLader = false;
   
    AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        
    }

    

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        onGround = controller.ReturnGrounded();


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("Jump");
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(jumpClip);
        }

        if (onGround)
        {
            animator.SetBool("OnGround", onGround);
        }
        else
        {
            animator.SetBool("OnGround", onGround);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetTrigger("Crouch");
            crouch = true;
        }
        //else if(controller.crouch == true)
        //{
        //    animator.SetBool("isCrouching", controller.crouch);
        //}
        
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //animator.SetBool("isCrouching", controller.crouch);


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
            isDescent = true;
            animator.SetBool("isDescent", isDescent);
            rigidbody2d.gravityScale = 1f;
            isLader = true;


        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
            isDescent = false;
            animator.SetBool("isDescent", isDescent);
            rigidbody2d.gravityScale = 3f;
            isLader = false;
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        if (onGround == true && Mathf.Abs(horizontalMove) > 0.1f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.02f, 0.1f);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
       


    }

    
}
