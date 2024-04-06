using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCoinsCollected : MissionBase
{
    public MissionCoinsCollected() : base()
    {
        missionDescription = "Collect 50 coins in a single run";
        reward = 200;
    }

    public override bool CheckClear(GameStatistics stat) //stat = coinsCollected
    {
        if (stat.coinsCollected >= 50)
        {
            return true;
        }
        return false;
    }
}
