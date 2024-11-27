using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public SceneFader sceneFader;
    

    public void loadGame()
    {
        Time.timeScale = 1;
        sceneFader.FadeToScene("Game");
    }

    public void loadSettings()
    {
        Time.timeScale = 1;
        sceneFader.FadeToScene("Game");
    }

    public void loadLeaderboard()
    {
        Time.timeScale = 1;
        sceneFader.FadeToScene("Leaderboard");
    }

    public void loadMainMenu()
    {
        Time.timeScale = 1;
        sceneFader.FadeToScene("MainMenu");
    }

    public void loadEndless()
    {
        sceneFader.FadeToScene("Endless");
    }

    public void loadQuiz()
    {
        sceneFader.FadeToScene("Quiz");
    }

    public void loadGlossary()
    {
        sceneFader.FadeToScene("Glossary");
    }

    public void exitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
