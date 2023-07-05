using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    private Node target;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellValue;

    public GameObject ui;
    public GameObject RangeIndicator;

    public void SetTarget(Node _target, Vector3 range)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        if (!target.isFullyUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellValue.text = "$" + target.turretBlueprint.GetSellValue();
        RangeIndicator.transform.localScale = range;

        ui.SetActive(true);
        ImageFollower.DeselectAllImages();
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

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectTurret();
    }
}
