using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI scoreText;
    public int HighScore;

    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void OnEnable()
    {
        Debug.Log("Here");
        waveNumberText.text = ((EndlessWS.waveNumber)/3).ToString() + " Waves Cleared!";
        scoreText.text = "Score: " + (PlayerInfo.EndlessScore).ToString();

        StartCoroutine(HandleOnEnable());
    }

    public void UpdateHighScore()
    {
        if (HighScore < PlayerInfo.EndlessScore)
        {
            HighScore = Player.EndlessScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
    }
}
