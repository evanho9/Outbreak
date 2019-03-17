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
    
    private bool lastSpawnedWasLava = false;
    public float spawnPositionOffset = 20;
  
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
      
      player = GameObject.Find("Player");
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
    
    public void SpawnTile(int tileType)
    {
      if (tileType == 0)
        SpawnGround();
      else if (tileType == 1)
        SpawnBuilding();
      else if (tileType == 2)
        if (lastSpawnedWasLava)
          SpawnGround();
        else
          SpawnLava();
    }
    
    void SpawnBuilding()
    {
      Debug.Log("building spawned");
      lastSpawnedWasLava = false;
      Vector3 spawnPosition = player.transform.position;
      spawnPosition.x += spawnPositionOffset;
      spawnPosition.y = -3;
      GameObject newBuilding = Instantiate(ground, spawnPosition, Quaternion.identity);
      newBuilding.name = "Ground";
    }
    
    void SpawnGround()
    {
      Debug.Log("ground spawned");
      lastSpawnedWasLava = false;
      Vector3 spawnPosition = player.transform.position;
      spawnPosition.x += spawnPositionOffset;
      spawnPosition.y = -5;
      GameObject newGround = Instantiate(ground, spawnPosition, Quaternion.identity);
      newGround.name = "Ground";
    }
    
    void SpawnLava()
    {
      Debug.Log("lava spawned");
      lastSpawnedWasLava = true;;
      Vector3 spawnPosition = player.transform.position;
      spawnPosition.x += spawnPositionOffset;
      spawnPosition.y = -5;
      GameObject newLava = Instantiate(ground, spawnPosition, Quaternion.identity);
      newLava.name = "Lava";
      newLava.GetComponent<SpriteRenderer>().enabled = false;
    }
}
