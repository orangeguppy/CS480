using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void loadSettings()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void loadLeaderboard()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
