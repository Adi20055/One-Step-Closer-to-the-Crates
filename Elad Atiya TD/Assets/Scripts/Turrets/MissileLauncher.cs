using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : BulletTurret
{
    protected int explosionRadius = 5;

    [HideInInspector]
    public int explosionRadiusArrayIndex = 0;

    [Header("Missile Launcher")]
    public int[] ExplosionRadiusUpgradeList = new int[3];

    protected override void Start()
    {
        explosionRadius = ExplosionRadiusUpgradeList[0];
        base.Start();
    }

    override public bool upgradeTurret()
    {
        bool didUpgrade;
        didUpgrade = base.upgradeTurret();
        if (explosionRadiusArrayIndex < ExplosionRadiusUpgradeList.Length - 1)
        {
            explosionRadius = ExplosionRadiusUpgradeList[++explosionRadiusArrayIndex];

            return true;
        }
        return didUpgrade;
    }

    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Missile missile = bulletGO.GetComponent<Missile>();

        if (missile != null)
        {
            missile.Seek(target);
            missile.SetDamage(damage);
            missile.SetExplosionRadius(explosionRadius);
        }
    }
}
