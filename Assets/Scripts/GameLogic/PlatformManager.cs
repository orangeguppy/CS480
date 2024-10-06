using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    public GameObject turret;
    public Blueprint turretBlueprint;
    public bool isUpgradable;


    BuildManager buildManager;

    [Header("Menu Adj")]
    public Vector3 platformOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        platformOffset = new Vector3(0f, 0.1f, 0f);
    }

    void OnMouseEnter()
    {
        if (turret == null)
        {
            rend.material.color = hoverColor; // Only highlight when buildable
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        // If the platform is already selected, deselect it and hide the menus
        if (buildManager.IsPlatformSelected(this))
        {
            Debug.Log("Hide Menu");
            buildManager.DeselectPlatform(); // Use DeselectPlatform to hide the menus
        }
        else
        {
            // Set the selected platform to ensure correct positioning of the menus
            buildManager.SetSelectedPlatform(this);

            // Show the appropriate menu based on whether a turret exists
            if (turret != null)
            {
                if (turretBlueprint.level == 1)
                {
                    // Show stage 2 upgrade menu if the turret is level 1
                    Debug.Log("Show stage 2 upgrade menu");
                    buildManager.ShowUpgradeMenu(this); // Show level 2 upgrade menu
                }
                else if (turretBlueprint.level == 2)
                {
                    // Show stage 3 upgrade menu if the turret is level 2
                    Debug.Log("Show stage 3 upgrade menu");
                    buildManager.ShowFinalUpgradeMenu(this); // Show level 3 upgrade menu
                }
                else if (turretBlueprint.level >= 3)
                {
                    //show sell menu
                    buildManager.ShowSellMenu(this);
                }
            }
            else
            {
                Debug.Log("Show buyMenu");
                buildManager.ShowBuyMenu(this); // Show buy UI if no turret exists
            }
        }
    }


    public void BuildTurret()
    {
        if (turret != null)
        {
            Debug.Log("Turret already here");
            return;
        }

        Build(buildManager.GetTurret());

    }

    public void Build(Blueprint blueprint)
    {
        if (PlayerInfo.Money < blueprint.cost)
        {
            Debug.Log("broke no money");
            return;
        }

        PlayerInfo.Money -= blueprint.cost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position + platformOffset, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);
        turretBlueprint.level = 1;
    }

    public void Stage2Upgrade()
    {
        if (turret == null)
        {
            Debug.Log("No turret to upgrade");
            return;
        }

        if (PlayerInfo.Money < turretBlueprint.stage2UpgradeCost)
        {
            Debug.Log("cant upgrade,broke");
            return;
        }

        PlayerInfo.Money -= turretBlueprint.stage2UpgradeCost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        //out with old
        Destroy(turret);

        //in with the new
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.stage2, transform.position + platformOffset, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("STG2");
        turretBlueprint.level = 2;

        buildManager.DeselectPlatform();
    }

    public void Sell()
    {
        Debug.Log("sell turr");
        PlayerInfo.Money += turretBlueprint.SellAmount();

        Destroy(turret);
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);

        turretBlueprint = null;
        buildManager.DeselectPlatform();
    }

    public void Stage3AUpgrade()
    {
        if (turret == null)
        {
            Debug.Log("No turret to upgrade");
            return;
        }

        if (PlayerInfo.Money < turretBlueprint.stage3AUpgradeCost)
        {
            Debug.Log("cant upgrade,broke");
            return;
        }

        PlayerInfo.Money -= turretBlueprint.stage3AUpgradeCost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        //out with old
        Destroy(turret);

        //in with the new
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.stage3A, transform.position + platformOffset, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("3A");
        turretBlueprint.level = 3;

        buildManager.DeselectPlatform();
    }

    public void Stage3BUpgrade()
    {
        if (turret == null)
        {
            Debug.Log("No turret to upgrade");
            return;
        }

        if (PlayerInfo.Money < turretBlueprint.stage3BUpgradeCost)
        {
            Debug.Log("cant upgrade,broke");
            return;
        }

        PlayerInfo.Money -= turretBlueprint.stage3BUpgradeCost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        //out with old
        Destroy(turret);

        //in with the new
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.stage3B, transform.position + platformOffset, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("3B");
        turretBlueprint.level = 4;

        buildManager.DeselectPlatform();
    }
}