using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeCellLogic : MonoBehaviour
{
    //---------------------------------------
    public UpgradeSliderScript lvlIndicator;
    //---------------------------------------

    public Slider slider;
    public TextMeshProUGUI tmpTitle, tmpValueOverMax, PriceDisplay;
    private int upgradePrice;
    
    
    private string upgradeName;
    private int maxLevel;

    public Button freeUpgradeButton;
    public Button button;
    public TextMeshProUGUI buttonText;
    [SerializeField] Image AdWindow;
    
    // Start is called before the first frame update
    void Start()
    {
        freeUpgradeButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //add a disable effect to the upgrade button when there is not enough money
    }

    private void SetSliderDisplay(int value, int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = value;
        //----------------------------------
        lvlIndicator.SetMax(maxValue);
        lvlIndicator.SetLevelStart(value);
        //---------------------------------
    }

    private void SetTitleText()
    {
        tmpTitle.text = this.upgradeName;
    } 
    private void SetValueOverMaxText(int value, int maxValue)
    {
        tmpValueOverMax.text = value + "/" + maxValue;
    }


    //get the information read from the json in the UpgradeListLogic
    public void SetInfo(string upgradeName, int value)
    {
        this.upgradeName = upgradeName;
        UpgradeConsistentInfo constUp = UpgradeConsistentInfo.GetUpgradeConsistentInfo(upgradeName);
        this.upgradePrice=constUp.CalculatePrice(value);
        this.maxLevel = constUp.maxLevel;
        SetDisplay(value);
    }

    //set the display to the current level
    public void SetDisplay(int value)
    {
        SetTitleText();
        SetSliderDisplay(value, maxLevel);
        SetValueOverMaxText(value, maxLevel);
        DisplayPrice(value >= maxLevel);
        FreeChance();
    }

    private void ReCalculatePrice(int value)
    {
        this.upgradePrice = UpgradeConsistentInfo.GetUpgradeConsistentInfo(upgradeName).CalculatePrice(value);
    }

    private void DisplayPrice(bool isMax)
    {
        if (isMax)
        {
            PriceDisplay.text = "-";
            DisableButton();
        }
        else
        {
            PriceDisplay.text = upgradePrice.ToString();
        }
    }

    private void DisableButton()
    {
        button.interactable = false;
        buttonText.text = "MAXED OUT";
    }

 

    public void ButtonUpgrade()
    {
        List<UpgradeInfo> upgradeInfos = FileHandler.ReadListFromJSon<UpgradeInfo>(UpgradeInfo.upgradeFileName);

        try
        {
            if (Coin.IsMoneyEnough(upgradePrice))
            {

                //TBI: if level is less than maxLevel
                //get the upgrade info from the static list
                UpgradeInfo upgradeInfo = UpgradeInfo.FindUpgradeInfoByName(upgradeName, upgradeInfos);
                if (upgradeInfo != null)
                {
                    //if the upgrade isnt maxed up
                    if (maxLevel > upgradeInfo.level)
                    {
                        //pay the price and update the coins in json and on screen
                        CoinDisplayLogic.PayUpgrade(upgradePrice);
                        upgradeInfo.level++;
                        upgradeInfo.RaiseAbilityMultiplier();

                        //save in json after the upgrade
                        FileHandler.SaveToJSon(upgradeInfos, UpgradeInfo.upgradeFileName);
                        
                        if (maxLevel > upgradeInfo.level)
                        {
                            UpgradeConsistentInfo constUp = UpgradeConsistentInfo.GetUpgradeConsistentInfo(upgradeName);
                            this.upgradePrice = constUp.CalculatePrice(upgradeInfo.level);
                            DisplayPrice(isMax:false);
                        }
                        else
                        {
                            DisplayPrice(isMax:true);

                        }
                        SetDisplay(upgradeInfo.level);
                    }
                    else
                    {
                            print("upgrade already maxed out");
                    }
                }
                else
                {
                    Debug.LogError("upgrade name could not be found on file");
                }
            }
            else
            {
                print("not enough coins");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        } 
    }
   

    void Upgrade()
    {
        List<UpgradeInfo> upgradeInfos = FileHandler.ReadListFromJSon<UpgradeInfo>(UpgradeInfo.upgradeFileName);
        UpgradeInfo upgradeInfo = UpgradeInfo.FindUpgradeInfoByName(upgradeName, upgradeInfos);
        if (upgradeInfo != null)
        {
            //if the upgrade isnt maxed up
            if (maxLevel > upgradeInfo.level)
            {
                //UpgradeForFree
                upgradeInfo.level++;
                upgradeInfo.RaiseAbilityMultiplier();

                //save in json after the upgrade
                FileHandler.SaveToJSon(upgradeInfos, UpgradeInfo.upgradeFileName);

                if (maxLevel > upgradeInfo.level)
                {
                    UpgradeConsistentInfo constUp = UpgradeConsistentInfo.GetUpgradeConsistentInfo(upgradeName);
                    this.upgradePrice = constUp.CalculatePrice(upgradeInfo.level);
                    DisplayPrice(isMax: false);
                }
                else
                {
                    DisplayPrice(isMax: true);

                }
                SetDisplay(upgradeInfo.level);
            }
            else
            {
                print("upgrade already maxed out");
            }
        }
        else
        {
            Debug.LogError("upgrade name could not be found on file");
        }
    }

    void FreeChance()
    {
        float r = Random.Range(0f, 1f);
        if(r < 0.1f)
        {
            freeUpgradeButton.gameObject.SetActive(true);
        }
    }

    public void FreeUpgrade()
    {
        StartCoroutine(AdWindowScript.PlayAd(AdWindow));
        Upgrade();
        freeUpgradeButton.gameObject.SetActive(false);
    }
}
