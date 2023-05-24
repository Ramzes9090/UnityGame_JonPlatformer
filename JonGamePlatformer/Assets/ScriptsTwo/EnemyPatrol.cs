using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed; //prêdkoœæ poruszania
    bool movingRight = true; //czy porusza siê w prawo
    bool moving = true; //czy porusza siê
    public Transform groundDetection; //obiekt, którym bêdziemy sprawdzaæ czy dotykamy
    
    public bool flip = false;

    Animator animator; //komponent animator
    

   

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        animator.SetBool("IsRunning", moving);
        if (moving)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.1f);

            if (groundInfo.collider == false || flip == true)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    movingRight = false;
                    flip = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                    flip = false;
                }
            }
        }
    }
    public void Attack()
    {
        moving = false;
        animator.SetTrigger("Attacking1");
    }
    public void ReturnToPatrol()
    {
        moving = true;
    }
}
