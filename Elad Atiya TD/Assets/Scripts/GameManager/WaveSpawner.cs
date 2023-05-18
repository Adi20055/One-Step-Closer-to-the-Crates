using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;

    public int enemyMultiplier = 1;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;
    private float cooldown = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:0.0}", countdown);
    }

    IEnumerator SpawnWave()
    {
        ++waveIndex;
        PlayerStats.Rounds++;
        int numberOfEnemies = waveIndex * enemyMultiplier;

        for(int i = 0; i< numberOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(cooldown);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
