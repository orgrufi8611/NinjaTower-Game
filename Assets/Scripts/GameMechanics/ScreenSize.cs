using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize
{
    public static Vector2 screenSize = new Vector2();
    public static Vector2 resolution = new Vector2(900,1600);
    public static void SetScreenSize(float width, float height)
    {
        screenSize.x = width;
        screenSize.y = height;
    }

    public static float GetScreenWidth()
    {
        return screenSize.x;
    }

    public static float GetScreenHeight()
    {
        return screenSize.y;
    }
}
