using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionItemsUsed : MissionBase
{
    public MissionItemsUsed() : base()
    {
        missionDescription = "Used 2 items in a single run";
        reward = 200;
    }

    public override bool CheckClear(GameStatistics stat)
    {
        if (stat.ObjectsUsed >= 2)
        {
            return true;
        }
        return false;
    }
}
