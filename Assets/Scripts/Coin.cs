using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem scoreEmitter;
    private Transform tranform;
    private CircleCollider2D circleCollider;
    private AudioSource source;
  
    void Awake() 
    {
      scoreEmitter = GameObject.Find("ScoreEmitter").GetComponent<ParticleSystem>();
      tranform = GetComponent<Transform>();
      circleCollider = GetComponent<CircleCollider2D>();
      source = GetComponent<AudioSource>();
    }
  
    void OnTriggerEnter2D(Collider2D other) 
    {
      if ((other.gameObject.name == "Player")) {
        if (!GameControl.instance.gameOver) {

          
          source.Play();
          GameControl.instance.AddScore(250f);
          scoreEmitter.Play();
          transform.localScale = new Vector3(0, 0, 0);
          circleCollider.enabled = false;
          Invoke("DestroyItself", 1f);
        }
      }   
    }
    
    void DestroyItself() {
      Destroy(this.gameObject);
    }
}
