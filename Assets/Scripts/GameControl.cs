﻿using System.Collections;
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
    
    public float scrollSpeed = -5f;
    
    public float score = 0;
    public Text scoreText;
    
    public GameObject player;
    public GameObject enemy;
    public GameObject coin;
    public GameObject fire;
    public GameObject ammo;
    public Weapon weapon;
    
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
      weapon = player.GetComponent<Weapon>();
    }
    
    void Start()
    {
      //InvokeRepeating("SpawnEnemy", enemySpawnTime, enemySpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
      if (scrollSpeed >= -15 && !gameOver) {
        scrollSpeed -= 0.0015f;
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
      if (!gameOver) {
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
}
