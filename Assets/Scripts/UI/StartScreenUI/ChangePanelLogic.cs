using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelLogic : MonoBehaviour
{
    [Header("Panels Locations")]
    [SerializeField] float[] panelLocations = new float[4];
    public float easing;
    void Start()
    {
        easing = 0.5f; //how many seconds for the transition to happen
        for(int i = 0; i < panelLocations.Length; i++) 
        {
            panelLocations[i] = -i * transform.parent.GetComponent<RectTransform>().rect.width;
        }
        transform.localPosition = new Vector3(panelLocations[2], 0 , 0); //start on the game panel

    }

    void Update()
    {
        
    }


    public void MoveToPanel(int panelIndex)
    {
        Vector3 newLocation = new Vector3(panelLocations[panelIndex], 0, 0);
        StartCoroutine(SmoothMove(transform.localPosition, newLocation, easing));
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds) // move from start position to end position smoothly over number of given seconds
    {
        float t = 0f;
        while(t<= 1.0f)
        {
            t+= Time.deltaTime/seconds;
            transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t)); //smoothstep is like lerp (returns a number between two numbers given based on a third number that is between 0 and 1, closer to 0 returns closer to the first number, closer to 1 retruns a number closer to the second number) except it gradually speeds up from the start and slow down to the end, causing a smoother transition than lerp
            yield return null;
        }
    }

}
