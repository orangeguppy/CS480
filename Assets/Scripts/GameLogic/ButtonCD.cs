using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCD : MonoBehaviour
{
    public Button button;
    public float cooldown = 60f;

    public Image buttonBackground;
    private bool onCooldown = false;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        if (!onCooldown)
        {
            StartCoroutine(StartCD());
        }
    }

    IEnumerator StartCD()
    {
        onCooldown = true;

        button.interactable = false;

        float timeElapsed = 0f;
        
        while (timeElapsed < cooldown)
        {
            timeElapsed += Time.deltaTime;
            buttonBackground.fillAmount = timeElapsed / cooldown;

            yield return null;
        }

        buttonBackground.fillAmount = 0;
        button.interactable = true;
        onCooldown = false;  
    }
}
