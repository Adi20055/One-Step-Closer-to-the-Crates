using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public static Node[] nodes;
    public static int nodeIndex;
    private Renderer rend;
    private Color startColor;

    //[HideInInspector]
    private GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isFullyUpgraded = false;
    //[HideInInspector]
    //public int turretType = -1;
    //[HideInInspector]
    //public int turretUpgradeType = -1;

    [Header("Unity Setup Fields")]
    public Vector3 positionOffset;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    BuildManager buildManager;

    void Awake()
    {
        nodeIndex = 0;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        //nodes[nodeIndex++] = this;
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

        GameObject buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        //if (turret.GetType() == typeof(StandardTurret))
        //{
        //    turretType = 1;
        //}
        //else if (turret.GetType() == typeof(MissileLauncher))
        //{
        //    turretType = 2;
        //}
        //else if (turret.GetType() == typeof(LaserBeamer))
        //{
        //    turretType = 3;
        //}
        //    turretUpgradeType = 0;
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

        if (turret.GetComponent<Turret>().upgradeTurret() == true) //If stat upgrade is successful
        {
            upgradeEffect = Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(upgradeEffect, 5f);
            //if (turretType != -1)
            //{
            //    ++turretUpgradeType;
            //}
            return;
        }

        //Remove the old turret
        Destroy(turret);

        //Build upgraded version
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        upgradeEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(upgradeEffect, 5f);

        isFullyUpgraded = true;
        //++turretUpgradeType;
    }

    public void SellTurret()
    {
        GameObject sellEffect;

        PlayerStats.Money += turretBlueprint.GetSellValue();
        sellEffect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);
        Destroy(turret);
        turretBlueprint = null;
        //turretUpgradeType = -1;
        //turretType = -1;
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

    //public int GetTurretStatus()
    //{
    //    return turretType;
    //}
    //public int GetTurretUpgradeStatus()
    //{
    //    return turretUpgradeType;
    //}

    //public void LoadTurret()
    //{
    //    if (turret != null)
    //    {
    //        PlayerStats.Money -= turretBlueprint.cost;
    //        SellTurret();
    //    }

    //    switch (turretType)
    //    {
    //        case 1:
    //            PlayerStats.Money += Shop.standardTurret.cost;
    //            BuildTurret(Shop.standardTurret);
    //            break;
    //        case 2:
    //            PlayerStats.Money += Shop.missileLauncher.cost;
    //            BuildTurret(Shop.missileLauncher);
    //            break;
    //        case 3:
    //            PlayerStats.Money += Shop.laserBeamer.cost;
    //            BuildTurret(Shop.laserBeamer);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
