using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        waveNumberText.text = ((EndlessWS.waveNumber)/3).ToString() + " Waves Cleared!";
        scoreText.text = "Score: " + (PlayerInfo.EndlessScore).ToString();
    }
}
