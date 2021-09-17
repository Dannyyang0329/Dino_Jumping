using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string winner;
    public static bool isGameOver = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject playerStatus1;
    public GameObject playerStatus2;


    private PlayerController controller1;
    private PlayerController controller2;

    private ScoreManager scoreManager;    
    private SpawnManager spawnManager;
    
    

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        spawnManager = FindObjectOfType<SpawnManager>();

        FindObjectOfType<AudioManager>().Play("Bgm");

        controller1 = player1.GetComponent<PlayerController>();
        controller2 = player2.GetComponent<PlayerController>();

        if(MainMenu.isOnePlayer) {
            player2.SetActive(false);
            playerStatus2.SetActive(false);
        }
    }


    private void Update()
    {
        if(controller1.isDead && !isGameOver) {
            isGameOver = true;
            winner = "Red Dino";

            controller1.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40000);
            controller1.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            controller1.gameObject.GetComponent<Animator>().SetBool("isGameOver", true);
            FindObjectOfType<AudioManager>().Play("GameOver");

            scoreManager.enabled = false;
            spawnManager.enabled = false;

            Invoke("GameOver", 2f);
        }

        if(MainMenu.isTwoPlayers && controller2.isDead && !isGameOver) {
            isGameOver = true;
            winner = "Blue Dino";

            controller2.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40000);
            controller2.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            controller2.gameObject.GetComponent<Animator>().SetBool("isGameOver", true);
            FindObjectOfType<AudioManager>().Play("GameOver");

            scoreManager.enabled = false;
            spawnManager.enabled = false;

            Invoke("GameOver", 2f);
        }
    }


    private void GameOver()
    {
        if(MainMenu.isOnePlayer) {
            SceneManager.LoadScene("GameOver1");
        }
        if(MainMenu.isTwoPlayers) {
            SceneManager.LoadScene("GameOver2");
        }
    }
}
