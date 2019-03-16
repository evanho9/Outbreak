﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{  
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
      
      if (GameControl.instance.gameOver) {
        rb2d.velocity = new Vector2(0, 0);
      }
    }
    
    void OnBecameInvisible()
    {
        Debug.Log("out of view");
        int randInt = (int)Random.Range(0, 3);
        GameControl.instance.SpawnTile(randInt);
        Destroy(this.gameObject);
    }
}
