using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameObject instance;
    public static string[] sceneNames = new string[] {"MainMenu","GameScene", "EnrtuScene"};
    public static string currScene;
    public Animator animator;

    //load a scene by name
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(SceneLoadWithTransition(sceneName));
    }

    public void ImidiateLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //load scene with cut in and out animation
    IEnumerator SceneLoadWithTransition(string sceneName)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(sceneName);
    }

}
