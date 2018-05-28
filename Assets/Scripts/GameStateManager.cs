using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;

    void Start() {
        lives = 3;
        score = 0;
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        Debug.Log("Lives: " + lives);
        Debug.Log("Score: " + score);
    }
}
