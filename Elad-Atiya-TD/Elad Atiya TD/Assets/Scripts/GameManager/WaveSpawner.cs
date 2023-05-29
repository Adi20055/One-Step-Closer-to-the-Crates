using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive;
    public static int enemiesKilled;
    [Header("Unity setup fields")]
    public DataOptions dataOptions;

    [Header("Wave Spawner")]
    public Wave[] waves;
    public Transform spawnPoint;
    public Text waveCountdownText;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private static int waveIndex = 0;

    void Start()
    {
        enemiesAlive = 0;
        enemiesKilled = 0;
        waveIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:0.0}", countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        PlayerStats.Rounds = waveIndex;

        dataOptions.Save(); //automatically save game state

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        if(++waveIndex == waves.Length)
        {
            GameState.WinGame = true;
            this.enabled = false;
        }
    }

    static public void LoadWave ()
    {
        waveIndex = PlayerStats.Rounds;
    }

    static public void ResetWave ()
    {
        waveIndex = 0;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        ++enemiesAlive;
    }
}
