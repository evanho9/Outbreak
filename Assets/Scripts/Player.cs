﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upForce = 300f;
  
    private bool isDead = false;
    private bool isGrounded = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float startingX;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      rb2d.velocity = new Vector2(0, 0);
      anim.SetTrigger("Run"); 
      startingX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
      //isGrounded = Physics2D.OverlapCircle(GroundCheck1.position,0.15f, groundLayer);
      rb2d.velocity = new Vector2(0, rb2d.velocity.y);
      if (this.transform.position.x < startingX-0.1) {
        anim.SetTrigger("onwall");
      }
      if (!isDead) {  
        //Jump
        if (Input.GetKeyDown("space") && isGrounded) {
          isGrounded = false;
          rb2d.AddForce(new Vector2(0, upForce));
          anim.SetTrigger("Jump");
        }
        if (Input.GetKeyDown("x")) { 
          Weapon weapon = GetComponent<Weapon>();
          if (weapon != null) {
            weapon.Attack(false);
          }
        }
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision) 
    {
      //Player collides with ground
      if ((collision.gameObject.name == "Ground" || collision.gameObject.name == "SpawnGround" 
      || collision.gameObject.name == "SpawnGround2" || collision.gameObject.name == "Building"
      || collision.gameObject.name == "Coin" || collision.gameObject.name == "Ammo")) {
        isGrounded = true;
      } else {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.PlayerDied();
      }
    }
    
    void OnBecameInvisible()
    {
      isDead = true;
      anim.SetTrigger("Die");
      GameControl.instance.PlayerDied();
    }
}
