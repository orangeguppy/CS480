using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("2 buildmanagers");
        }
        instance = this;
    } // singleton

    private Blueprint turretToBuild;
    private PlatformManager selectedPlatform; // Store the platform on which we want to build the turret
    
    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;

    public L2UpgradeManager L2upgradeUI; // upgrade UI
    public L3UpgradeManager L3upgradeUI;
    public BuyManager buyUI; // buy UI
    public SellManager SellUI;


    public void SetTurretToBuild(Blueprint turret)
    {
        turretToBuild = turret;
        if (selectedPlatform != null)
        {
            selectedPlatform.BuildTurret();
        }
    }

    public void SetSelectedPlatform(PlatformManager platform)
    {

        
        selectedPlatform = platform;
    }

    public bool IsPlatformSelected(PlatformManager platform)
    {
        return selectedPlatform == platform;
    }

    public void DeselectPlatform()
    {
        selectedPlatform = null;
        buyUI.HideUI();
        L2upgradeUI.HideUI();
        L3upgradeUI.HideUI();
        SellUI.HideUI();
    }

    public void ShowUpgradeMenu(PlatformManager platform)
    {
        SetSelectedPlatform(platform);
        L2upgradeUI.TargetPlatform(platform); // Position the upgrade menu correctly
        buyUI.HideUI(); // Hide the buy menu when showing the upgrade menu
        L3upgradeUI.HideUI();
        SellUI.HideUI();
    }

    // Show the buy UI if no turret exists
    public void ShowBuyMenu(PlatformManager platform)
    {
        SetSelectedPlatform(platform);
        buyUI.TargetPlatform(platform); // Position the buy menu correctly
        L2upgradeUI.HideUI(); // Hide the upgrade menu when showing the buy menu
        L3upgradeUI.HideUI();
        SellUI.HideUI();
    }

    public void ShowFinalUpgradeMenu(PlatformManager platform)
    {
        SetSelectedPlatform(platform);
        L3upgradeUI.TargetPlatform(platform); // Position the upgrade menu correctly
        buyUI.HideUI(); // Hide the buy menu when showing the upgrade menu
        L2upgradeUI.HideUI();
        SellUI.HideUI();
    }

    public void ShowSellMenu(PlatformManager platform)
    {
        SetSelectedPlatform(platform);
        SellUI.TargetPlatform(platform);
        buyUI.HideUI();
        L2upgradeUI.HideUI();
        L3upgradeUI.HideUI();
    }

    public Blueprint GetTurret()
    {
        return turretToBuild;
    }

}