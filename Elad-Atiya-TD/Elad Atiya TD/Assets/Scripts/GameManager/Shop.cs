using UnityEngine;

public class Shop : MonoBehaviour
{
    //public TurretBlueprint standardTurret;
    //public TurretBlueprint missileLauncher;
    //public TurretBlueprint laserBeamer;
    public TurretBlueprint[] blueprints;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    //public void SelectStandardTurret()
    //{
    //    buildManager.SelectTurretToBuild(standardTurret);
    //}
    //public void SelectMissileLauncher()
    //{
    //    buildManager.SelectTurretToBuild(missileLauncher);
    //}
    //public void SelectLaserBeamer()
    //{
    //    buildManager.SelectTurretToBuild(laserBeamer);
    //}
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(blueprints[0]);
    }
    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(blueprints[1]);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(blueprints[2]);
    }

}
