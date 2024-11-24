using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQnManager : MonoBehaviour
{
    public GameObject question;

    public void CorrectAns()
    {
        question.SetActive(false);
        Time.timeScale = 1f; //unpause game
        //playerscore + 1
    }

    public void WrongAns()
    {
        question.SetActive(false);
        Time.timeScale = 1f; //unpause game
    }
}
