using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upVelocity;
  
    private bool isDead = false;
    private bool isGrounded = false;
    private bool isFirstJump = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float startingX;
    
    public bool isHardMode;
    private bool gravitySwitched = false;
    private int numberOfJumpsLeft;
    public int maxJumps;
    private int noJumpFrames = 0;
    
    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      rb2d.velocity = new Vector2(0, 0);
      //anim.SetTrigger("run"); 
      startingX = this.transform.position.x;
      numberOfJumpsLeft = maxJumps;
      //transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      if (noJumpFrames > 0)
        noJumpFrames--;
      //isGrounded = Physics2D.OverlapCircle(GroundCheck1.position,0.15f, groundLayer);
      rb2d.velocity = new Vector2(0, rb2d.velocity.y);
      if (this.transform.position.x < startingX-0.25) {
        anim.SetTrigger("onwall");
        if (gravitySwitched)
          this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, this.transform.position.z);
        else
          this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.02f, this.transform.position.z);
      }
      if (!isDead) {  
        //Jump
        if (Input.GetKeyDown("space") && noJumpFrames == 0 && numberOfJumpsLeft>0) {
          if (isGrounded == false && isFirstJump == true) {
            if (gravitySwitched)
              rb2d.velocity = new Vector2(0, -upVelocity);
            else
              rb2d.velocity = new Vector2(0, upVelocity);
            //anim.SetTrigger("doublejump");
            numberOfJumpsLeft--;
          } else {
            if (gravitySwitched)
              rb2d.velocity = new Vector2(0, -upVelocity);
            else
              rb2d.velocity = new Vector2(0, upVelocity);
            //anim.SetTrigger("jump");
            numberOfJumpsLeft--;
          } 
        }
        if (Input.GetKeyDown("x")) { 
          Weapon weapon = GetComponent<Weapon>();
          if (weapon != null) {
            weapon.Attack(false);
          }
        }
        if (isHardMode && noJumpFrames == 0 && Input.GetKeyDown("z")) { 
          rb2d.gravityScale *= -1;
          gravitySwitched = !gravitySwitched;
          noJumpFrames = 100;
          transform.Rotate(0, 180, 180);
        }
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision) 
    {
      //Player collides with ground
      if (collision.gameObject.name == "Ground" || collision.gameObject.name == "SpawnGround" 
      || collision.gameObject.name == "SpawnGround2" || collision.gameObject.name == "Building") {
        Debug.Log("REFRESH JUMPS");
        numberOfJumpsLeft = maxJumps;
      } else if (collision.gameObject.name != "Coin" || collision.gameObject.name != "Ammo"){
        isDead = true;
        //anim.SetTrigger("die");
        GameControl.instance.PlayerDied();
      }
    }
    
    void OnBecameInvisible()
    {
      isDead = true;
      //anim.SetTrigger("Die");
      GameControl.instance.PlayerDied();
    }
}
