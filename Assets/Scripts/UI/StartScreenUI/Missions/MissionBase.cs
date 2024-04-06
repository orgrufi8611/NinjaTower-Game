using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBase
{

    public string missionDescription;
    public int reward;
    public bool isCleared;
    public bool isClaimed;

    public static readonly string misssionFileName = "DailyMissions.json";


    public static MissionBase[] currentMissions = new MissionBase[3];

    public MissionBase()
    {
        isCleared = false;
        isClaimed = false;
    }

    public virtual bool CheckClear(GameStatistics stat)
    {
        return false;
    }

    public void SetClear(GameStatistics stat)
    {
        if (!isCleared)
        {
            isCleared = CheckClear(stat);
            SaveDailyMissions();
        }
    }
  
    public static void Set3NewRandomDailyMissions()
    {
        int num1, num2, num3;
        num1 = Random.Range(1, 6);
        do
        {
            num2 = Random.Range(1, 6);
        }
        while (num2 == num1);

        do
        {
            num3 = Random.Range(1, 6);
        }
        while (num3 == num1 || num3 == num2);
        currentMissions[0] = CreateMissionByNumber(num1);
        currentMissions[1] = CreateMissionByNumber(num2);
        currentMissions[2] = CreateMissionByNumber(num3);
        SaveDailyMissions();
    }
    private static MissionBase CreateMissionByNumber(int number)
    {
        switch (number)
        {
            case 1:
                return new Mission2ndFloor();
            case 2:
                return new MissionSkipWhileGliding();
            case 3:
                return new MissionSkipWhileWallRun();
            case 4:
                return new MissionCoinsCollected();
            case 5:
                return new MissionItemsUsed();
            default:
                throw new System.Exception("unexpected random number");
        }
    }

    public static void SaveDailyMissions()
    {

        MissionJsonInfo[] Arr = new MissionJsonInfo[3];
        Arr[0] = new MissionJsonInfo(currentMissions[0]);
        Arr[1] = new MissionJsonInfo(currentMissions[1]);
        Arr[2] = new MissionJsonInfo(currentMissions[2]);
        FileHandler.SaveToJSon(Arr, misssionFileName);
    }

    public static void LoadDailyMissions()
    {
        List<MissionJsonInfo> list = FileHandler.ReadListFromJSon<MissionJsonInfo>(misssionFileName);

        for (int i = 0; i < currentMissions.Length; i++)
        {
            switch (list[i].missionID)
            {
                case 1:
                    currentMissions[i] = new Mission2ndFloor();
                    break;
                case 2:
                    currentMissions[i] = new MissionSkipWhileGliding();
                    break;
                case 3:
                    currentMissions[i] = new MissionSkipWhileWallRun();
                    break;
                case 4:
                    currentMissions[i] = new MissionCoinsCollected();
                    break;
                case 5:
                    currentMissions[i] = new MissionItemsUsed();
                    break;
                default:
                    throw new System.Exception("unexpected Mission ID");
            }
            currentMissions[i].isCleared = list[i].isCleared;
            currentMissions[i].isClaimed = list[i].isClaimed;
        }

    }
}
