using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Text first;
    public string difficulty;
  
  
    // Start is called before the first frame update
    void Start()
    {
        first.text = PlayerPrefs.GetFloat(difficulty+"HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
