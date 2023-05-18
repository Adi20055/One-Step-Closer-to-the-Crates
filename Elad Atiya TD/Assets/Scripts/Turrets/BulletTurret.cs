using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletTurret : Turret
{
    public GameObject bulletPrefab;

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

    protected void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
