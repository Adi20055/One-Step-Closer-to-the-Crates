using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("not enough money to upgrade that!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Remove the old turret
        Destroy(turret);

        //Build upgraded version
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
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
}
