using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOptions : MonoBehaviour
{
    public PlayerStats playerStats;
    public NodeData nodeData;

    void Start()
    {
        Load();
    }

    public void Save()
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

        for (int i = 0; i < nodeData.nodes.Length; i++) //Load nodeData
        {
            nodeData.nodes[i].LoadNode(data.turretIDs[i], data.upgradeIDs[i]);
        }

        nodeData.UpdateNodeData();
        Save();
    }

    public void ResetHighscoreSaveData()
    {
        PlayerStats.Highscore = 0;

        Save();
    }

    public void ResetSaveData()
    {
        playerStats.resetPlayerStats();
        WaveSpawner.ResetWave();

        for (int i = 0; i < nodeData.nodes.Length; i++) //Reset nodeData
        {
            NodeData.ResetIDs(i);
        }

        Save();
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
