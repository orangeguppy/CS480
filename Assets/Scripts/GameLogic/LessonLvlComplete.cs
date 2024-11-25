using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LessonLvlComplete : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        scoreText.text = (PlayerInfo.LessonScore).ToString() + "/3 correct answers!";
    }
}
