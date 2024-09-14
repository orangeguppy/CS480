using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildManager : MonoBehaviour
{
    public GameObject menu; // Shared menu across all platforms
    public Color hoverColor;
    public float heightOffset = 0.2f;

    public float xOffset = 0f; // Manual offset on the x-axis
    public float zOffset = 0f; // Manual offset on the z-axis

    private Renderer rend;
    private Color startColor;

    private static GameObject currentOpenMenu; // Tracks the active menu instance
    private static BuildManager selectedPlatform; // Tracks the currently selected platform

    private GameObject turret;

    [Header("Build Prefabs")]
    public GameObject gunner;
    public GameObject zapper;
    public GameObject bomber;
    public GameObject flamer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        // Assign button click events to shared menu buttons
        AssignButtonEvents();
    }

    void OnMouseEnter()
    {
        if(turret == null)
        {
            rend.material.color = hoverColor; // only highlight when buildable
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Turret already present.");
            return;
        }

        if (menu != null)
        {
            // If the currently active menu is already open on this platform, close it
            if (currentOpenMenu == menu && selectedPlatform == this)
            {
                menu.SetActive(false);
                currentOpenMenu = null;
                selectedPlatform = null;
            }
            else
            {
                // Deactivate the currently active menu if it's open on another platform
                if (currentOpenMenu != null)
                {
                    currentOpenMenu.SetActive(false);
                }

                // Activate and position the shared menu
                menu.SetActive(true);

                // Adjust the menu's position to be above the platform
                Vector3 newPosition = transform.position;
                newPosition.y += heightOffset;

                // Manually adjust the x and z axes with the new offsets
                newPosition.x += xOffset;
                newPosition.z += zOffset;

                // Set the menu's new position
                menu.transform.position = newPosition;

                // Update the currently active menu and selected platform
                currentOpenMenu = menu;
                selectedPlatform = this;
            }
        }
        else
        {
            Debug.LogWarning("No menu found.");
        }
    }

    // Assign button click events for placing turrets
    private void AssignButtonEvents()
    {
        // Assuming buttons are direct children of the shared menu GameObject
        Button[] buttons = menu.GetComponentsInChildren<Button>();

        if (buttons.Length >= 4)
        {
            // Clear existing listeners to avoid multiple registrations
            buttons[0].onClick.RemoveAllListeners();
            buttons[1].onClick.RemoveAllListeners();
            buttons[2].onClick.RemoveAllListeners();
            buttons[3].onClick.RemoveAllListeners();

            // Assign each button to call PlaceTurret on the currently selected platform
            buttons[0].onClick.AddListener(() => selectedPlatform?.PlaceTurret(gunner));
            buttons[1].onClick.AddListener(() => selectedPlatform?.PlaceTurret(zapper));
            buttons[2].onClick.AddListener(() => selectedPlatform?.PlaceTurret(bomber));
            buttons[3].onClick.AddListener(() => selectedPlatform?.PlaceTurret(flamer));
        }
        else
        {
            Debug.LogWarning("Not enough buttons found. Ensure the shared menu has 4 buttons.");
        }
    }

    // Method to place a turret on the current platform
    private void PlaceTurret(GameObject turretPrefab)
    {
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);

        if (turretPrefab != null && turret == null) // Ensure the platform is empty
        {
            // Instantiate the selected turret on top of this platform
            turret = Instantiate(turretPrefab, transform.position + platformOffset, Quaternion.identity);
            menu.SetActive(false); // Close the shared menu after placing the turret
            currentOpenMenu = null; // Clear the current menu reference
            selectedPlatform = null; // Clear the selected platform reference
        }
    }
}

