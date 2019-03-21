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
    
    public GameObject player;
    public GameObject enemy;
    
    public GameObject building;
    public GameObject ground;
    
    private float enemySpawnTime = 2f;
    private bool lastSpawnedWasLava = false;
    private float spawnPositionOffset = 17.5f;
  
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
      //InvokeRepeating("SpawnEnemy", enemySpawnTime, enemySpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
      if (scrollSpeed >= -20)
        scrollSpeed -= 0.002f;
      if (gameOver && Input.GetKeyDown("space")) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
      if (Input.GetKeyDown("escape")) {
        Application.Quit();
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
        if (lastSpawnedWasLava) {
          int randInt = (int)Random.Range(0, 2);
          if (randInt == 0)
            SpawnGround();
          else
            SpawnBuilding();
        }
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
      SpawnEnemy(2f);
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
      SpawnEnemy(0f);
    }
    
    void SpawnLava()
    {
      Debug.Log("lava spawned");
      lastSpawnedWasLava = true;;

      Invoke("SpawnGround",0.4f);
    }
    
    void SpawnEnemy(float yPos)
    {
      Debug.Log("enemy spawned");
      Vector3 spawnPosition = player.transform.position;
      int randFloat = Random.Range(0, 5);
      spawnPosition.x += spawnPositionOffset + randFloat;
      spawnPosition.y = yPos;
      GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
