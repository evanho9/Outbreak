using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -5f;
    public float score = 0;
    public Text scoreText;
  
    // Start is called before the first frame update
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
    }

    // Update is called once per frame
    void Update()
    {
      scrollSpeed -= 0.005f;
      if (gameOver && Input.GetMouseButtonDown(0)) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
    
    public void PlayerDied() 
    {
      gameOverText.SetActive(true);
      gameOver = true;
    }
    
    public void AddScore(float add)
    {
      score += add;
      UpdateScore();
    }
    
    void UpdateScore()
    {
      int scoreRounded = (int)score;
      scoreText.text = "Score: " + scoreRounded.ToString();
    }
}
