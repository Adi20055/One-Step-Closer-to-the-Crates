using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : Turret
{
    public int damageOvertime = 30;
    public float slowPct = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float laserParticleOffset = 0f;

    // Update is called once per frame
    override protected void Update()
    {
        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }
        LockOnTarget();
        Laser();
    }

    void Laser()
    {
        target.GetComponent<Enemy>().TakeDamage(damageOvertime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * laserParticleOffset;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
}