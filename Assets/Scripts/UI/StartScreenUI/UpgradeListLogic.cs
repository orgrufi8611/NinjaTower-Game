using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeListLogic : MonoBehaviour
{
    public GameObject cellPrefab, emptyCellPrefab;
    // Start is called before the first frame update
    void Start()
    {
        DisplayList();   
    }


    private void DisplayList()
    {

        //collect the upgrade list from the json file
        List<UpgradeInfo> upgradeInfos = FileHandler.ReadListFromJSon<UpgradeInfo>(UpgradeInfo.upgradeFileName);

        //if the list is empty
        if (!upgradeInfos.Any())
        {
            UpgradeInfo.ResetUpgrades();
            upgradeInfos = FileHandler.ReadListFromJSon<UpgradeInfo>(UpgradeInfo.upgradeFileName);
        }

        foreach (UpgradeInfo upgradeInfo in upgradeInfos)
        {
            //generate the upgrade cell in the list
            GameObject obj = Instantiate(cellPrefab);
            obj.transform.SetParent(this.gameObject.transform, false);
            //initalize the upgrade cell with the correct data
            obj.GetComponent<UpgradeCellLogic>().SetInfo(upgradeInfo.upgradeName, upgradeInfo.level);
        }

    }
}
