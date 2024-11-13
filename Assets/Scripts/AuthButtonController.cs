using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AuthButtonController : MonoBehaviour
{
    private SceneNav sceneNav;

    void Start()
    {
        // if (sceneNav == null)
        // {
        //     sceneNav = GameObject.Find("Canvas").GetComponent<SceneNav>();
        // }
        // string expiresAtString = PlayerPrefs.GetString("expires_at");
        
        // // Don't check for an active session if there is none
        // if (expiresAtString == null) {
        //     return;
        // }

        // // If the user has already started a session previously and has a valid access token, log them in automatically
        // try
        // {
        //     DateTime expires_at = DateTime.Parse(expiresAtString);
        //     Debug.Log($"Parsed DateTime: {expires_at}");
            
        //     // If the session hasn't expired yet, check if the access token is still valid
        //     DateTime sgTimeZone = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        //     // Check if the session has expired
        //     if (sgTimeZone > expires_at)
        //     {
        //         Debug.Log("Session exists but has expired.");
        //         return;
        //     }
        //     Debug.Log("Session is still active.");
        // }
        // catch (Exception e)
        // {
        //     Debug.Log(e.Message);
        // }

        // // Checks passed, log in the user
        // sceneNav = GameObject.Find("Canvas").GetComponent<SceneNav>();
        // sceneNav.loadMainMenu();
    }

    public void StartLogin()
    {
        StartCoroutine(Login());
    }

    public void StartCreateAccount()
    {
        StartCoroutine(CreateAccount());
    }

    public void StartSendOTP()
    {
        StartCoroutine(SendOTPCoroutine());
    }

    public void StartPWReset()
    {
        StartCoroutine(UserClient.SendPwRequestCoroutine());
    }

    public void StartAccActivation()
    {
        StartCoroutine(UserClient.ActivateAccRequestCoroutine());
    }
    
    public IEnumerator Login()
    {
        // Check if the AuthClient instance exists and start the Login coroutine
        yield return StartCoroutine(AuthClient.Login());
    }

    public IEnumerator CreateAccount()
    {
        Debug.Log("Here");
        yield return StartCoroutine(UserClient.CreateUserCoroutine());
    }

    public IEnumerator SendOTPCoroutine()
    {
        yield return StartCoroutine(UserClient.SendOTPEmailCoroutine());
    }
}
