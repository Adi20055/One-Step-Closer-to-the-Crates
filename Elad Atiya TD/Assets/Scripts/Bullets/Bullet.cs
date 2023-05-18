using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Transform target;
    protected bool isInherited;
    public GameObject impactEffect;
    public string enemyTag = "Enemy";

    public float speed = 30f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
        transform.LookAt(target);
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

        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 15f).eulerAngles; //Lurp will allow smooth transition between targets
        //transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.LookAt(target);
    }

    virtual protected void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 7f);

        if (!isInherited)
        {
            Damage(target);
        }

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
