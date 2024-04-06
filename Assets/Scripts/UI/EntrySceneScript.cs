using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntrySceneScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] Animator animator;

    bool transitioned;

    private void Start()
    {
        transitioned = false;
    }

    private void Update()
    {
        if (!transitioned)
        {
            if(Input.GetMouseButtonDown(0))
            {
                transitioned = true;
                animator.SetTrigger("Start");
                title.gameObject.SetActive(false);
                description.gameObject.SetActive(false);
            }
        }
    }

    public void SceneTransition()
    {
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }
}
