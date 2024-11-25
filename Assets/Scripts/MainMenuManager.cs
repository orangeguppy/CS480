using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject gameSettings;
    public GameObject accountSettings;
    public GameObject sendOTP;
    public GameObject resetPwPage;
    public GameObject teamsScreen;
    public GameObject departmentScreen;

    public TMP_InputField teamInput;
    public TMP_InputField deptInput;

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

    public void ShowTeamsScreen()
    {
        ShowScreen(teamsScreen, accountSettings);
    }

    public void ShowDepartmentScreen()
    {
        ShowScreen(departmentScreen, accountSettings);
    }

    public void JoinTeam()
    {
        string teamName = teamInput.text;

        if (!string.IsNullOrEmpty(teamName))
        {
            PlayerPrefs.SetString("Team", teamName); 
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Team empty");
        }
    }

    public void JoinDept()
    {
        string deptName = deptInput.text;

        if (!string.IsNullOrEmpty(deptName))
        {
            PlayerPrefs.SetString("Department", deptName);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Dept empty");
        }
    }
}