using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject levelFinishUI;
    private bool levelCompleted = false;
  

    void Start()
    {
        levelFinishUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (levelFinishUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        

        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }

    }

    private void CompleteLevel()
    {
        levelFinishUI.SetActive(true);
    }
   
  
   
}

