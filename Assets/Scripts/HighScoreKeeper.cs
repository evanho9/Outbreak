using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreKeeper : MonoBehaviour
{
    public static HighScoreKeeper instance;
  
    public ArrayList easyHighScores;
    public ArrayList mediumHighScores;
    public ArrayList hardHighScores;
    public string difficulty;
  
    void Awake()
    {
      if (instance == null) {
        instance = this;
      } else {
        Destroy (gameObject);
      }
      easyHighScores = new ArrayList();
      mediumHighScores = new ArrayList();
      hardHighScores = new ArrayList();
    }

}
