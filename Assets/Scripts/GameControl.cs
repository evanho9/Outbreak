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
    public GameObject coin;
    public GameObject fire;
    
    public GameObject building;
    public GameObject ground;
    
    private float enemySpawnTime = 2f;
    private bool lastSpawnedWasLava = false;
    private float spawnPositionOffset = 25f;
  
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
      if (scrollSpeed >= -15 && !gameOver) {
        scrollSpeed -= 0.002f;
      }
      if (!gameOver)
        AddScore(1);
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
      int scoreRounded = (int)score * 100;
      scoreText.text = "Score: " + score.ToString();
    }
    
    public void SpawnTile(int tileType)
    {
      if (!gameOver) {
        SpawnCoin(tileType-0.5f);
        if (tileType == 0)
          SpawnGround();
        else if (tileType == 1)
          SpawnBuilding();
        else if (tileType == 2)
          if (lastSpawnedWasLava) {
            int randInt = (int)Random.Range(0, 3);
            if (randInt == 0 || randInt == 1)
              SpawnBuilding();
            else
              SpawnGround();
          }
          else
            SpawnLava();
      }
    }
    
    void SpawnBuilding()
    {
      if (!gameOver) {
        lastSpawnedWasLava = false;
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnPositionOffset;
        spawnPosition.y = -4;
        GameObject newBuilding = Instantiate(ground, spawnPosition, Quaternion.identity);
        newBuilding.name = "Ground";
        int randInt = (int)Random.Range(0, 3);
        if (randInt == 0 || randInt == 1)
          SpawnObstacle(2f);
      }
    }
    
    void SpawnGround()
    {
      if (!gameOver) {
        lastSpawnedWasLava = false;
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnPositionOffset;
        spawnPosition.y = -5.001f;
        GameObject newGround = Instantiate(ground, spawnPosition, Quaternion.identity);
        newGround.name = "Ground";
        int randInt = (int)Random.Range(0, 3);
        if (randInt == 0 || randInt == 1)
          SpawnObstacle(2f);
      }
    }
    
    void SpawnLava()
    {
      if (!gameOver) {
        lastSpawnedWasLava = true;;

        Invoke("SpawnGround",0.4f);
      }
    }
    
    void SpawnObstacle(float yPos)
    {
      if (!gameOver) {
        int rand = Random.Range(0,3);
        if (rand == 0 || rand ==1)
          SpawnEnemy(yPos);
        else
          SpawnFire(yPos);
      }
    }
    
    void SpawnFire(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-2, 0);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        GameObject newEnemy = Instantiate(fire, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnEnemy(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(0, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnCoin(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        GameObject newEnemy = Instantiate(coin, spawnPosition, Quaternion.identity);
      }
    }
}
