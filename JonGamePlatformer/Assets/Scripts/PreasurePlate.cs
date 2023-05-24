using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    public Vector3 orginalPos;
    bool moveBack = false;
    public bool openTheDoor = false;
    public float pressPlate = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        orginalPos = transform.position;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.Translate(0, -0.01f, 0);
            moveBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
            //GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveBack = true;
            collision.transform.parent = null;
            //GetComponent<SpriteRenderer>().color = Color.white;
           
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < (orginalPos.y - pressPlate))
        {
            openTheDoor = true;
        }
        /*
        if(moveBack)
        {
            if(transform.position.y < orginalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }

        }
        */
    }
}
