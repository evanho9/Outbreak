using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    
    public GameObject gameOverText;
    public Text ammoCounter;
    public bool gameOver = false;
    public bool isEasyMode;
    public bool isHardMode;
    
    public float startingScrollSpeed;
    public float scrollSpeed;
    public float scrollSpeedIncrement;
    public float maxScrollSpeed;
    
    public float score = 0;
    public Text scoreText;
    
    private GameObject player;
    public GameObject enemy;
    public GameObject coin;
    public GameObject fire;
    public GameObject ammo;
    public Weapon weapon;
    
    public GameObject building;
    public GameObject ground;
    
    private float enemySpawnTime = 2f;
    private bool lastSpawnedWasLava = false;
    private bool lastSpawnedWasLavaHard = false;
    public float spawnPositionOffset;
  
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
      
      player = GameObject.Find("Player");
      weapon = player.GetComponent<Weapon>();
    }
    
    void Start()
    {
      scrollSpeed = -startingScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
      if (scrollSpeed >= -maxScrollSpeed && !gameOver) {
        scrollSpeed -= scrollSpeedIncrement;
      }
      if (!gameOver)
        AddScore(1);
      if (gameOver && Input.GetKeyDown("space")) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
      if (gameOver && Input.GetKeyDown("x")) {
        SceneManager.LoadScene(1);
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
    
    
    public void UpdateAmmoCounter()
    {
      
      ammoCounter.text = "Ammo: " + weapon.ammo.ToString();
    }
    
    public void SpawnTile(int tileType)
    {
        if (!gameOver) {
          int randInt = (int)Random.Range(0, 10);
          if (randInt <= 5)
            SpawnCoin(tileType+1.5f);
          else if (randInt > 5 && randInt < 8)
            SpawnAmmo(tileType+1.5f);
          else
            SpawnCoinBlock(tileType+2f);
          if (tileType == 0)
            SpawnGround();
          else if (tileType == 1)
            SpawnBuilding();
          else if (tileType == 2)
            if (lastSpawnedWasLava) {
              int randInt2 = (int)Random.Range(0, 3);
              if (randInt2 == 0 || randInt2 == 1)
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
        float randFloat = Random.Range(-1.5f,-5);
        spawnPosition.y = randFloat;
        GameObject newBuilding = Instantiate(ground, spawnPosition, Quaternion.identity);
        newBuilding.name = "Ground";
        int randInt = (int)Random.Range(0, 6);
        if (randInt < 5)
          SpawnObstacle(2f);
      }
    }
    
    void SpawnGround()
    {
      if (!gameOver) {
        lastSpawnedWasLava = false;
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnPositionOffset;
        float randFloat = Random.Range(-4, -6);
        spawnPosition.y = randFloat;
        GameObject newGround = Instantiate(ground, spawnPosition, Quaternion.identity);
        newGround.name = "Ground";
        int randInt = (int)Random.Range(0, 6);
        if (randInt < 5)
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
        int randInt = (int)Random.Range(0, 10);
        if (randInt < 5)
          SpawnEnemy(yPos);
        else
          SpawnFire(yPos);
      }
    }
    
    void SpawnFire(float yPos)
    {
      if (!gameOver && !isEasyMode) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(0, -1);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        Instantiate(fire, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnEnemy(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(0, 3);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        Instantiate(enemy, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnCoin(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        Instantiate(coin, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnCoinBlock(float yPos) {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        int i = 0;
        while (i < 3) {
          spawnPosition.x += 1.25f;
          Instantiate(coin, spawnPosition, Quaternion.identity);
          spawnPosition.y -= 1.25f;
          Instantiate(coin, spawnPosition, Quaternion.identity);
          spawnPosition.y += 1.25f;
          i++;
        }
      }
    }
    
    void SpawnAmmo(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        Instantiate(ammo, spawnPosition, Quaternion.identity);
      }
    }
    
    public void SpawnHardTile(int tileType)
    {
      int rand = (int)Random.Range(0, 3);
      if (rand == 0)
          SpawnTile(tileType);
      else {
        if (!gameOver) {
          int randInt = (int)Random.Range(0, 10);
          if (randInt <= 5)
            SpawnHardCoin(tileType+1.5f);
          else if (randInt > 5 && randInt < 8)
            SpawnHardAmmo(tileType+1.5f);
          else
            SpawnCoinBlock(tileType+2f);
          if (tileType == 0)
            SpawnHardGround();
          else if (tileType == 1)
            SpawnHardBuilding();
          else if (tileType == 2)
            if (lastSpawnedWasLavaHard) {
              int randInt2 = (int)Random.Range(0, 3);
              if (randInt2 == 0 || randInt2 == 1)
                SpawnHardBuilding();
              else
                SpawnHardGround();
            }
            else
              SpawnHardLava();
        }
      }
    }
    
    void SpawnHardBuilding()
    {
      if (!gameOver) {
        lastSpawnedWasLavaHard = false;
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnPositionOffset;
        float randFloat = Random.Range(6, 8);
        spawnPosition.y = randFloat;
        GameObject newBuilding = Instantiate(ground, spawnPosition, Quaternion.identity);
        newBuilding.name = "Ground";
        int randInt = (int)Random.Range(0, 6);
        if (randInt < 5)
          SpawnHardObstacle(2f);
      }
    }
    
    void SpawnHardGround()
    {
      if (!gameOver) {
        lastSpawnedWasLavaHard = false;
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnPositionOffset;
        float randFloat = Random.Range(8, 10);
        spawnPosition.y = randFloat;
        GameObject newGround = Instantiate(ground, spawnPosition, Quaternion.identity);
        newGround.name = "Ground";
        int randInt = (int)Random.Range(0, 6);
        if (randInt < 5)
          SpawnHardObstacle(2f);
      }
    }
    
    void SpawnHardLava()
    {
      if (!gameOver) {
        lastSpawnedWasLavaHard = true;;

        Invoke("SpawnHardGround",0.4f);
      }
    }
    
    void SpawnHardObstacle(float yPos)
    {
      if (!gameOver) {
        int randInt = (int)Random.Range(0, 10);
        if (randInt < 5)
          SpawnHardEnemy(yPos);
        else
          SpawnHardFire(yPos);
      }
    }
    
    void SpawnHardFire(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(0, -1);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        Instantiate(fire, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().gravityScale = -50;
      }
    }
    
    void SpawnHardEnemy(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(0, 3);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos;
        Instantiate(enemy, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>().gravityScale = -50;
      }
    }
    
    void SpawnHardCoin(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        Instantiate(coin, spawnPosition, Quaternion.identity);
      }
    }
    
    void SpawnHardCoinBlock(float yPos) {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        int i = 0;
        while (i < 3) {
          spawnPosition.x += 1.25f;
          Instantiate(coin, spawnPosition, Quaternion.identity);
          spawnPosition.y -= 1.25f;
          Instantiate(coin, spawnPosition, Quaternion.identity);
          spawnPosition.y += 1.25f;
          i++;
        }
      }
    }
    
    void SpawnHardAmmo(float yPos)
    {
      if (!gameOver) {
        Vector3 spawnPosition = player.transform.position;
        int randFloat = Random.Range(-5, 5);
        spawnPosition.x += spawnPositionOffset + randFloat;
        spawnPosition.y = yPos-0.5f;
        Instantiate(ammo, spawnPosition, Quaternion.identity);
      }
    }
}
