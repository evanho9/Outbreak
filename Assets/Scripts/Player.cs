using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 1.5f;
    public float upForce = 200f;
  
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      rb2d.velocity = new Vector2(0, 0);
      anim.SetTrigger("Run"); 
    }

    // Update is called once per frame
    void Update()
    {
      if (!isDead) {  
        //Jump
        if (Input.GetMouseButtonDown(0)) {
          Debug.Log("jumped");
          rb2d.velocity = new Vector2(0, 0); 
          rb2d.AddForce(new Vector2(0, upForce));
          anim.SetTrigger("Jump");
        }
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
      //Player collides with ground
      if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Ground 2") {
        Debug.Log("touched the ground");
      } else {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.PlayerDied();
      }
    }
}
