using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    [SerializeField] private GameObject emailScreen;
    private EmailManager emailManager;

    private void Start()
    {
        // Make sure EmailManager exists and is cached
        emailManager = emailScreen.GetComponent<EmailManager>();
        if (emailManager == null)
        {
            Debug.LogError("[EndlessManager] EmailManager not found on emailScreen!");
        }
    }

    public void Email()
    {
        Debug.Log("[EndlessManager] Opening email screen");
        if (emailManager != null)
        {
            Time.timeScale = 0f;
            emailScreen.SetActive(true);
            emailManager.LoadNewEmail(); // Load new email when screen is opened
        }
    }

    public void Real()
    {
        Time.timeScale = 1f;
        emailScreen.SetActive(false);
    }

    public void Fake()
    {
        Time.timeScale = 1f;
        emailScreen.SetActive(false);
    }
}