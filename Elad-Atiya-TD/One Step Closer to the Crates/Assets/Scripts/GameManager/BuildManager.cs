using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public TurretBlueprint turretToBuild;
    private Node selectedTurret;

    public static BuildManager instance;

    public TurretUI turretUI;
    public GameObject buildEffect;
    public GameObject sellEffect;
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

    void Start()
    {
        DeselectTurret();
        turretToBuild = null;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurret (Node turret, Vector3 range)
    {
        if (selectedTurret == turret)
        {
            DeselectTurret();
            return;
        }

        selectedTurret = turret;
        turretToBuild = null;

        turretUI.SetTarget(turret, range);
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
