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
    
    public GameObject building;
    public GameObject ground;
  
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
      building = GameObject.Find("Building");
      building.SetActive(false);

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
    
    public void SpawnBuilding(float x)
    {
      Instantiate(building, new Vector3(x,0,0), transform.rotation);
    }
    
    public void ShowBuilding()
    {
      building.SetActive(true);
    }
    
    public void HideBuilding()
    {
      building.SetActive(false);
    }
    
    public void ShowGround()
    {
      ground.SetActive(true);
    }
    
    public void HideGround()
    {
      ground.SetActive(false);
    }
}
