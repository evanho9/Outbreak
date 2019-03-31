using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public int hp = 1;

  public bool isEnemy = true;

  private AudioSource source;
  
  void Awake() 
  {
    source = GetComponent<AudioSource>();
  }
  
  public void Damage(int damageCount)
  {
    hp -= damageCount;
    if (hp <= 0) {
      source.Play();
      GetComponent<SpriteRenderer>().enabled = false;
      GetComponent<PolygonCollider2D>().enabled = false;
      Invoke("DestroyItself", 1f);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) 
  {
    Shot shot = collision.gameObject.GetComponent<Shot>();
    if (shot != null) {
      if (shot.isEnemyShot != isEnemy) {
        Damage(shot.damage);
        GameControl.instance.AddScore(25f);
        Destroy(shot.gameObject);
      }
    }
  }
  
  void DestroyItself() {
    Destroy(gameObject);
  }
}