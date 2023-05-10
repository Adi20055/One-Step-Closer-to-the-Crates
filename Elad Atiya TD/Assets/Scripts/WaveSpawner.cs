using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;

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
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        ++waveIndex;

        for(int i = 0; i< waveIndex; i++)
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
