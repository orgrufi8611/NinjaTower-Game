using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameLogic gameLogic;
    [SerializeField] Transform collisionCheck;
    [SerializeField] LayerMask player;
    [SerializeField] Vector3 radius;
    [SerializeField] AudioClip collect;
    AudioSource aS;
    Animator animator;
    bool collected;
    [Header("Gizmo on/off")]
    [SerializeField] bool gizmoOn;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Collect", false);
        aS = GetComponent<AudioSource>();
        gizmoOn = true;
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckBox(collisionCheck.position, radius, transform.rotation, player) && !collected)
        {
            collected = true;
            gameLogic.addCoin();
            //animation for coin collection
            aS.PlayOneShot(collect);
            animator.SetBool("Collect",true);
        }
        
    }

    public void DestroyOnEndOfAnimation()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(gizmoOn)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(collisionCheck.position, radius);
        }
    }
}
