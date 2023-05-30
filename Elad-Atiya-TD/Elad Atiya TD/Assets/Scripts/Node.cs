using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint = null;
    [HideInInspector]
    public bool isFullyUpgraded = false;

    public int nodeID;
    public Shop shop;

    public Vector3 positionOffset;

    public Color hoverColor;
    public Color notEnoughMoneyColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectTurret(this);
            return;
        }
        else
        {
            buildManager.DeselectTurret();
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("not enough money to build that!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;

        NodeData.SetIDs(turretBlueprint.turretID, 0, nodeID);

        GameObject buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("not enough money to upgrade that!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        GameObject upgradeEffect;
        upgradeEffect = Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(upgradeEffect, 5f);

        TurretUpgradeTrigger();
        NodeData.SetIDs(turretBlueprint.turretID, turretBlueprint.upgradeID, nodeID);
    }

    public void SellTurret()
    {
        GameObject sellEffect;

        PlayerStats.Money += turretBlueprint.GetSellValue();
        sellEffect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);
        RemoveTurret();
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void AddTurret(int _turretID)
    {
        foreach (TurretBlueprint blueprint in shop.blueprints)
        {
            if (blueprint.turretID == _turretID)
            {
                GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
                turret = _turret;
                turretBlueprint = blueprint;
                return;
            }
        }
    }

    public void TurretUpgradeTrigger()
    {
        if(isFullyUpgraded)
        {
            return;
        }

        if (turret.GetComponent<Turret>().upgradeTurret() == true) //If stat upgrade successful
        {
            turretBlueprint.upgradeID++;
            NodeData.SetIDs(turretBlueprint.turretID, turretBlueprint.upgradeID, nodeID);
            Debug.Log("Turret on node " + nodeID + " is upgraded to lvl " + turretBlueprint.upgradeID);
            return;
        }

        //Remove the old turret
        Destroy(turret);

        //Build upgraded version
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;


        turretBlueprint.upgradeID++;
        NodeData.SetIDs(turretBlueprint.turretID, turretBlueprint.upgradeID, nodeID);
        Debug.Log("(Max lvl) Turret on node " + nodeID + " is upgraded to lvl " + turretBlueprint.upgradeID);
        isFullyUpgraded = true;
        return;
    }

    public void RemoveTurret()
    {
        turretBlueprint.upgradeID = 0;
        turretBlueprint.turretID = -1;
        if (turret == null)
        {
            return;
        }
        Destroy(turret);
        NodeData.ResetIDs(nodeID);
        turretBlueprint = null;
        isFullyUpgraded = false;
    }

    public void LoadNode(int turretID, int upgradeID)
    {
        RemoveTurret();
        if (turretID == -1)
        {
            return;
        }
        Debug.Log("upgrade to lvl: " + upgradeID);
        AddTurret(turretID);
        for (int i = 0; i < upgradeID; i++)
        {
            TurretUpgradeTrigger();
        }
    }
}
