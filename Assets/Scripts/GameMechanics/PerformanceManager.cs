using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsIndicator;
    [SerializeField] float normalFPS = 30;
    [SerializeField] float currFPS;
    float lagTime;
    int lagCount;
    // Start is called before the first frame update
    void Start()
    {
        lagCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currFPS = 1/Time.deltaTime;
        fpsIndicator.text = currFPS.ToString();
        if(currFPS < normalFPS / 2)
        {
            lagTime += Time.deltaTime;
        }
        else
        {
            if (lagTime > 0)
            {
                lagCount++;
                //Debug.Log("Lagged for " + lagTime);
                lagTime = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Time.fixedDeltaTime = 1f;
        int numberOfPlatforms = GameObject.FindGameObjectsWithTag("Platform").Length;
        //Debug.Log(numberOfPlatforms + " platforms in scene");
        int numberOfBuildingParts = GameObject.FindGameObjectsWithTag("Wall").Length;
        //Debug.Log(numberOfBuildingParts + " buildingParts in scene");
    }
}
