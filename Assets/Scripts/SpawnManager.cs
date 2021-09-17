using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Item")]
    public GameObject[] stairs;

    [Header("Spawn Bound")]
    public float leftBound;
    public float rightBound;

    [Header("Delay")]
    public float topDelay;
    public float lowerDelay;
    public float decreaseTime;
    public float decreaseRequirement;

    private float spawnDelay;

    [Header("Speed")]
    public float lowerSpeed;
    public float topSpeed;
    public float incrementSpeed;

    private float currentSpeed;

    [Header("Grade")]
    public static int score;
    public int level;



    private void Start()
    {
        spawnDelay = topDelay;
        currentSpeed = lowerSpeed;

        score = 0;
        level = 0;

        Invoke("SpawnStair", spawnDelay);
    }


    private void Update()
    {
        if(score > PlayerPrefs.GetInt("maxScore", 0)) {
            PlayerPrefs.SetInt("maxScore", score);
        }
    }


    private void SpawnStair() 
    {
        score++;

        if(score / decreaseRequirement >= level) {
            Upgrade();
        }

        int index = Random.Range(0, stairs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(leftBound, rightBound), -50, 0 );
        Instantiate(stairs[index], spawnPos, stairs[index].transform.rotation);

        if(!GameManager.isGameOver) Invoke("SpawnStair", spawnDelay);
    }


    private void Upgrade()
    {
        level++;

        if(spawnDelay-decreaseTime >= lowerDelay) {
            spawnDelay -= decreaseTime;
        }
        if(currentSpeed+incrementSpeed <= topSpeed) {
            currentSpeed += incrementSpeed;
        }
    }


    public float GetSpeed()
    {
        return currentSpeed;
    } 
}
