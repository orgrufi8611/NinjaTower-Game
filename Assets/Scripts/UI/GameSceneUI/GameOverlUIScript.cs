using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] Button retry;
    [SerializeField] Button exit;
    [SerializeField] Button seconChance;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI msgScore;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Image AdWindow;
    int contineusCount;

    private void Awake()
    {
        contineusCount = 0;
    }
    public void GameOverActive()
    {
        highScore.text = "High Score:" + gameLogic.highScore;
        if (gameLogic.newHighScore)
        {
            msgScore.text = "New\nHigh Score";
        }
        else
        {
            msgScore.text = "Score:" + gameLogic.score;
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        gameLogic.Exit();
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("SceneController").GetComponent<SceneController>().ResetScene();
    }

    public void SecondChance()
    {
        contineusCount++;
        //play Ad and invoke the gameLogic.SecondChance
        StartCoroutine(PlayAdAndWait());
        if(contineusCount > 2)
        {
            seconChance.gameObject.SetActive(false);
        }
    }

    IEnumerator PlayAdAndWait()
    {
        StartCoroutine(AdWindowScript.PlayAd(AdWindow));
        yield return new WaitForSeconds(3);
        gameLogic.SecondChance();
    }
}
