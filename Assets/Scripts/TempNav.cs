using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempNav : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Game");
    }
}
