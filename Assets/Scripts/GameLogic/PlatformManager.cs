using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    public GameObject turret;
    public GameObject buildMenu; // UI for building turrets
    public GameObject upgradeMenu; // UI for upgrading turrets

    private static GameObject currentOpenMenu = null;
    private static PlatformManager selectedPlatform = null;

    [Header("Menu Adj")]
    public float heightOffset = 0.2f;
    public float xOffset = 0f;
    public float zOffset = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
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
        // If a turret exists, show the upgrade menu
        if (turret != null)
        {
            HandleMenu(upgradeMenu);
        }
        else
        {
            HandleMenu(buildMenu);
        }

        // Set the current selected platform in BuildManager
        BuildManager.instance.SetSelectedPlatform(this);
    }

    private void HandleMenu(GameObject menu)
    {
        if (currentOpenMenu == menu && selectedPlatform == this)
        {
            menu.SetActive(false);
            currentOpenMenu = null;
            selectedPlatform = null;
        }
        else
        {
            if (currentOpenMenu != null)
            {
                currentOpenMenu.SetActive(false);
            }

            menu.SetActive(true);
            SetMenuPosition(menu);

            currentOpenMenu = menu;
            selectedPlatform = this;
        }
    }

    private void SetMenuPosition(GameObject menu)
    {
        Vector3 offset = new Vector3(xOffset, heightOffset, zOffset);
        Vector3 newPosition = transform.position + offset;
        menu.transform.position = newPosition;
    }

    public void BuildTurret()
    {
        if (turret != null)
        {
            Debug.Log("Turret already here");
            return;
        }

        // Build the selected turret using the BuildManager
        BuildManager.instance.BuildOn(this);

        HideMenu();
    }

    public void UpgradeTurret()
    {
        if (turret == null)
        {
            Debug.Log("No turret to upgrade");
            return;
        }

        // Your upgrade logic here

        HideMenu();
    }

    private void HideMenu()
    {
        if (currentOpenMenu != null)
        {
            currentOpenMenu.SetActive(false);
            currentOpenMenu = null;
            selectedPlatform = null;
        }
    }
}
