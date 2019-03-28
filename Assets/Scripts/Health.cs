using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public int hp = 1;

  public bool isEnemy = true;

  public void Damage(int damageCount)
  {
    hp -= damageCount;
    if (hp <= 0) {
      Destroy(gameObject);
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
}