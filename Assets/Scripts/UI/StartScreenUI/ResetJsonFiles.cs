using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJsonFiles : MonoBehaviour
{
    string[] jsonFilesNames = new string[] 
    {
        "CoinAmountTest.json",
        "DailyMissions.json",
        "testjasonattempt.json"
    };
    public string[] jsonFilesPath = new string[3];

    private void Start()
    {
        for(int i = 0;  i < jsonFilesNames.Length; i++)
        {

            jsonFilesPath[i] = FileHandler.GetPath(jsonFilesNames[i]);
        }
    }

    public void ResetAllJson()
    {
        UpgradeInfo.ResetUpgrades();
        Coin.ResetCoins();
        MissionBase.Set3NewRandomDailyMissions();
    }
}
