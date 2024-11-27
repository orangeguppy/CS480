using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;

    public SceneFader sceneFader;

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
        sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeToScene("Game");
    }

    public void Quiz()
    {
        Time.timeScale = 1f;
        string currScene = SceneManager.GetActiveScene().name;

        switch (currScene)
        {
            case "Level1":
                ModuleData.CurrentSubcategory = "email_web";
                break;
            case "Level2":
                ModuleData.CurrentSubcategory = "social_engineering";
                break;
            case "Level3":
                ModuleData.CurrentSubcategory = "BEC_and_quishing";
                break;
            case "Level4":
                ModuleData.CurrentSubcategory = "Auth";
                break;
            case "Level5":
                ModuleData.CurrentSubcategory = "SSRF";
                break;
            default:
                Debug.LogError("Invalid level for quiz transition");
                return;
        }
        sceneFader.FadeToScene("Quiz");
    }

    public void RedoLesson()
    {
        string currScene = SceneManager.GetActiveScene().name;
        if (currScene == "Level1")
        {
            sceneFader.FadeToScene("Lesson1");
        } 
        else if (currScene == "Level2")
        {
            sceneFader.FadeToScene("Lesson2");
        }
        else if (currScene == "Level3")
        {
            sceneFader.FadeToScene("Lesson3");
        }
        else if (currScene == "Level4")
        {
            sceneFader.FadeToScene("Lesson4");
        }
        else if (currScene == "Level5")
        {
            sceneFader.FadeToScene("Lesson5");
        }
    }
}
