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


    public void SetTurretToBuild(Blueprint turret)
    {
        turretToBuild = turret;
        if(selectedPlatform != null)
        {
            selectedPlatform.BuildTurret();
        }
    }

    public void SetSelectedPlatform(PlatformManager platform)
    {
        selectedPlatform = platform;
    }

    public void BuildOn(PlatformManager platform)
    {
        if(PlayerInfo.Money < turretToBuild.cost)
        {
            Debug.Log("broke no money");
            return;
        }

        PlayerInfo.Money -= turretToBuild.cost;
        Debug.Log(" Left $" + PlayerInfo.Money);
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, platform.transform.position + platformOffset, Quaternion.identity);
        platform.turret = turret;
    }
}
