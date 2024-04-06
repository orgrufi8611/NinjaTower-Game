using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSkipWhileWallRun : MissionBase
{
    public MissionSkipWhileWallRun() : base()
    {
        missionDescription = "Skip 4 platfroms during a wall run";
        reward = 200;
    }

    public override bool CheckClear(GameStatistics stat) //stat = platformsSkippedWhileWallRunMax
    {
        if (stat.platformsSkippedWhileWallRunMax >= 4)
        {
            return true;
        }
        return false;
    }
}
