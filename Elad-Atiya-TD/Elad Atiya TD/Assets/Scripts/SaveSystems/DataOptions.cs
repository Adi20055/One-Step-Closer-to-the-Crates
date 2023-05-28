using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOptions : MonoBehaviour
{
    public PlayerStats playerStats;

    void Start()
    {
        Load();
    }

    public static void Save()
    {
        SaveSystem.SaveData();
    }

    public void Load()
    {
        DataToSave data =  SaveSystem.LoadData();
        if (data == null)
        {
            return;
        }
        PlayerStats.Highscore = data.highScore;
        PlayerStats.Lives = data.currLives;
        PlayerStats.Money = data.currMoney;
        PlayerStats.Rounds = data.currRound;
        WaveSpawner.LoadWave();
    }
    public static void ResetHighscoreSaveData()
    {
        PlayerStats.Highscore = 0;

        SaveSystem.SaveData();
    }

    public void ResetSaveData()
    {
        playerStats.resetPlayerStats();
        WaveSpawner.ResetWave();

        SaveSystem.SaveData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            ResetHighscoreSaveData();
            Debug.Log("Highscore Save Data Has Been Erased");
        }
    }
}
