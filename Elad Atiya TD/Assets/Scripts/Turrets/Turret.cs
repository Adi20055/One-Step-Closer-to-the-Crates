using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    private float turnSpeed = 10f;
    protected Transform target;
    protected Enemy targetEnemy;
    [HideInInspector]
    public int arrayIndex = 0;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;

    [Header("Stats")]
    protected float range = 0;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public int[] RangeArr = new int[3];

    // Start is called before the first frame update
    virtual protected void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f); //Calls the UpdateTarget function every 0.5 seconds
        range = RangeArr[arrayIndex];
    }

    protected void UpdateTarget()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= range) //If turret already has a target, there's no need to search for another target
        {
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        LockOnTarget();
    }

    virtual public bool upgradeTurret()
    {
        if (arrayIndex < RangeArr.Length - 1)
        {
            range = RangeArr[++arrayIndex];

            return true;
        }
        return false;
    }

    protected void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //Lurp will allow smooth transition between targets
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}