using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSkipWhileGliding : MissionBase
{
    public MissionSkipWhileGliding() : base()
    {
        missionDescription = "Reach 10 points without using Glide";
        reward = 200;
    }

    public override bool CheckClear(GameStatistics stat) // stat = platformsClearedWithoutGlidingMax
    {
        if (stat.platformsClearedWithoutGlidingMax >= 10)
        {
            return true;
        }
        return false;
    }
}

