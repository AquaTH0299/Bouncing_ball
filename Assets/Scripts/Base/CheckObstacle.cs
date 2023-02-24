using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckObstacle : MonoBehaviour
{
    private static CheckObstacle instance;
    public static CheckObstacle Instance{get {return instance;}}

    
    [SerializeField] ParticleSystem effectSystem;
   
     
    private int scoreNum;
    public int ScoreNum => scoreNum;
    
    private void Awake() 
    {
        if(instance !=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0;
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore(){
        scoreNum = 0;
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            scoreNum++;
            // effectSystem.transform.position = other.transform.position;
            // effectSystem.Play();
            GameManager.instance.UpdateScoreText(scoreNum);
            GameManager.instance.UpdateHighScore(scoreNum);
        }
    }
}
