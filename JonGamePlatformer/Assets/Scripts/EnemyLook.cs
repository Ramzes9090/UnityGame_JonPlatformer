using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    public EnemyPatrol patrol;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            patrol.Attack();
        }
            
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            patrol.ReturnToPatrol();
        }
    }
}
