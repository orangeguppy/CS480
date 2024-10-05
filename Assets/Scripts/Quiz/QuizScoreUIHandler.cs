using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuizScoreUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScoreUI;
    public int Score;
    public TextMeshProUGUI ScoreInteger;
    void Awake()
    {
        ScoreInteger.text = Score.ToString();
    }

    // Update is called once per frame

    public void showScoreUI()
    { 
        ScoreUI.SetActive(true); 
    }
    public void hideScoreUI()
    {
        ScoreUI.SetActive(false);
    }
}
