using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    public GameObject deathEffect;
    public Image healthBar;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float startHealth = 100;
    private float health;
    public int moneyGain = 50;


    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

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
        --WaveSpawner.enemiesAlive;
        ++WaveSpawner.enemiesKilled;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
