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
    
    public AudioClip mainTheme;
    
    public GameObject player;
    
    public GameObject building;
    public GameObject ground;
    
    public float spawnPositionOffset = 10;
  
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
      
      player = GameObject.Find("Player");
      
      building = GameObject.Find("Building");

      ground = GameObject.Find("Ground");
    }
    
    void Start()
    {
      //SoundManager.PlayRepeating(mainTheme);
    }

    // Update is called once per frame
    void Update()
    {
      if (scrollSpeed >= -7.5)
        scrollSpeed -= 0.005f;
      if (gameOver && Input.GetKeyDown("space")) {
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
    
    public void SpawnBuilding()
    {
      Vector3 spawnPosition = player.transform.position;
      spawnPosition.x += spawnPositionOffset;
      spawnPosition.y = -3;
      GameObject spawnedIn = Instantiate(ground, spawnPosition, Quaternion.identity);
    }
    
    public void SpawnGround()
    {
      Debug.Log("ground spawned");
      Vector3 spawnPosition = player.transform.position;
      spawnPosition.x += spawnPositionOffset;
      spawnPosition.y = -5;
      Instantiate(ground, spawnPosition, Quaternion.identity);
    }
}
