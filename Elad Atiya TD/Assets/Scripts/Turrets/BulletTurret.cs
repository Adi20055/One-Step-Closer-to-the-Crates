using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletTurret : Turret
{
    protected int damage = 0;
    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;
    [HideInInspector]
    public int damageArrayIndex = 0;

    [Header("Bullet Stats")]
    public int[] DamageUpgradeList = new int[3];

    override protected void Start()
    {
        damage = DamageUpgradeList[0];
        base.Start();
    }

    override protected void Update()
    {
        if (target == null)
        {
            fireCountdown -= Time.deltaTime;
            return;
        }

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    override public bool upgradeTurret()
    {
        bool didUpgrade;
        didUpgrade =  base.upgradeTurret();
        if (damageArrayIndex < DamageUpgradeList.Length - 1)
        {
            damage = DamageUpgradeList[++damageArrayIndex];

            return true;
        }
        return didUpgrade;
    }

    virtual protected void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
            bullet.SetDamage(damage);
        }
    }
}
