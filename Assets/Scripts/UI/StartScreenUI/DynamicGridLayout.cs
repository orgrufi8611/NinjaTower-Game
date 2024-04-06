using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{

    public GridLayoutGroup GridLayoutGroup;
    public RectTransform canvasRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        if (GridLayoutGroup != null && canvasRectTransform != null)
        {

            float cellWidth = canvasRectTransform.rect.width;
            float cellHeight = canvasRectTransform.rect.height;

            GridLayoutGroup.cellSize = new Vector2 (cellWidth, cellHeight);
        }
        else
        {
            Debug.LogError("Missing GridLayeoutGroup or Canvas Recttransform reference");
        }
    }

}
