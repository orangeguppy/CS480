using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject gameSettings;
    public GameObject accountSettings;
    public GameObject sendOTP;
    public GameObject resetPwPage;

    public void ShowScreen(GameObject toEnable, GameObject toDisable)
    {
        toEnable.SetActive(true);
        toDisable.SetActive(false);
    }

    public void ShowEmailSubmitScreen()
    {
        ShowScreen(sendOTP, accountSettings);
    }

    public void ShowPWResetScreen()
    {
        ShowScreen(resetPwPage, sendOTP);
    }
}