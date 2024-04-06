using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdWindowScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    float time;
    int timeDisplayed;
    public int timeToPlay;
    // Start is called before the first frame update
    void Start()
    {
        timeToPlay = 3;
        Destroy(gameObject, timeToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeDisplayed =timeToPlay - (int)time;
        timer.text = timeDisplayed.ToString();
    }

    public static IEnumerator PlayAd(Image AdWindow)
    {
        Image ad = Instantiate(AdWindow,parent: GameObject.Find("Ad").transform);
        ad.rectTransform.localPosition = Vector3.zero;
        ad.rectTransform.localScale = Vector3.one;
        yield return new WaitForSeconds(3);
        //Debug.Log("Attempt Ad distruction");
        //foreach (Transform child in ad.rectTransform)
        //{
        //    Destroy(child.gameObject);
        //}
        //Destroy(ad.gameObject);
    }
}
