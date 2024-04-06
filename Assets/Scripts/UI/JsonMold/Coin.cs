using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Coin
{
    public int amount;

    public static readonly string coinFilePath = "CoinAmountTest.json";
    public Coin(int amount)
    {
        this.amount = amount;
    }

    public static void ResetCoins()
    {
        Coin coin = new Coin(0);
        FileHandler.SaveToJSon(coin, coinFilePath);
    }

    public static void SpendCoins(int amount)
    {
        Coin t = FileHandler.ReadFromJSon<Coin>(coinFilePath);
        t.amount -= amount;
        FileHandler.SaveToJSon(t, coinFilePath); 
    }

    public static bool IsMoneyEnough(int price)
    {
        Coin t = FileHandler.ReadFromJSon<Coin>(coinFilePath);
        try
        {
            if (t.amount >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            throw new System.Exception("coins file is either empty or missing");
            //return false;
        }
    }

    public static void AddCoins(int amount)
    {
        try
        {
            Coin t = FileHandler.ReadFromJSon<Coin>(coinFilePath);
            t.amount += amount;
            FileHandler.SaveToJSon(t, coinFilePath);
        }
        catch
        {
            throw new System.Exception("coins file is error while trying to add coins");
        }
    }
}
