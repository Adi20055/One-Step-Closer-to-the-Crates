using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;

    public float speed = 10f;
    public int health = 100;
    public int moneyGain = 50;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        target = Waypoints.waypoints[++wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += moneyGain;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
