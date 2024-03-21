using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float MaxChainTime;
    public float score;
    public TextMeshProUGUI scoreText;
    public List<ChainScore> chainScoreList;

    [System.Serializable]
    public class ChainScore
    {
       public Brick.Element elementTypeScore;
       public float score;
       public int amount;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void GetScore()
    {        
    }
}
