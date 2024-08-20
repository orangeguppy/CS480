using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
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

    public void loadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void loadRegister()
    {
        Debug.Log("Register here");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void loadPasswordReset()
    {
        Debug.Log("reset pw here");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void sendOTP()
    {
        Debug.Log("send otp pepega");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void registerAccount()
    {
        Debug.Log("send register details ");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


    public void exitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
