using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOptions : MonoBehaviour
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
    public static void ResetSaveData()
    {
        PlayerStats.Highscore = 0;

        SaveSystem.SaveData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            ResetSaveData();
            Debug.Log("All Save Data Has Been Erased");
        }
    }
}
