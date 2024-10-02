using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject gameOverScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
