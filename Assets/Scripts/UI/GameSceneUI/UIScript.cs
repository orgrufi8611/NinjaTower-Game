using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] GameObject pause;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = gameLogic.score.ToString();
        coins.text = gameLogic.coins.ToString();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        gameLogic.active = false;
        pauseMenu.SetActive(true);
        pause.SetActive(false);
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameLogic.active = true;
        pauseMenu.SetActive(false);
        pause.SetActive(true);
    }

    //tell the gamelogic to extract game info
    public void Exit()
    {
        gameLogic.Exit();
    }

    public void GameOver()
    {
        pause.SetActive(false);
    }
}
