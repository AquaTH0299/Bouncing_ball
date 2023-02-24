using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Random = UnityEngine.Random;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Spawn spawn;
    [SerializeField] private Ball ball;
    [SerializeField] private MoveGround ground;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI totalsPlayed;
    public List<Color> blockMaterials;

    public TextMeshProUGUI textObject; // GameObject chứa TextMesh
    public SpriteRenderer backGround; // SpriteRenderer của Background
    public SpriteRenderer[] groundRenderer; // SpriteRenderer của Ground
    
    //public ParticleSystem particleSystemEffect; // component ParticleSystem của particle
    public Color initialMaterial;

    public GameObject panelEndGame;
    public GameObject restartButton;

    private RaycastHit2D hit;
    private ColorChanger colorChanger;

    private int highScoreNum;
    private int newNum = 0;

    private bool isGameStarted = false;
    private bool isGameOver = false;
    private bool canRestart = false;
    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        groundRenderer = GameObject.FindGameObjectsWithTag("Ground").SelectMany(a => a.GetComponentsInChildren<SpriteRenderer>()).ToArray();
       
    }

    private void Start()
    {
        newNum = PlayerPrefs.GetInt("newNum") + 1;
        PlayerPrefs.SetInt("newNum",newNum);
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }

        else if (!isGameOver)
        {
            if(ball != null && ball.IsGameOver())
            {
                EndGame();
            }
        }
        
        if(canRestart && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);
            if (hit.collider != null)
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "restartButton":
                        RestartGame();
                        break;
                    case "settingButton":
                        hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);   
                        hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
                        hit.collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                        break;
                    case "volumeButton":
                        hit.collider.gameObject.GetComponent<Toggle>().ToggleAudio();
                        break;
                }
                
            }
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        ground.StartMoving();
        ball.StartPlaying();
        startText.text = "Tap to Bounce";
        Invoke("Delay", 1.25f);
        panelEndGame.SetActive(false);
        spawn.StartSpawn();
    }

    private void Delay()
    {
        startText.gameObject.SetActive(false);
    }

    private void EndGame()
    {
        isGameOver = true;
        ground.StopMoving();
        ball.Dead();
        panelEndGame.SetActive(true);
        CountPlayed();
        spawn.StopSpawn();
        canRestart = true;
        highScoreNum = PlayerPrefs.GetInt("highScore");
        highScoreText.text = "High Score: " + highScoreNum.ToString();
    }

    public void RestartGame()
    {
        scoreText.text = "0";
        CheckObstacle.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        colorChanger = GameObject.FindGameObjectWithTag("Player").GetComponent<ColorChanger>();
        if (colorChanger != null) 
        {
            colorChanger.ResetColor();
        }
    }

    public void GameOver()
    {
        EndGame();
    }

    private void CountPlayed()
    {
        totalsPlayed.text = "Games Played: " + newNum.ToString();
    }
    
    public void UpdateScoreText(int scoreNum){
        scoreText.text = scoreNum.ToString();
    }

    public void UpdateHighScore(int scoreNum)
    {
        highScoreNum = PlayerPrefs.GetInt("highScore");
        if(scoreNum > highScoreNum)
        {
            highScoreNum = scoreNum;
            PlayerPrefs.SetInt("highScore", highScoreNum);
        }
        highScoreText.text = "High Score: " + highScoreNum.ToString();
    }

    public Color GetRandomBlockMaterial()
    {
        int index = Random.Range( 0, blockMaterials.Count);
        Color material = blockMaterials[index];
        return material;
    }
}