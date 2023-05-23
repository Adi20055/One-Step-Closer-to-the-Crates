using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataToSave
{
    public int highScore;

    public DataToSave()
    {
        highScore = PlayerStats.Highscore;
    }
}