using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeSliderScript : MonoBehaviour
{
    [SerializeField] GameObject fillElement;
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] RectTransform fillArea;
    [SerializeField] Slider upgradeSlider;
    public int maxValue;
    public int currValue;
    public Vector2 dimentions;
    public Vector2 cellSize;
    List<GameObject> levels = new List<GameObject>();
    bool init;
   
    private void Start()
    {
        init = false;
        dimentions = new Vector2(fillArea.rect.width, fillArea.rect.height);
        cellSize = new Vector2(fillArea.rect.width, fillArea.rect.height);
        ResizeCells();
    }
    public void SetMax(int max)
    {
        maxValue = max;
        if(cellSize.magnitude > 0)
        {
            ResizeCells();
        }
    }

    private void Update()
    {
        if(!init && cellSize.magnitude > 0)
        {
            ResizeCells();
            init = true;
        }
    }

    void ResizeCells()
    {
        cellSize.x = dimentions.x / maxValue;
        //Debug.Log("Change CellSize to " + cellSize.x + " , " + cellSize.y);
        gridLayout.cellSize = new Vector2(cellSize.x, cellSize.y);
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.enabled = false;
        gridLayout.enabled = true;
    }

    public void SetLevelStart(int level)
    {
        currValue = level;
        if(levels.Count < currValue)
        {
            for(int i = levels.Count; i < currValue; i++)
            {
                levels.Add(Instantiate(fillElement,transform.position,transform.rotation));
                levels[i].transform.SetParent(gridLayout.transform);
            }
        }
        
    }
    
}
