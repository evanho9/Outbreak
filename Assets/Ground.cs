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
        
    void OnBecameInvisible()
    {
        Debug.Log("out of view");
        int randInt = (int)Random.Range(0, 3);
        GameControl.instance.SpawnTile(randInt);
        Destroy(this.gameObject);
    }
}
