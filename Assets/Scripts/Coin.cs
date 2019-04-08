﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem scoreEmitter;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
  
    void Awake() 
    {
      scoreEmitter = GameObject.Find("ScoreEmitter").GetComponent<ParticleSystem>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      circleCollider = GetComponent<CircleCollider2D>();
    }
  
    void OnTriggerEnter2D(Collider2D other) 
    {
      if ((other.gameObject.name == "Player")) {
        if (!GameControl.instance.gameOver) {
          GameControl.instance.AddScore(100f);
          scoreEmitter.Play();
          spriteRenderer.enabled = false;
          circleCollider.enabled = false;
          Invoke("DestroyItself", 1f);
        }
      }   
    }
    
    void DestroyItself() {
      Destroy(this.gameObject);
    }
}
