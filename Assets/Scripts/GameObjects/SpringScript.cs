using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
    public GameLogic gameLogic;
    [SerializeField] Transform collisionCheck;
    [SerializeField] LayerMask player;
    [SerializeField] Vector3 area;
    bool jumped;

    [Header("Gizmo on/off")]
    [SerializeField] bool gizmoOn;
    // Start is called before the first frame update
    void Start()
    {
        gizmoOn = true;
       jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckBox(collisionCheck.position, area,transform.rotation,player) && !jumped)
        {
            //Debug.Log("Spring Jump");
            gameLogic.player.gameObject.GetComponent<PlayerController>().SpringJump();
            jumped = true;
            //animation for spring jump
        }
        else if(!Physics.CheckBox(collisionCheck.position, area, transform.rotation, player))
        {
            jumped = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(collisionCheck.position, area);
        }
    }
}
