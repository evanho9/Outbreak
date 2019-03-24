using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
      if ((other.gameObject.name == "SpawnGround2" || other.gameObject.name == "Ground")) {
        if (!GameControl.instance.gameOver) {
          Debug.Log("spawn trigger triggered");
          int rand = Random.Range(0,3);
          GameControl.instance.SpawnTile(rand);
        }
      }   
    }
}
