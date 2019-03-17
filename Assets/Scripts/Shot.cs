using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
  private Rigidbody2D rb2d;
  
  public int damage = 1;

  public bool isEnemyShot = false;

  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();
    Destroy(gameObject, 3);
  }
  
  void Update()
  {
    rb2d.velocity = new Vector2(-GameControl.instance.scrollSpeed*1.5f, 0);
  }
}