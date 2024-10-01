using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    public GameObject turret;
    public GameObject buildMenu; //UI


    private static GameObject currentOpenMenu = null;
    private static PlatformManager selectedPlatform = null;

    [Header("Menu Adj")]
    public float heightOffset = 0.2f;
    public float xOffset = 0f;
    public float zOffset = 0f;


    // Start is called before the first frame update
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
        if (turret != null)
        {
            Debug.Log("nobuild");
            return;
        }


        //Set platform 
        BuildManager.instance.SetSelectedPlatform(this);

        //Close menu if alr open
        if (currentOpenMenu == buildMenu && selectedPlatform == this)
        {
            buildMenu.SetActive(false);
            currentOpenMenu = null;
            selectedPlatform = null;
        }
        else
        {
           
            if (currentOpenMenu != null)
            {
                currentOpenMenu.SetActive(false);
            }

            buildMenu.SetActive(true);

            // Adjust the menu's position
            Vector3 offset = new Vector3(xOffset, heightOffset, zOffset);
            Vector3 newPosition = transform.position + offset;
            //newPosition.y += heightOffset;
            //newPosition.x += xOffset;
            //newPosition.z += zOffset;

            // Set the menu's new position
            buildMenu.transform.position = newPosition;

            // Update the currently active menu and selected platform
            currentOpenMenu = buildMenu;
            selectedPlatform = this;
            
        }
    }

    public void BuildTurret()
    {
        if (turret != null)
        {
            Debug.Log("Turret alr here");
            return;
        }

        BuildManager.instance.BuildOn(this);
        currentOpenMenu.SetActive(false); // Hide the menu after building the turret
        currentOpenMenu = null;
        selectedPlatform = null;
    }


}
