using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOrLoadScore : MonoBehaviour
{
    public static void Save()
    {
        SaveSystem.SaveData();
    }

    public static bool Load()
    {
        DataToSave data =  SaveSystem.LoadData();
        if (data != null)
        {
            PlayerStats.Highscore = data.highScore;
            return true;
        }
        return false;
    }
}
