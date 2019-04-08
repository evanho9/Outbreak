using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private AudioSource audioSource;
  
    // Start is called before the first frame update
    void Start()
    {
      audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTrigger2D() 
    {
      audioSource.Play();
    }
    
    void OnBecameInvisible()
    {
      if (!GameControl.instance.gameOver) {
        Destroy(this.gameObject);
      }
    }
}
