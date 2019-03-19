using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upForce = 300f;
  
    private bool isDead = false;
    private bool isGrounded = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
      anim.SetTrigger("Run"); 
    }

    // Update is called once per frame
    void Update()
    {
      //isGrounded = Physics2D.OverlapCircle(GroundCheck1.position,0.15f, groundLayer);
      rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, rb2d.velocity.y);
      if (!isDead) {  
        GameControl.instance.AddScore(0.01f);
        //Jump
        if (Input.GetKeyDown("space") && isGrounded) {
          isGrounded = false;
          rb2d.AddForce(new Vector2(0, upForce));
          anim.SetTrigger("Jump");
        }
        if (Input.GetKeyDown("x")) {
          Weapon weapon = GetComponent<Weapon>();
          if (weapon != null) {
            weapon.Attack(false);
          }
        }
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision) 
    {
      //Player collides with ground
      if ((collision.gameObject.name == "Ground" || collision.gameObject.name == "SpawnGround"
      || collision.gameObject.name == "Building")) {
        isGrounded = true;
      } else {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.PlayerDied();
      }
    }
    
    void OnBecameInvisible()
    {
      isDead = true;
      anim.SetTrigger("Die");
      GameControl.instance.PlayerDied();
    }
}
