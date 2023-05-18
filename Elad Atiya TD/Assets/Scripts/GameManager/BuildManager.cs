using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBlueprint turretToBuild;
    private Node selectedTurret;

    public static BuildManager instance;

    public TurretUI turretUI;
    public GameObject buildEffect;
    public GameObject upgradeEffect;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurret (Node turret)
    {
        if (selectedTurret == turret)
        {
            DeselectTurret();
            return;
        }

        selectedTurret = turret;
        turretToBuild = null;

        turretUI.SetTarget(turret);
    }

    public void DeselectTurret()
    {
        selectedTurret = null;
        turretUI.Hide();
    }

   public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectTurret();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
