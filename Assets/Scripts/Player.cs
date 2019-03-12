﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upForce = 10f;
  
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
      anim.SetTrigger("Run"); 
    }

    // Update is called once per frame
    void Update()
    {
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, rb2d.velocity.y);
      if (!isDead) {  
        //Jump
        GameControl.instance.AddScore(0.01f);
        if (Input.GetMouseButtonDown(0)) {
          Debug.Log("jumped");
          rb2d.AddForce(new Vector2(0, upForce));
          //rb2d.velocity = new Vector2(rb2d.velocity.x, upForce); 
          //rb2d.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
          anim.SetTrigger("Jump");
        }
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
      //Player collides with ground
      if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Ground 2") {
        Debug.Log("touched the ground");
      } else {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.PlayerDied();
      }
    }
}
