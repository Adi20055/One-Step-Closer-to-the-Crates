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

    public DataToSave()
    {
        highScore = PlayerStats.Highscore;
        currLives = PlayerStats.Lives;
        currMoney = PlayerStats.Money;
        currRound = PlayerStats.Rounds;
    }
}