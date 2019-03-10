using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upForce = 200f;
  
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if (!isDead) {
        
        //Jump
        if (Input.GetMouseButtonDown(0)) {
          rb2d.velocity = new Vector2(0, 0); 
          rb2d.AddForce(new Vector2(0, upForce));
          anim.SetTrigger("Jump");
        }
      }
    }
    
    void OnCollisionEnter2D() {
      isDead = true;
      anim.SetTrigger("Run");
      GameControl.instance.PlayerDied();
    }
}
