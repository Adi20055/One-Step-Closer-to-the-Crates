using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int turretID;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;
    [HideInInspector]
    public int upgradeID = 0;

    public int GetSellValue()
    {
        return cost / 2;
    }
}
