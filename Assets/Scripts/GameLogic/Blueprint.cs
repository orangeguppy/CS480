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
    public int level; //lvl 1 or 2

    public GameObject stage3A;
    public int stage3AUpgradeCost;

    public GameObject stage3B;
    public int stage3BUpgradeCost;
}
