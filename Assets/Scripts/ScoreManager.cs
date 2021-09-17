using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        int currentScore = Int32.Parse(scoreText.text);
        if(currentScore != SpawnManager.score) {
            scoreText.text = SpawnManager.score.ToString();
        }
    }
}
