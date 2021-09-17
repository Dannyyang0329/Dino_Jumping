using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public Image black;
    public float fadedTime;
    private float currentFadedTime = 0;

    public TMP_Text gameOverTitle;
    public TMP_Text score;
    public TMP_Text maxScore;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameOverBgm");

        if(MainMenu.isTwoPlayers) {
            gameOverTitle.text = GameManager.winner + " Win";
        }

        score.text = "Your score : " + (SpawnManager.score-1).ToString();
        maxScore.text = "Max score : " + PlayerPrefs.GetInt("maxScore", 0).ToString();
    }


    public void Restart()
    {
        GameManager.isGameOver = false;

        SceneFaded();
        Invoke("LoadGame", fadedTime+0.5f);
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void SceneFaded()
    {
        if(!black.enabled) black.enabled = true;

        if(currentFadedTime < fadedTime) {
            float transparent = (currentFadedTime/fadedTime);

            Color color = black.color;
            black.color = new Color(color.r, color.g, color.b, transparent);

            currentFadedTime += Time.deltaTime;
            Invoke("SceneFaded", Time.deltaTime);
        }
    }


    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }


    public void HoverSound() 
    {
        FindObjectOfType<AudioManager>().Play("HoverSound");
    } 
}
