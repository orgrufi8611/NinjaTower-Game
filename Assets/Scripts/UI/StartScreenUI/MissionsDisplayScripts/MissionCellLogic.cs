using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionCellLogic : MonoBehaviour
{

    public GameObject coinPrefab;
    public Transform coinIconLocation;
    public MissionBase mission;
    public TextMeshProUGUI missionInstructionsText, rewardText, claimButtonText;
    public GameObject rewardDisplay;
    public Button claimButton;
    public Button doubleButton;
    int rewardMultiplyer = 1;
    [SerializeField] Image AdWindow;
    // Start is called before the first frame update
    void Start()
    {
        //doubleButton.gameObject.SetActive(false);
        rewardMultiplyer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void DisplayInfo()
    {
        missionInstructionsText.text = mission.missionDescription;
        //------------------------------------------------------------------
        rewardText.text = (mission.reward * rewardMultiplyer).ToString();
        //------------------------------------------------------------------
        if (mission.isCleared)
        {
            //claimButtonText.text = "Claim";
            
            if (mission.isClaimed)
            {
                claimButtonText.text = "Claimed";
                rewardDisplay.SetActive(false);
                claimButton.interactable = false;
                //----------------------------------
                doubleButton.interactable = false;
                //----------------------------------
            }
            else
            {
                claimButton.interactable = true;
                //--------------------------------
                doubleButton.gameObject.SetActive(true);
                doubleButton.interactable= true;
                //--------------------------------
            }
        }
        else
        {
            claimButtonText.text = "";
            claimButton.interactable = false;
            doubleButton.interactable = false;
        }

        
    }

    public void SetInfo(MissionBase mission)
    {
        this.mission = mission;
        DisplayInfo();
    }

    public void ClaimCoins()
    {

        //TBI + disable button + change text etc + change saved mission to claimed
        //MoveCoin();
        try
        {
            CoinDisplayLogic.AddCoins(mission.reward * rewardMultiplyer);

            //with how arrays work this changes the info in the mission in MissionBase.currentMissions too.
            mission.isClaimed = true;
            mission.isCleared = true;
            MissionBase.SaveDailyMissions();
            //  TBI save array to json
           
            //Animtion
            MoveCoin();

            //set coins to Json
            //update coins amount in coinDisplay
            claimButton.interactable = false;
            claimButtonText.text = "Claimed";
            rewardDisplay.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void MoveCoin()
    {
        GameObject tempCoin = Instantiate(coinPrefab, coinIconLocation.position, Quaternion.identity);
        tempCoin.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        tempCoin.transform.position = coinIconLocation.position;
    }
    //-----------------------------------------------------------------------------------
    public void DubbleReward()
    {
        StartCoroutine(AdWindowScript.PlayAd(AdWindow));
        doubleButton.interactable = false;
        
        rewardMultiplyer = 2;
        StopAllCoroutines();
        DisplayInfo();
    }

    
    //-----------------------------------------------------------------------------------
}
