using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;
    private Vector2 groundOffset;
    private bool isEvenTile;
  
    // Start is called before the first frame update
    void Start()
    {
      groundCollider = GetComponent<BoxCollider2D>();
      groundHorizontalLength = groundCollider.size.x * transform.localScale.x;
      groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
      isEvenTile = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (transform.position.x < -groundHorizontalLength) {
        RepositionBackground();
        isEvenTile = !isEvenTile;
      }
    }
    
    private void RepositionBackground()
    {
      transform.position = (Vector2)transform.position + groundOffset;
    }
}
