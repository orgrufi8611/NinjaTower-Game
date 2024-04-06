using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2ndFloor : MissionBase
{
    public Mission2ndFloor() : base()
    {
        missionDescription = "Reach floor 2";
        reward = 200;
    }


    public override bool CheckClear(GameStatistics stat)
    {
        if (stat.platformsCleared >= 200)
        {
            return true;
        }
        return false;
        
    }
}
