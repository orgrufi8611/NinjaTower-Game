using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    [Header("Player movement Statistics")]
    public int platformsSkippedWhileWallRun;
    public int platformsClearedWithoutGliding;
    public int ObjectsUsed;
    public int platformsCleared;
    public int coinsCollected;

    public int platformsSkippedWhileWallRunMax;
    public int platformsClearedWithoutGlidingMax;


    // Start is called before the first frame update
    void Start()
    {
        platformsSkippedWhileWallRun = 0;
        platformsSkippedWhileWallRunMax = 0;
        platformsClearedWithoutGliding = 0;
        platformsClearedWithoutGlidingMax = 0;
        ObjectsUsed = 0;
        platformsCleared = 0;
        coinsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlatformSkippedWhileWallRun()
    {
        if(platformsSkippedWhileWallRun > platformsSkippedWhileWallRunMax)
        {
            platformsSkippedWhileWallRunMax = platformsSkippedWhileWallRun;
        }
            platformsSkippedWhileWallRun = 0;
    }

    public void ResetPlatformClearedWithoutGliding()
    {
        if (platformsClearedWithoutGliding > platformsClearedWithoutGlidingMax)
        {
            platformsClearedWithoutGlidingMax = platformsClearedWithoutGliding;
        }
        platformsClearedWithoutGliding = 0;
        
    }

    public void SetToMax()
    {
        platformsSkippedWhileWallRunMax = 100;
        platformsClearedWithoutGlidingMax = 100;
        ObjectsUsed = 100;
        platformsCleared = 500;
        coinsCollected = 100;
    }
}
