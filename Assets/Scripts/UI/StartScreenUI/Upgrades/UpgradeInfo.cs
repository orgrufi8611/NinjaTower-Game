using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UpgradeInfo 
{

    public string upgradeName;
    public int level;
    public float abilityChange;

    public static readonly string upgradeFileName = "testjasonattempt.json";

    public UpgradeInfo(string name, int level, float abilityChange)
    {
        this.upgradeName = name;
        this.level = level;
        this.abilityChange = abilityChange;
    }


    //initialize with no data, as base
    public UpgradeInfo(string upgradeName)
    {
        this.upgradeName=upgradeName;
        this.level = 0;
        this.abilityChange = 1;
    }

    public static UpgradeInfo FindUpgradeInfoByName(string upgradeName, List<UpgradeInfo> list)
    {
        return list.Where(i => i.upgradeName.Equals(upgradeName)).FirstOrDefault();
    }

    public static List<UpgradeInfo> GetAllUpgrades()
    {
        try
        {

            return FileHandler.ReadListFromJSon<UpgradeInfo>(UpgradeInfo.upgradeFileName);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }

    public static void ResetUpgrades()
    {
        List<UpgradeInfo>  upgradeInfos = new List<UpgradeInfo>();
        List<string> upgradeNames = UpgradeConsistentInfo.GetAllUpgradeNames();
        foreach (string upgradeName in upgradeNames)
        {
            upgradeInfos.Add(new UpgradeInfo(upgradeName));
        }
        FileHandler.SaveToJSon(upgradeInfos, upgradeFileName);
    }


    public void RaiseAbilityMultiplier()
    {
        float multiplierIncrease = UpgradeConsistentInfo.GetUpgradeConsistentInfo(upgradeName).multiplierIncrease;
        abilityChange = 1 + multiplierIncrease * level;
    }
}
