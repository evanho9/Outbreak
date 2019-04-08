using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private ParticleSystem scoreEmitter;
    private Transform transform;
    private PolygonCollider2D polygonCollider;
    private Weapon weapon;
    private AudioSource source;
  
    void Awake() 
    {
      weapon = GameObject.Find("Player").GetComponent<Weapon>();
      scoreEmitter = GameObject.Find("ScoreEmitter").GetComponent<ParticleSystem>();
      transform = GetComponent<Transform>();
      polygonCollider = GetComponent<PolygonCollider2D>();
      source = GetComponent<AudioSource>();
    }
  
    void OnTriggerEnter2D(Collider2D other) 
    {
      if ((other.gameObject.name == "Player")) {
        if (!GameControl.instance.gameOver) {
          source.Play();
          weapon.AddAmmo(1);
          scoreEmitter.Play();
          transform.localScale = new Vector3(0, 0, 0);
          polygonCollider.enabled = false;
          Invoke("DestroyItself", 1f);
        }
      }   
    }
    
    void DestroyItself() {
      Destroy(this.gameObject);
    }
}