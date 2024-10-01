using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public GameObject window;
    public TextMeshProUGUI msgStatus;
    public TextMeshProUGUI msgBody;
    public Image box;
    public Sprite blueBox;
    public Sprite greenBox;
    public Sprite redBox;

    public void ShowPopup(string colour, string status, string body)
    {
        msgStatus.text = status;
        msgBody.text = body;
        switch (colour) {
            default:
                box.sprite = blueBox;
                break;
            case "green":
                box.sprite = greenBox;
                break;
            case "red":
                box.sprite = redBox;
                break;
        }
        Time.timeScale = 0f;
        window.SetActive(true);
    }

    public void closePopup()
    {
        Time.timeScale = 1f;
        window.SetActive(false);
    }
}