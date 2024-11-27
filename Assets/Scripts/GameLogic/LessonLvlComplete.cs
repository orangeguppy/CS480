using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LessonLvlComplete : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int numQns;

    void OnEnable()
    {
        scoreText.text = (PlayerInfo.LessonScore).ToString() + "/" + numQns.ToString() + " correct answers!";
    }
}
