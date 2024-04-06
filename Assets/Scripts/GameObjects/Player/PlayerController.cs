using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("EditMove")]
    [SerializeField] bool editMode;

    [Header("Player Character Contoller")]
    [SerializeField] CharacterController cC;

    [Header("Player Animation Controller")]
    [SerializeField] PlayerAnimationController animController;

    [Header("Player Attributes")]
    [SerializeField] float horizantalVelocity;
    [SerializeField] float onWallVelocity;
    [SerializeField] float horizantalGlidingVelocity;
    [SerializeField] float glidingDesentVelocity;
    [SerializeField] float direction;
    
    [SerializeField] bool grounded;
    [SerializeField] bool hold;
    [SerializeField] bool onWall;
    [SerializeField] bool gliding;
    
    [SerializeField] float gravity;

    [Header("Stamina Data")]
    public float maxStamina = 100;
    [SerializeField] float staminaDepletion = 20;
    [SerializeField] float staminaRestoration = 40;
    
    public float stamina;

    [Header("Jump Info")]
    [SerializeField] float jumpVelocity;
    [SerializeField] int maxJumps;
    [SerializeField] int restoredJumpsOffWall;
    [SerializeField] int jumps;

    [Header("TimeMAnagment")]
    [SerializeField] float timeOnHold;
    [SerializeField] float holdTimeIndicater;

    [Header("GameLogic")]
    [SerializeField] GameLogic gameLogic;

    [Header("Game Statistics")]
    [SerializeField] GameStatistics gameStatictics;

    [Header("Collision check parameters & Layers Masks")]
    [SerializeField] LayerMask platformMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask springMask;
    [SerializeField] LayerMask kunaiMask;
    [SerializeField] float maxRaycastDistance;
    [SerializeField] Vector3 playerRadius;
    [SerializeField] Transform collisionCheck;
    [SerializeField] float offset;

    [Header("Player Corners")]
    [SerializeField] Transform topRight;
    [SerializeField] Transform topLeft;
    [SerializeField] Transform buttomRight;
    [SerializeField] Transform buttomLeft;

    [Header("Movement Vectors")]
    [SerializeField] Vector3 applyVelocity;
    [SerializeField] Vector3 applyGravity;

    [Header("Kunai and Spring target on use")]
    [SerializeField] Vector3 target;
    [SerializeField] float transsitionSpeed;

    [Header("Spring Attributes")]
    //spring attributes and managment
    [SerializeField] float springJumpHeight;

    [Header("Kunai Attributes")]
    //kunai attributes and managment
    [SerializeField] float kunaiTravelDistance;

    [Header("SoundController")]
    [SerializeField] PlayerSounds pS;

    [Header("Gizmo on/off")]
    [SerializeField] bool gizmoOn;

    Vector3 targetWorldPos;
    int lastPlatform;
    bool offWall;

    Vector3 sideHitR, sideHitL;
    
    // Start is called before the first frame update
    void Start()
    {
        gizmoOn = true;
        //---------------------------------------------------------------
        // Importing Data from database
        // importing spring velocity, kunai distance.
        // player horizantal speed, wallRunspeed, horizantalGlideSpeed
        // glideVerticalDisede, jumpHeight,maxJumps, restoredjumpsOffWall
        //
        //---------------------------------------------------------------
        SetPlayerAttributs();
        cC = GetComponent<CharacterController>();
        hold = false;
        onWall = false;
        grounded = false;
        gliding = false;
        editMode = false;
        offWall = false;
        direction = 1;
        applyGravity = Vector3.zero;
        applyVelocity = Vector3.zero;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnviroment();
        
        if (!editMode && gameLogic.active)
        {
            pS.active = true;

            //if click\touch screen jump
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
                timeOnHold = 0;
            }
            //if hold long enough indicate the hold
            else if (Input.GetMouseButton(0))
            {
                timeOnHold += Time.deltaTime;
                if (timeOnHold >= holdTimeIndicater)
                {
                    hold = true;
                }
            }
            //when release hold indicate hold release
            else if (Input.GetMouseButtonUp(0))
            {
                hold = false;
                gliding = false;
            }

            //if holding apply the correct action
            if (hold && stamina > 0)
            {
                
                //if on wall then change velocity to wall run
                if (onWall)
                {
                    stamina -= staminaDepletion * Time.deltaTime;
                    WallRun();
                    //CheckPlatformSkipped();
                }
                //if not on the ground glide
                else if (!grounded && applyGravity.y < 0)
                {
                    stamina -= staminaDepletion * Time.deltaTime;
                    Gliding();
                }
            }
            else
            {
                stamina = Mathf.Clamp(stamina + staminaRestoration * Time.deltaTime, 0, maxStamina);
            }
            if (stamina <= 0)
            {
                gliding = false;
                onWall = false;
                hold = false;
            }

            if (targetWorldPos.y - transform.position.y < 0 && (gameLogic.spring || gameLogic.kunai))
            {
                gameLogic.spring = false;
                gameLogic.kunai = false;
                applyGravity.y = 0;
            }

            if (gameLogic.kunai || gameLogic.spring)
            {
                cC.Move(target * transsitionSpeed * Time.deltaTime);
            }

            else
            {
                //apply the player velocity and movement
                PlayerMovement();
            }

            if (transform.position.y <= gameLogic.bottom.position.y)
            {
                gameLogic.GameOver();
                pS.active = false;
            }
        }
        else
        {
            pS.active = false;
        }
    }

    void SetPlayerAttributs()
    {
        PlayerMultiplyer.LoadDataFromJson();
        horizantalVelocity = PlayerBaseAttributes.baseAttributes[0] * PlayerMultiplyer.multAttributes[0];
        onWallVelocity = PlayerBaseAttributes.baseAttributes[1] * PlayerMultiplyer.multAttributes[1];
        horizantalGlidingVelocity = PlayerBaseAttributes.baseAttributes[2] * PlayerMultiplyer.multAttributes[2];
        jumpVelocity = PlayerBaseAttributes.baseAttributes[3]   * PlayerMultiplyer.multAttributes[3];   
        maxJumps = (int)(PlayerBaseAttributes.baseAttributes[4] + PlayerMultiplyer.multAttributes[4]);
        restoredJumpsOffWall = (int)(PlayerBaseAttributes.baseAttributes[5] + PlayerMultiplyer.multAttributes[5]);
        restoredJumpsOffWall = Mathf.Clamp(restoredJumpsOffWall, 0, maxJumps);
        maxStamina = PlayerBaseAttributes.baseAttributes[6] * PlayerMultiplyer.multAttributes[6];
        staminaRestoration = PlayerBaseAttributes.baseAttributes[7] * PlayerMultiplyer.multAttributes[7];
        staminaDepletion= PlayerBaseAttributes.baseAttributes[8]    * PlayerMultiplyer.multAttributes[8];
        kunaiTravelDistance = PlayerBaseAttributes.baseAttributes[9] * PlayerMultiplyer.multAttributes[9];
        springJumpHeight = PlayerBaseAttributes.baseAttributes[10] * PlayerMultiplyer.multAttributes[10];
    }

    void CheckEnviroment()
    {
        CheckGrounded();
        CheckOnWall();
        CheckPlatformsInVacinity();
    }

    void Jump()
    {
        //Debug.Log("Attempt Jump");
        if (jumps < maxJumps)
        {
            grounded = false;
            jumps++;
            applyGravity.y = 0;
            applyGravity.y += jumpVelocity;
            pS.Jump();
            animController.Jump();
        }
    }

    //remove gravity and set the vertical velocity
    void Gliding()
    {
        //Debug.Log("Gliding");
        gliding = true;
        applyVelocity = new Vector3(direction * horizantalGlidingVelocity, 0, 0);
        applyGravity.y = glidingDesentVelocity;
        gameStatictics.ResetPlatformClearedWithoutGliding();
        pS.runing = false;
        animController.MidAir();
    }

    //remove gravity and apply vertical velocity only to run on wall
    void WallRun()
    {
        applyVelocity = Vector3.zero;
        applyGravity.y = onWallVelocity;
        pS.runing = true;
        animController.WallRun(direction);
    }

    //jump of wall and turn direction
    void JumpOffWall()
    {
        direction *= -1;
        //animation turn
        onWall = false;
        pS.OffWall();
        animController.OffWall(direction);

    }

    //apply the player movement
    void PlayerMovement()
    {
        if (!onWall && !gliding)
        {
            //Debug.Log("apply not hold velocity");
            applyVelocity.x = direction * horizantalVelocity;
            if (!grounded)
                applyGravity.y = Mathf.Clamp(applyGravity.y + gravity * Time.deltaTime, -20, 20);
        }
        cC.Move(applyVelocity * Time.deltaTime);
        cC.Move(applyGravity * Time.deltaTime);
    }

    //check if the platform is above or bellow to activate and deactivate collision
    // allowing to pass through the platform from underneath and stop passing through from above
    void CheckPlatformsInVacinity()
    {
        RaycastHit hit;

        //-----------------------------------------------------------------------------------------------
        // check left and right cast on platforms

        //if there is a platform to the right of the player, then ignore collision
        //if (Physics.BoxCast(transform.position, new Vector3(maxRaycastDistance, (topLeft.position.y - buttomLeft.position.y)*4/5, 1), transform.right, out hit, transform.rotation, maxRaycastDistance, platformMask))
        //{
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
        //    if(gameLogic.changeColor)
        //        hit.transform.GetComponent<Renderer>().material.color = Color.red;
        //    sideHitR = hit.transform.position;
        //}


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        //check with loaclScale istead of topRight - bottumRight
        if (Physics.BoxCast(transform.position, transform.localScale * 4/5, transform.right, out hit, transform.rotation, maxRaycastDistance, platformMask))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
            sideHitR = hit.point;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------

        //if there is a platform to the left of the player, then ignore collision
        //if (Physics.BoxCast(transform.position, new Vector3(1, (topLeft.position.y - buttomLeft.position.y) * 4 / 5, 1), -transform.right, out hit, transform.rotation, maxRaycastDistance, platformMask))
        //{
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
        //    if (gameLogic.changeColor)
        //        hit.transform.GetComponent<Renderer>().material.color = Color.red;
        //    sideHitL = hit.transform.position;
        //}


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        //check with loaclScale istead of topRight - bottumRight
        if (Physics.BoxCast(transform.position, transform.localScale * 4/5, -transform.right, out hit, transform.rotation, maxRaycastDistance, platformMask))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
            sideHitL = hit.point;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------
        //Check up and down cast on platforms

        //if there is a pltform above the player from the left, then ignore the collision
        if (Physics.Raycast(topLeft.position, transform.up, out hit, maxRaycastDistance, platformMask) )
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        //if there is a pltform above the player from the right, then ignore the collision
        if (Physics.Raycast(topRight.position, transform.up, out hit, maxRaycastDistance, platformMask))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
        }

        //if there is a platform underneath the player from the left, make them collideable
        if (Physics.Raycast(buttomLeft.position, -transform.up, out hit, maxRaycastDistance, platformMask))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), false);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.green; 
        }
        //if there is a platform underneath the player from the right, make them collideable
        if (Physics.Raycast(buttomRight.position, -transform.up, out hit, maxRaycastDistance, platformMask))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), false);
            if (gameLogic.changeColor)
                hit.transform.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    //indicate if the player on a platform
    void CheckGrounded()
    {
        //check if hit the platform
        Collider[] hit = Physics.OverlapBox(collisionCheck.position, playerRadius, transform.rotation, platformMask);
        //check if above the platform
        bool abovePlatform = Physics.Raycast(buttomLeft.position, -transform.up, offset) ||
            Physics.Raycast(buttomRight.position, -transform.up, offset);
        //check if on the ground
        if (hit.Length > 0 && abovePlatform)
        {
            //check if we are falling not jumping over the platfrom
            if (applyGravity.y <= 0)
            {
                jumps = 0;
                
                //notify the platform that it been cleared
                foreach(Collider c in hit)
                {
                    if(c.transform.position.y < buttomLeft.position.y)
                    {
                        PlatformMovement platform = c.transform.GetComponent<PlatformMovement>();

                        if (platform != null)
                        {
                            UpdateGameStatistics(platform);
                        }
                        //need to be ereased
                        else
                        {
                            Debug.Log("No class in object");
                            platform = hit[0].transform.GetComponentInParent<PlatformMovement>();
                            if(platform != null)
                            {
                                UpdateGameStatistics(platform);
                            }
                            Debug.Log("No class in child");
                        }
                    } 
                }
                grounded = true;
                pS.Land();
                animController.Land();
            }
        }
        else
        {
            grounded = false;
            pS.landed = false;
        }
    }

    //updating gamestatistics when clear a platform
    void UpdateGameStatistics(PlatformMovement platform)
    {
        if(!platform.cleared)
        {
            platform.Cleared();
            gameStatictics.platformsClearedWithoutGliding++;
        }
        if (offWall)
        {
            gameStatictics.platformsSkippedWhileWallRun = platform.platformNumber - lastPlatform;
            offWall = false;
        }
        
        lastPlatform = platform.platformNumber;
    }

    //indicate if the player on a wall
    void CheckOnWall()
    {
        //check if on the wall
        if (Physics.CheckBox(collisionCheck.position, playerRadius, transform.rotation, wallMask) &&
            Physics.Raycast(collisionCheck.position, Vector3.right * direction, playerRadius.x, wallMask))
        {
            if (!onWall)
            {
                jumps = Mathf.Clamp(jumps - restoredJumpsOffWall, 0, maxJumps);
            }
            onWall = true;
            if (!hold)
            {
                JumpOffWall();
                offWall = true;
                gameStatictics.ResetPlatformSkippedWhileWallRun();
            }
        }
    }


    public void KunaiJump()
    {
        if (!gameLogic.kunai)
        {
            float xTarget = transform.position.x > 0 ? gameLogic.rightWall.position.x : gameLogic.leftWall.position.x;
            target = new Vector3(xTarget, kunaiTravelDistance, 0);
            targetWorldPos = target + Vector3.up * transform.position.y;
            gameLogic.kunai = true;
            gameStatictics.ObjectsUsed++;
            pS.Kunai();
            animController.Jump();
        }
    }

    public void SpringJump()
    {
        if (!gameLogic.spring)
        {
            target = new Vector3(0, springJumpHeight, 0);
            targetWorldPos = transform.position + target;
            gameLogic.spring = true;
            gameStatictics.ObjectsUsed++;
            pS.Spring();
            animController.Jump();
        }
    }
    
    
   
    
    //draw outline in scene
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = Color.red;
            //drawing the collision check vacinity
            Gizmos.DrawCube(collisionCheck.position, playerRadius);
            
            //drawing the rays to check collision with platforms
            //red mean block passing, green mean allow passing
            Gizmos.DrawRay(buttomLeft.position, -transform.up * maxRaycastDistance);
            Gizmos.DrawRay(buttomRight.position, -transform.up * maxRaycastDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(topLeft.position, transform.up * maxRaycastDistance);
            Gizmos.DrawRay(topRight.position, transform.up * maxRaycastDistance);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(buttomRight.position, Vector3.right * gameLogic.edges * 2);
            Gizmos.DrawWireCube(transform.position + Vector3.right * maxRaycastDistance/2, new Vector3(maxRaycastDistance, transform.localScale.y * 4 / 5, transform.localScale.z * 4 / 5));
            Gizmos.DrawRay(buttomLeft.position, Vector3.left * gameLogic.edges * 2);
            Gizmos.DrawWireCube(transform.position + Vector3.left * maxRaycastDistance / 2, new Vector3(maxRaycastDistance, transform.localScale.y * 4 / 5, transform.localScale.z * 4 / 5));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(sideHitR, 0.5f);
            Gizmos.DrawWireSphere(sideHitL, 0.5f);
        }
    }
}