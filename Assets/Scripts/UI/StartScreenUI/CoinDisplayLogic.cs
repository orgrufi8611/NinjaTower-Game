using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDisplayLogic : MonoBehaviour
{
    public static int coinAmount = 0;

    private static string coinFilePath = "CoinAmountTest.json";
    public TextMeshProUGUI coinAmountDisplay;
    public static TextMeshProUGUI coinAmountText;

    
    // Start is called before the first frame update
    void Start()
    {
        coinAmountText = coinAmountDisplay;
        if(!File.Exists(Application.persistentDataPath + "/" + coinFilePath))
        {
            Coin.ResetCoins();
        }

        SetCoinDisplayStatic();
        
    }

   

    // Update is called once per frame
    void Update()
    {
        //set coin to text
        coinAmountText.text = coinAmount.ToString();
    }


    //Take the coins amount saved in the json file if exist
    public static void SetCoinDisplayStatic()
    {
        Coin t = FileHandler.ReadFromJSon<Coin>(coinFilePath);
        if (t != null)
        {
            coinAmount = t.amount;
            coinAmountText.text = t.amount.ToString();
        }
        else
        {
            Debug.LogError("coin file empty or missing");
            Coin.ResetCoins();
            coinAmountText.text = "0";

        }
    }


    //Reduce coins with the upgrade price
    public static void PayUpgrade(int price)
    {

        Coin.SpendCoins(price);
        coinAmount -= price;
    }

    public static void AddCoins(int add)
    {
        coinAmount += add;
        Coin.AddCoins(add);
    }
}
