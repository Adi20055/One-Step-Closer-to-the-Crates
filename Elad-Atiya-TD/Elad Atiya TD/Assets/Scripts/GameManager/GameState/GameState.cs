using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public static bool WinGame;
    public static bool gameIsOver;

    public GameObject gameOverUI;
    public GameObject gameWonUI;
    public DataOptions dataOptions;

    void Start()
    {
        gameIsOver = false;
        WinGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
        if (WinGame && WaveSpawner.enemiesAlive <= 0)
        {
            WonGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
        dataOptions.ResetSaveData();
    }

    public void WonGame()
    {
        gameIsOver = true;
        gameWonUI.SetActive(true);
        dataOptions.ResetSaveData();
    }
}
