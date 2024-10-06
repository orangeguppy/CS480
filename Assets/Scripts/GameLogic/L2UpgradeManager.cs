using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L2UpgradeManager : MonoBehaviour
{
    public GameObject upgradeMenuUI;

    private PlatformManager target;

    [Header("UI")]
    public Button upgradeButton;
    public Button sellButton;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI sellAmt;

    [Header("Faded Button Settings")]
    public float fadeAlpha = 0.5f;

    void Update()
    {
        UpdateButtonState(upgradeButton, PlayerInfo.Money >= target.turretBlueprint.stage2UpgradeCost);
    }

    public void TargetPlatform(PlatformManager target)
    {
        this.target = target;
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);
        transform.position = target.transform.position + platformOffset;

        upgradeCost.text = "$" + target.turretBlueprint.stage2UpgradeCost;
        sellAmt.text = "$" + target.turretBlueprint.SellAmount();

        upgradeMenuUI.SetActive(true);  
    }

    public void HideUI()
    {
        upgradeMenuUI.SetActive(false);
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

    public void Upgrade()
    {
        target.Stage2Upgrade();
    }

    public void Sell()
    {
        target.Sell();
    }
}
