using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    static GameStateManager gameInstance = null;

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
    void Awake () {
        if (gameInstance != null)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate Live Manager; Self Destructing!");
        }
        else
        {
            gameInstance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            GameObject.DontDestroyOnLoad(livesText);
            GameObject.DontDestroyOnLoad(scoreText);
        }
    }
}
