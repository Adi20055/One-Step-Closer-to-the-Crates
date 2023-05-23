using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject enemyTexture;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        enemyTexture.transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        target = Waypoints.waypoints[++wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        --WaveSpawner.enemiesAlive;
        Destroy(gameObject);
    }


}
