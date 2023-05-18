using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int moneyGain = 50;


    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow (float Pct)
    {
        speed = startSpeed * (1f - Pct);
    }

    void Die()
    {
        PlayerStats.Money += moneyGain;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
