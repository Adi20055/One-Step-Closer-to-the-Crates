using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataToSave
{
    public int highScore;
    //public int[] boardTurretStatus;
    //public int[] boardTurretUpgradeStatus;

    public DataToSave()
    {
        highScore = PlayerStats.Highscore;
        //for (int i = 0; i < Nodes.turretTypeArr.Length; i++)
        //{
        //    boardTurretStatus[i] = Nodes.turretTypeArr[i];
        //}
        //for (int i = 0; i < Nodes.turretUpgradeTypeArr.Length; i++)
        //{
        //    boardTurretUpgradeStatus[i] = Nodes.turretUpgradeTypeArr[i];
        //}
    }
}