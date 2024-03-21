using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int score;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            scoreManager.score += score;
            Destroy(gameObject);
        }
    }
}
