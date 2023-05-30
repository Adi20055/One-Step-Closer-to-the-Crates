using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public string name;
    public GameObject prefab;
    public int turretID;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellValue()
    {
        return cost / 2;
    }
}
