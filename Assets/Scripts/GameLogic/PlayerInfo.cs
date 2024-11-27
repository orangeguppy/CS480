using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public Image Hearts;
    public static int LessonScore;
    public static int EndlessScore;

    [Header("Player stats")]
    public int startingMoney = 500;
    public int startingLives = 20;
    private int startingScore = 0;

    [Header("ui")]
    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI livesUI;


    void Start()
    {
        Money = startingMoney;
        Lives = startingLives;
        LessonScore = startingScore;
        EndlessScore = startingScore;
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = Money.ToString();
        livesUI.text = Lives.ToString();
        Hearts.fillAmount = (float)Lives / startingLives;
    }

}
