using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Transform target;
    protected int damage = 50;
    public GameObject impactEffect;
    public string enemyTag = "Enemy";

    public float speed = 30f;

    public void Seek(Transform _target)
    {
        target = _target;
        transform.LookAt(target);
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(target);
    }

    virtual protected void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 7f);

        Damage(target);

        Destroy(gameObject);

        return;
    }

    virtual protected void Damage(Transform enemyGo)
    {
        Enemy enemy = enemyGo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
