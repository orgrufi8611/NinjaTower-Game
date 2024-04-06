using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBaseAttributes
{
    public static List<float> baseAttributes = new List<float>() { 5,4,2,7,1,0,100,20,20,40,30 };
    public static float baseGlidingDesentVelocity = -0.3f;
    
    public static float baseHorizantalVelocity = 5;
    
    public static float baseOnWallVelocity = 4;
    
    public static float baseGlidingHorizantalVelocity = 2;
    
    public static float baseJumpVelocity = 7;
    
    public static float baseMaxJumps = 1;
    
    public static float baseRestoredJumpsOffWall = 0;
    
    public static float baseMaxStamina = 100;
    
    public static float baseStaminaRestoration = 20;
    
    public static float baseStaminaDepletion = 20;
    
    public static float baseKunaiTravelDistance = 40;
    
    public static float baseSpringJumpHeight = 30;

}
