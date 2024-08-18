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
    
    public IEnumerator Login()
    {
        // Check if the AuthClient instance exists and start the Login coroutine
        yield return StartCoroutine(AuthClient.Login());
        sceneNav.loadMainMenu();
    }
}
