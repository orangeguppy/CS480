using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    BuildManager buildManager;

    public BuyManager buyUI; // buy UI

    [Header("Turret Prefabs")]
    public Blueprint gunner;
    public Blueprint zapper;
    public Blueprint bomber;
    public Blueprint flamethrower;

    [Header("Buttons")]
    public Button gunnerButton;
    public Button zapperButton;
    public Button bomberButton;
    public Button flamethrowerButton;

    [Header("Faded Button Settings")]
    public float fadeAlpha = 0.5f; // Set to how transparent you want the button to be when inactive

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    void Update()
    {

        UpdateButtonState(gunnerButton, PlayerInfo.Money >= gunner.cost);
        UpdateButtonState(zapperButton, PlayerInfo.Money >= zapper.cost);
        UpdateButtonState(bomberButton, PlayerInfo.Money >= bomber.cost);
        UpdateButtonState(flamethrowerButton, PlayerInfo.Money >= flamethrower.cost);
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

    public void BuyGunner()
    {
        if (PlayerInfo.Money >= gunner.cost)
        {
            buildManager.SetTurretToBuild(gunner);
            buildManager.DeselectPlatform();
        }
    }

    public void BuyZapper()
    {
        if (PlayerInfo.Money >= zapper.cost)
        {
            buildManager.SetTurretToBuild(zapper);
            buildManager.DeselectPlatform();
        }
    }

    public void BuyBomber()
    {
        if (PlayerInfo.Money >= bomber.cost)
        {
            buildManager.SetTurretToBuild(bomber);
            buildManager.DeselectPlatform();
        }
    }

    public void BuyFlamethrower()
    {
        if (PlayerInfo.Money >= flamethrower.cost)
        {
            buildManager.SetTurretToBuild(flamethrower);
            buildManager.DeselectPlatform();
        }
    }
}