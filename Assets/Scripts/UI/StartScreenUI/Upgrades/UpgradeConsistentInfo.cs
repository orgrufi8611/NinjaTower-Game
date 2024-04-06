using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeConsistentInfo
{
    public string upgradeName;
    public int maxLevel;
    public int basePrice;
    public float multiplierIncrease;


    // Generate the available upgrades with thier:
    // name, max level, base priece, multiplyer persantage increasment
    public static readonly List<UpgradeConsistentInfo> upgradesList = new List<UpgradeConsistentInfo>()
    {
        new UpgradeConsistentInfo("horizontalSpeed",8,100, 0.1f),
        new UpgradeConsistentInfo("wallSpeed",8,100,0.1f),
        new UpgradeConsistentInfo("glideVerticalDrop",3,1000, -0.1f),
        new UpgradeConsistentInfo("jumpHeight",5,1000, 0.1f),
        new UpgradeConsistentInfo("maxJumps",5,1000,1),
        new UpgradeConsistentInfo("restoredJumpsOffWall",3,1000, 1),
        new UpgradeConsistentInfo("maxStamina",10,1000,0.2f),
        new UpgradeConsistentInfo("staminaRestorationRate",5,1000,0.2f),
        new UpgradeConsistentInfo("staminaDeplitionRate",5,1000, -0.1f),
        new UpgradeConsistentInfo("KunaiTravelDistance",10,1000, 0.1f),
        new UpgradeConsistentInfo("springJumpHeight",10,1000, 0.1f)
        
    };

  

    public UpgradeConsistentInfo(string upgradeName, int maxLevel, int basePrice, float multiplierIncrease) 
    {
        this.upgradeName = upgradeName;
        this.maxLevel = maxLevel;
        this.basePrice = basePrice;
        this.multiplierIncrease = multiplierIncrease;
    }

    //calculate the price of the current upgrade depend on the upgrade stage
    public int CalculatePrice(int currentLevel)
    {
        return basePrice*(currentLevel+1);
    }


    //find the item in the list by upgradeName property
    public static UpgradeConsistentInfo GetUpgradeConsistentInfo(string upgradeName)
    {
        return upgradesList.Where(i => i.upgradeName.Equals(upgradeName)).FirstOrDefault(); 
        //if can't find it returns null
    }


    //returns a list of all the upgrade names
    public static List<string> GetAllUpgradeNames()
    {
        return upgradesList.Select(i => i.upgradeName).ToList(); 
    }
}
