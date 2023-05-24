using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject endGameUI;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);

        //Cursor.visible = false;                           // W³¹czanie kursora 1
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.name == "Menu")
        {
            gameOverUI.SetActive(true);
        }

        if(gameOverUI.activeInHierarchy || endGameUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }
        else
        {
            Cursor.visible = false;                           // W³¹czanie kursora 2
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainManu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
