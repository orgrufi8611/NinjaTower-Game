using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionListLogic : MonoBehaviour
{
    public GameObject cellPrefab, emptyCellPrefab;
    public Button claimAllButton;

    private const string LastCallKey = "LastRandomMissionsSetCall";
    // Start is called before the first frame update
    void Start()
    {
        claimAllButton.interactable = false;
        try
        {
            if (CanCallFunctionToday())
            {
                // set 3 new mission on first entry of the day
                MissionBase.Set3NewRandomDailyMissions();
                
                // Update the last call time
                UpdateLastCallTime();
            }
            else
            {
                //if not first entry load existed missions
                MissionBase.LoadDailyMissions();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        DisplayMissions();
        DisplayClaimAllButton();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void DisplayMissions()
    {
        foreach (MissionBase mission in MissionBase.currentMissions)
        {
            //TBI   check game over stats and do SetClear(stat); depending on the type of mission class the mission is
            GameObject obj = Instantiate(cellPrefab);
            obj.transform.SetParent(this.gameObject.transform, false);
            obj.GetComponent<MissionCellLogic>().SetInfo(mission);
        }

        GameObject emptyobj = Instantiate(emptyCellPrefab);
        emptyobj.transform.SetParent(this.gameObject.transform, false);

        

    }

    private void DisplayClaimAllButton()
    {
        int availableClaims = 0;
        //return list.Where(i => i.upgradeName.Equals(upgradeName)).FirstOrDefault();
        foreach(MissionBase mission in MissionBase.currentMissions)
        {
            print("out");
            print(mission.isCleared +" clear || claim "+ mission.isClaimed);
            if(mission.isCleared && !mission.isClaimed)
            {
                availableClaims++;
                print("in");
                //make button interactble
                claimAllButton.interactable = true;
                break;
            }
            if(availableClaims > 0)
                claimAllButton.interactable = true;
            else claimAllButton.interactable = false;
        }
    }

    public void ClaimAll()
    {
        //TBI

        MissionCellLogic[] missionCellLogics = gameObject.GetComponentsInChildren<MissionCellLogic>();
        foreach(MissionCellLogic missionCellLogic in missionCellLogics)
        {
            if (missionCellLogic.mission.isCleared && !missionCellLogic.mission.isClaimed)
            {
                missionCellLogic.ClaimCoins();
            }
            //missionCellLogic.ClaimCoins();
        }
        claimAllButton.interactable = false;
    }


    private bool CanCallFunctionToday()
    {
        // Check if the function has never been called before
        if (!PlayerPrefs.HasKey(LastCallKey))
        {
            return true;
        }

        // Retrieve the last call time from PlayerPrefs
        string lastCallTimeString = PlayerPrefs.GetString(LastCallKey);
        DateTime lastCallTime = DateTime.Parse(lastCallTimeString);

        // Check if the last call was more than 24 hours ago
        double timepass = (DateTime.Now - lastCallTime).TotalHours;
        Debug.Log("Time between plays: " + timepass);
        return (DateTime.Now - lastCallTime).TotalHours >= 24;
    }

    private void UpdateLastCallTime()
    {
        // Save the current time as the last call time in PlayerPrefs
        string currentTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString(LastCallKey, currentTimeString);
        PlayerPrefs.Save();
    }

}
