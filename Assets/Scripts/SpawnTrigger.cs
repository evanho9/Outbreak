using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
  
    public bool isHardMode;
  
    void OnTriggerEnter2D(Collider2D other) 
    {
      if ((other.gameObject.name == "SpawnGround2" || other.gameObject.name == "Ground")) {
        if (!GameControl.instance.gameOver) {
          int rand = Random.Range(0,3);
          if (isHardMode)
            GameControl.instance.SpawnHardTile(rand);
          else
            GameControl.instance.SpawnTile(rand);
        }
      }   
    }
}
