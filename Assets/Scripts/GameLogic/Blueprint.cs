using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blueprint 
{
    public GameObject prefab;
    public int cost;

    public GameObject stage2;
    public int stage2UpgradeCost;
    

    public GameObject stage3A;
    public int stage3AUpgradeCost;

    public GameObject stage3B;
    public int stage3BUpgradeCost;
    [HideInInspector]
    public int level;

    public int SellAmount()
    {
        int totalCost = cost;

        switch (level)
        {
            case 2:
                totalCost += stage2UpgradeCost;
                break;
            case 3:
                totalCost += stage2UpgradeCost + stage3AUpgradeCost;
                break;
            case 4:
                totalCost += stage2UpgradeCost + stage3BUpgradeCost;
                break;
        }

        return totalCost / 2;
    }
}
