using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public int hp = 1;

  public bool isEnemy = true;

  private ParticleSystem scoreEmitter;
  private AudioSource source;
  
  private Transform transform;
  private PolygonCollider2D polygonCollider;
  
  void Awake() 
  {
    source = GetComponent<AudioSource>();
    scoreEmitter = GameObject.Find("ScoreEmitter").GetComponent<ParticleSystem>();
    transform = GetComponent<Transform>();
    polygonCollider = GetComponent<PolygonCollider2D>();
  }
  
  public void Damage(int damageCount)
  {
    hp -= damageCount;
    if (hp <= 0) {
      source.Play();
      GameControl.instance.AddScore(500f);
      scoreEmitter.Play();
      transform.localScale = new Vector3(0, 0, 0);
      polygonCollider.enabled = false;
      Invoke("DestroyItself", 1f);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) 
  {
    Shot shot = collision.gameObject.GetComponent<Shot>();
    if (shot != null) {
      if (shot.isEnemyShot != isEnemy) {
        Damage(shot.damage);
        
        Destroy(shot.gameObject);
      }
    }
  }
  
  void DestroyItself() {
    Destroy(gameObject);
  }
}