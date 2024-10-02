using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;

    void OnEnable()
    {
        waveNumberText.text = ((EndlessWS.waveNumber)/3).ToString();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //waveNumberText.text = ((EndlessWS.waveNumber) / 3).ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Game");
    }
}
