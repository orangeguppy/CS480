using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blueprint 
{
    public GameObject prefab;
    public int cost;

    public GameObject upgrade;
    public int upgradeCost;
    //public string type; //g,b,z,f
    //public int level; //lvl 1 or 2
}
