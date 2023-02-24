using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private bool isGameOver;
    private Rigidbody2D rb;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject[] buttons;
    private Vector2 ballStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballStartPosition = transform.position;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
             if(Input.GetMouseButtonDown(0))
            {
                StartPlaying();
            }
        }
    }

    public void StartPlaying()
    {
        rb.velocity = new Vector2( 0 , -4.75f);
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!isGameOver)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            }
            else if(other.gameObject.CompareTag("Obstacle"))
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                foreach(var button in buttons)
                {
                    button.gameObject.SetActive(true);
                }
                Dead();
            }
        }
    }
    

    public void Dead()
    {
        isGameOver = true;
        rb.velocity = Vector2.zero;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
