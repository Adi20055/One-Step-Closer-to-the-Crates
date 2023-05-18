using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletTurret : Turret
{
    private int damage = 0;
    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;

    [Header("Bullet Stats")]
    public int[] DamageArray = new int[3];

    override protected void Start()
    {
        damage = DamageArray[arrayIndex];
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
        if (!base.upgradeTurret())
        {
            return false;
        }
        if (arrayIndex < DamageArray.Length)
        {
            damage = DamageArray[arrayIndex];

            return true;
        }
        return false;
    }

    protected void Shoot()
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
