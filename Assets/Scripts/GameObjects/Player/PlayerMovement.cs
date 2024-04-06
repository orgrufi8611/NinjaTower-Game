using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [Header("player Info")]
    [SerializeField] bool grounded;
    [SerializeField] bool hold;
    [SerializeField] bool onWall;
    [SerializeField] bool gliding;
    [SerializeField] float horizantalVelocity;
    [SerializeField] float onWallVelocity;
    [SerializeField] float horizantalGlidingVelocity;
    [SerializeField] float glidingDesentVelocity;
    [SerializeField] float direction;
    public float stamina;
    public float maxStamina = 100;
    [SerializeField] float staminaDepletion = 20;
    [SerializeField] float staminaRestoration = 40;

    [Header("Jump Info")]
    [SerializeField] float jumpVelocity;
    [SerializeField] int maxJumps;
    [SerializeField] int restoredJumpsOffWall;
    [SerializeField] int jumps;

    [Header("TimeMAnagment")]
    [SerializeField] float timepass;
    [SerializeField] float timeOnHold;
    [SerializeField] float holdTimeIndicater;

    [SerializeField] GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        timepass = 0;
        rb = GetComponent<Rigidbody>();
        jumps = 0;
        grounded = false;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {

        timepass += Time.deltaTime;
        //jump on touch or mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        //on hold if on wall wallrun, if midair and disending glide
        else if(Input.GetMouseButton(0)) 
        {
            timeOnHold += Time.deltaTime;
            //indicate if we hold for enough time to not cancel a touch or click
            if(timeOnHold > holdTimeIndicater)
            {
                hold = true;
            }
            if (hold && stamina > 0) 
            {
                if (onWall)
                {
                    WallRun();
                    //deplete stamina while holding
                    stamina -= staminaDepletion * Time.deltaTime;
                }
                else if(!grounded && !onWall && rb.velocity.y <= 0)
                {
                    Gliding();
                    //deplete stamina while holding
                    stamina -= staminaDepletion * Time.deltaTime;
                }
            }
        }

        //when touch release or clock release stop all hold actions
        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
            timeOnHold = 0;
            //jump off the wall
            if (onWall)
            {
                JumpOffWall();
            }
            //stop gliding
            if (gliding)
            {
                StopGliding();
            }
        }

        //if stamina fully depleted jump off wall and stop gliding
        if (stamina <= 0)
        {
            if (onWall)
            {
                JumpOffWall();
            }
            else if (gliding)
            {
                StopGliding();
            }
        }

        //if grounded restore stamina
        if (!hold && grounded)
        {
            stamina = Mathf.Clamp(stamina + staminaRestoration * Time.deltaTime, 0, maxStamina);
        }
    }

    void Jump()
    {
        grounded = false;
        if(jumps < maxJumps)
        {
            rb.AddForce(Vector3.up *  jumpVelocity,ForceMode.Impulse);
            jumps++;
        }
    }

    void JumpOffWall()
    {
        direction = transform.position.x < 0 ? 1 : -1;
        onWall = false;
        rb.useGravity = true;
        jumps -= restoredJumpsOffWall;
        rb.velocity = new Vector3(direction * horizantalVelocity, rb.velocity.y, 0);
    }

    void WallRun()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.up * onWallVelocity;
    }

    void StopGliding()
    {
        gliding = false;
        rb.useGravity = true;
        rb.velocity = new Vector3(direction * horizantalVelocity,0,0);
    }
    void Gliding()
    {
        gliding = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(direction * horizantalGlidingVelocity, - glidingDesentVelocity,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            jumps = 0;
            grounded = true;
            gliding = false;
            onWall = false;
            rb.useGravity = true;
            rb.velocity = new Vector3(direction * horizantalVelocity, 0, 0);
            gameLogic.kunai = false;
            gameLogic.spring = false;
        }
        if(collision.gameObject.tag == "Wall")
        {
            onWall = true;
            gliding = false;
            if (!hold)
            {
                JumpOffWall();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            onWall = false;
        }
    }

}
