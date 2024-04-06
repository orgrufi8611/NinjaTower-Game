using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutToScreenSize : MonoBehaviour
{
    float width;
    float height;

    GridLayoutGroup gridLayout;
    // Start is called before the first frame update
    void Start()
    {
     
    }
    private void Update()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        width = transform.parent.GetComponent<RectTransform>().rect.width;
        height = transform.parent.GetComponent<RectTransform>().rect.height;
        gridLayout.cellSize = new Vector2(width, height);
    }
}
