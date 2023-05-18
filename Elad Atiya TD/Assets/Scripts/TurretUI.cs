using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    private Node target;

    public GameObject ui;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectTurret();
    }
}
