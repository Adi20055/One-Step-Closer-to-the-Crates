using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private bool gameIsOver = false;

    public GameObject gameOverUI;

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
    }

    void EndGame()
    {
        gameIsOver = true;

        gameOverUI.SetActive(true);
    }
}
