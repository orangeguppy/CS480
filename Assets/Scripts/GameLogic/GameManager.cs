using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        
        if (gameOver)
        {
            return;
        }
        
        if(PlayerInfo.Lives <= 0)
        {
            EndGame();
        }
        
    }

    void EndGame()
    {
        Debug.Log("gg no re");
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void TogglePause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);

        if(pauseScreen.activeSelf)
        {
            Time.timeScale = 0f; // freeze time loop
        }
        else
        {
            Time.timeScale = 1f; //reset
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
