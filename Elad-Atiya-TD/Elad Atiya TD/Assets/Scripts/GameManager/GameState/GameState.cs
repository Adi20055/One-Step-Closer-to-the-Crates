using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public static bool GameHasWon;
    public static bool gameIsOver;

    public GameObject gameOverUI;
    public GameObject gameWonUI;
    public DataOptions dataOptions;

    void Start()
    {
        gameIsOver = false;
        GameHasWon = false;
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
        if (GameHasWon && WaveSpawner.enemiesAlive <= 0)
        {
            WonGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;

        gameOverUI.SetActive(true);
    }

    public void WonGame()
    {
        gameIsOver = true;

        gameWonUI.SetActive(true);
    }
}
