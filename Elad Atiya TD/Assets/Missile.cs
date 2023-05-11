using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    public float explosionRadius = 0f;
    void OnEnable()
    {
        isInherited = true;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (target == null)
        {
            FindNewTarget();
        }
        base.Update();
    }

    void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                Seek(enemy.transform);
            }
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    protected override void HitTarget()
    {
        Explode();
        base.HitTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
