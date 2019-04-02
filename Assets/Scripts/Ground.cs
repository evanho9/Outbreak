using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      
    void OnCollisionEnter2D(Collision2D collision) 
    {
      Shot shot = collision.gameObject.GetComponent<Shot>();
      if (shot != null) {
        Destroy(shot.gameObject);
      }
    }
    
    void OnBecameInvisible()
    {
      if (!GameControl.instance.gameOver) {
        Destroy(this.gameObject);
      }
    }
}
