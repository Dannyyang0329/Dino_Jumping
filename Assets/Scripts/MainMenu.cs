using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool isOnePlayer = false;
    public static bool isTwoPlayers = false;

    public Image black;
    public float fadedTime;
    private float currentFadedTime = 0;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("StartBgm");
    }


    public void OnePlayer()
    {
        isOnePlayer = true;
        isTwoPlayers = false;

        SceneFaded();
        Invoke("LoadGame", fadedTime+0.5f);
    }


    public void TwoPlayers()
    {
        isOnePlayer = false;
        isTwoPlayers = true;

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
