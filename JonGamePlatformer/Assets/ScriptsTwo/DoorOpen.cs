using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator anim;
    public PreasurePlate pp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pp.openTheDoor)
        {
            GetComponent<Collider2D>().enabled = false;
            anim.SetBool("OpenDoor", true);
            pp.openTheDoor = false;
        }
        // add this if you want close the door, when player does not staying on the pressure plate 
        /*
        else if(!pp.openTheDoor)
        {
            anim.SetBool("OpenDoor", false);
            GetComponent<Collider2D>().enabled = true;

        }
        */
    }
}
