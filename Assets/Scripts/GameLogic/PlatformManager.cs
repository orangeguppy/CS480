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
                Debug.Log("Show upgradeMenu");
                buildManager.ShowUpgradeMenu(this); // Show upgrade UI if turret exists
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

        // Build the selected turret using the BuildManager
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
    }

    public void UpgradeTurret()
    {
        if (turret == null)
        {
            Debug.Log("No turret to upgrade");
            return;
        }

        if (PlayerInfo.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("cant upgrade,broke");
            return;
        }

        PlayerInfo.Money -= turretBlueprint.upgradeCost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        //out with old
        Destroy(turret);

        //in with the new
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgrade, transform.position + platformOffset, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position + platformOffset, Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("lvlup");

        //HideMenu();
    }

}