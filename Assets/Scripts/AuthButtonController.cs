using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthButtonController : MonoBehaviour
{
    private SceneNav sceneNav;

    void Start()
    {
        if (sceneNav == null)
        {
            sceneNav = GameObject.Find("Canvas").GetComponent<SceneNav>();
        }
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
