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
}
