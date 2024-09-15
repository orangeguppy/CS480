using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    BuildManager buildManager;

    [Header("Turret Prefabs")]
    public Blueprint gunner;
    public Blueprint zapper;
    public Blueprint bomber;
    public Blueprint flamethrower;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void BuyGunner()
    {
        Debug.Log("G");
        buildManager.SetTurretToBuild(gunner);
    }

    public void BuyZapper()
    {
        Debug.Log("Z");
        buildManager.SetTurretToBuild(zapper);
    }

    public void BuyBomber()
    {
        Debug.Log("B");
        buildManager.SetTurretToBuild(bomber);
    }

    public void BuyFlamethrower()
    {
        Debug.Log("F");
        buildManager.SetTurretToBuild(flamethrower);
    }
}
