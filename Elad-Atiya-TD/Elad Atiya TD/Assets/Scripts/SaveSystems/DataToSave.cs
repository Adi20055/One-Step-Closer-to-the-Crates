using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataToSave
{
    public int highScore;
    public int currLives;
    public int currMoney;
    public int currRound;
    public int[] turretIDs;
    public int[] upgradeIDs;

    public DataToSave()
    {
        highScore = PlayerStats.Highscore;
        currLives = PlayerStats.Lives;
        currMoney = PlayerStats.Money;
        currRound = PlayerStats.Rounds;

        turretIDs = new int[NodeData.arraySize];
        upgradeIDs = new int[NodeData.arraySize];

        for (int i = 0; i < NodeData.arraySize; i++)
        {
            turretIDs[i] = NodeData.turretIDs[i];
            upgradeIDs[i] = NodeData.upgradeIDs[i];
        }
    }
}