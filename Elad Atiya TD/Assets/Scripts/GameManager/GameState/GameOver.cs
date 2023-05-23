using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Text roundsText;
    public Text highscoreNumberText;
    public Text highscoreText;

    private void OnEnable()
    {
        int score = PlayerStats.Rounds;

        roundsText.text = score.ToString();
        highscoreText.text = "Highscore";

        if (score > PlayerStats.Highscore)
        {
            PlayerStats.Highscore = score;
            DataOptions.Save();
            highscoreText.text = "Congratulations\nNew Highscore!!!";
        }
        highscoreNumberText.text = PlayerStats.Highscore.ToString();

        WaveSpawner.enemiesAlive++; //So wave spawning will stop
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu()
    {
        Debug.Log("Go to menu.");
    }
}
