using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Color Change")]
    public bool changeColor;

    [Header("UI Ellements")]
    [SerializeField] GameObject UI;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI countDown;

    [Header("Camera Attributes")]
    //camera attributes
    [SerializeField] CameraController cam;

    [Header("Transforms of gameborder")]
    //walls attributes
    public Transform bottom;
    public Transform rightWall;
    public Transform leftWall;
    public float edges;

    [Header("Platforms Attributes")]
    //platform attributes
    public float spawnIntervalmin;
    public float spawnIntervalmax;
    public float spawnIntervalFactor;
    public float platformMaxSize;
    public float platformMinSize;

    [Header("Level Counter")]
    //levels counter
    public int floor = 0;
    public int platformsPerFloor = 100;

    [Header("Game Attributes")]
    //game attributes
    public int score = 0;
    public int highScore;
    public bool newHighScore;
    public int coins = 0;
    public bool active;

    public GameStatistics statistics;

    [Header("Kunai and Spring Indicators")]
    public bool kunai;
    public bool spring;

    [Header("Player Attributes")]
    //player attributes
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        //-------------------
        // import high score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScore = 0;
        }
        //-------------------
        score = 0;
        spring = false;
        kunai = false;
        newHighScore = false;
        active = true;
        gameOverScreen.SetActive(false);
        UI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(kunai || spring)
        {
            cam.follow = true;
        }
        else
        {
            cam.follow = false;
        }
        
        //if cleard all the platforms in the floor
    }


    //activate the game
    public void gameStart()
    {
        active = true;
    }

    //stop the game
    public void gameStop() 
    {  
        active = false; 
    }

    //coin collected
    public void addCoin()
    {
        coins++;
        statistics.coinsCollected++;
    }

    //platform cleard
    public void addPlatformToScore(int newScore)
    {
        if(score < newScore)
            score = newScore;
    }

    //reach the next floor, make the game a little harder
    public void floorUp()
    {
        floor += 1;
        spawnIntervalmax += spawnIntervalFactor;
        cam.normalSpeed += cam.speedFactor * (floor + 1);
        if(floor >= 2)
        {
            platformSizeChange();
        }
    }

    //change the spawning platform's size
    public void platformSizeChange()
    {
        platformMaxSize = Mathf.Clamp(platformMaxSize - 0.25f, 1, 10);
        platformMinSize = Mathf.Clamp(platformMinSize - 0.1f, 0.5f, 10);
    }

    public void GameOver()
    {
        if(score > highScore)
        {
            newHighScore = true;
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        active = false;
        cam.speed = 0;
        cam.active = false;
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverUIScript>().GameOverActive();
        UI.GetComponent<UIScript>().GameOver();
    }

    public void SecondChance()
    {
        gameOverScreen.SetActive(false);
        StartCoroutine(ReActivateGame());
    }


    public void Exit()
    {
        CheckMissions();
        AddCoinsToPlayer();
    }

    // check in the gameOver screen if the mission objectives been cleared
    void CheckMissions()
    {
        foreach(MissionBase mission in MissionBase.currentMissions)
        {
            mission.SetClear(statistics);
        }
    }

    //add coins collected in game to player overall amount
    public void AddCoinsToPlayer()
    {
        Coin.AddCoins(coins);
    }

    IEnumerator ReActivateGame()
    {
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
        countDown.text = "";
        gameOverScreen.SetActive(false);
        player.position = Vector3.up * Camera.main.transform.position.y;
        active = true;
        cam.speed = cam.normalSpeed;
        cam.active = true;
    }
}
