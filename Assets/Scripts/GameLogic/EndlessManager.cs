using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    public GameObject emailScreen;
    public void Email()
    {
        Time.timeScale = 0f; // freeze time loop
        emailScreen.SetActive(true);
    }

    public void Real()
    {
        Time.timeScale = 1f; // unfreeze time loop
        emailScreen.SetActive(false);
    }

    public void Fake()
    {
        Time.timeScale = 1f; // unfreeze time loop
        emailScreen.SetActive(false);
    }
}
