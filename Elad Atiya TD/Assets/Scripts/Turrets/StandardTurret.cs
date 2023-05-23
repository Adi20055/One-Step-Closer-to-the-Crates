using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : BulletTurret
{
    protected float TimeBetweenShots = 0.3f;
    protected int numberOfShots = 1;

    [HideInInspector]
    public int numberOfShotsArrayIndex = 0;

    [Header("Standard Turret stats")]
    public int[] NumberOfShotsUpgradeList = new int[3];

    override protected void Start()
    {
        numberOfShots = NumberOfShotsUpgradeList[0];
        base.Start();
    }

    override protected void Shoot()
    {
        TurretShoot();
        if (numberOfShots > 1)
        {
            for (int i = 1; i < numberOfShots; i++)
            {
                Invoke("TurretShoot", TimeBetweenShots * i);
            }
        }
    }

    private void TurretShoot()
    {
        base.Shoot();
    }

    override public bool upgradeTurret()
    {
        bool didUpgrade;
        didUpgrade = base.upgradeTurret();
        if (numberOfShotsArrayIndex < NumberOfShotsUpgradeList.Length - 1)
        {
            numberOfShots = NumberOfShotsUpgradeList[++numberOfShotsArrayIndex];

            return true;
        }
        return didUpgrade;
    }
}
