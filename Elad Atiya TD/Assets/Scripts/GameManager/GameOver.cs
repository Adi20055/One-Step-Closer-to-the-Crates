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
        highscoreNumberText.text = PlayerStats.Highscore.ToString();
        highscoreText.text = "Highscore";

        if (score > PlayerStats.Highscore)
        {
            PlayerStats.Highscore = score;
            SaveOrLoadScore.Save();
            highscoreText.text = "Congratulations\nNew Highscore!!!";
        }
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
