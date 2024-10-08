using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L3UpgradeManager : MonoBehaviour
{
    public GameObject finalUpgradeUI;

    private PlatformManager target;

    [Header("UI")]
    public Button upgrade1Button;
    public Button upgrade2Button;
    public Button sellButton;

    public TextMeshProUGUI upgrade1Cost;
    public TextMeshProUGUI upgrade2Cost;
    public TextMeshProUGUI sellAmt;


    [Header("Faded Button Settings")]
    public float fadeAlpha = 0.5f;

    void Update()
    {
        UpdateButtonState(upgrade1Button, PlayerInfo.Money >= target.turretBlueprint.stage3AUpgradeCost);
        UpdateButtonState(upgrade2Button, PlayerInfo.Money >= target.turretBlueprint.stage3BUpgradeCost);
    }

    public void TargetPlatform(PlatformManager target)
    {
        this.target = target;
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);
        transform.position = target.transform.position + platformOffset;

        upgrade1Cost.text = "$" + target.turretBlueprint.stage3AUpgradeCost;
        upgrade2Cost.text = "$" + target.turretBlueprint.stage3BUpgradeCost;
        sellAmt.text = "$" + target.turretBlueprint.SellAmount();

        finalUpgradeUI.SetActive(true);
    }

    public void HideUI()
    {
        finalUpgradeUI.SetActive(false);
    }

    void UpdateButtonState(Button button, bool canAfford)
    {
        ColorBlock colorBlock = button.colors;
        Color buttonColor = button.image.color;
        Text buttonText = button.GetComponentInChildren<Text>();

        if (canAfford)
        {
            // Fully visible if player can afford the turret
            SetButtonTransparency(button, buttonText, 1f);
            button.interactable = true;
        }
        else
        {
            // Fade out the button and text if the player can't afford the turret
            SetButtonTransparency(button, buttonText, fadeAlpha);
            button.interactable = false;
        }
    }

    void SetButtonTransparency(Button button, Text buttonText, float alpha)
    {
        // Fade the button's image
        Color imageColor = button.image.color;
        imageColor.a = alpha;
        button.image.color = imageColor;

        // Fade the button's text
        if (buttonText != null)
        {
            Color textColor = buttonText.color;
            textColor.a = alpha;
            buttonText.color = textColor;
        }
    }

    public void UpgradeA()
    {
        target.Stage3AUpgrade();
        Debug.Log("upgrad1e");
    }

    public void UpgradeB()
    {
        target.Stage3BUpgrade();
        Debug.Log("upgrad2e");
    }

    public void Sell()
    {
        target.Sell();
    }
}
