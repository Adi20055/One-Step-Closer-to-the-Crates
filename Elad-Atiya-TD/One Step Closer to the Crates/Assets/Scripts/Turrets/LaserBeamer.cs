using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : Turret
{
    protected int damageOvertime = 30;
    [HideInInspector]
    public int damageOverTimeArrayIndex = 0;

    [Header("Unity Setup Fields")]
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float laserParticleOffset = 0f;

    [Header("Laser Beamer Stats")]
    public int[] DamageOverTimeUpgradeList = new int[3];
    public float slowPct = .2f;

    protected override void Start()
    {
        damageOvertime = DamageOverTimeUpgradeList[0];
        base.Start();
    }

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

    override public bool upgradeTurret()
    {
        bool didUpgrade;
        didUpgrade = base.upgradeTurret();
        if (damageOverTimeArrayIndex < DamageOverTimeUpgradeList.Length - 1)
        {
            damageOvertime = DamageOverTimeUpgradeList[++damageOverTimeArrayIndex];

            return true;
        }
        return didUpgrade;
    }
}