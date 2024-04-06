using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public class MissionJsonInfo
{
    public int missionID;
    public bool isCleared;
    public bool isClaimed;



    //GEt the ID of the info to write in the json by the mission type
    public MissionJsonInfo(MissionBase mission)
    {
        if (mission is Mission2ndFloor)
        {
            missionID = 1;
            
        }
        else if (mission is MissionSkipWhileGliding)
        {
            missionID= 2;
        }
        else if (mission is MissionSkipWhileWallRun)
        {
            missionID = 3;
        }
        else if (mission is MissionCoinsCollected)
        {
            missionID = 4;
        }
        else if (mission is MissionItemsUsed)
        {
            missionID = 5;
        }
        isCleared = mission.isCleared;
        isClaimed = mission.isClaimed;
    }
}
