using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMultiplyer : MonoBehaviour
{
    [SerializeField] List<float> mMultiplier = new List<float>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    public static List<float> multAttributes = new List<float>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    // Start is called before the first frame update
    void Start()
    {
        LoadDataFromJson();
        for(int i = 0; i < mMultiplier.Count; i++)
        {
            mMultiplier[i] = multAttributes[i];
        }
    }

    public static void LoadDataFromJson()
    {
        
        var upgradeList = UpgradeInfo.GetAllUpgrades();
        //----------------------------------
        // read from json and save
        //----------------------------------
        if( upgradeList != null)
        {
            for(int i = 0; i < multAttributes.Count;i++){
                multAttributes[i] = upgradeList[i].abilityChange;
            }
        }
    }

}
