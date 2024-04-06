using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform characterTransform;
    public Transform meshTransform;
    public Vector3 meshRotation;
    public float direction;
    public Vector3 initialRotation;
    public Vector3 currRotation;
    public ParticleSystem jumpEffect;
    string fall = "Land", jump = "Jump", run = "Run", wallRun = "WallRun",midAir = "MidAir";
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        animator.SetBool(fall, false);
        animator.SetBool(jump, false);
        animator.SetBool(wallRun, false);
        initialRotation = characterTransform.localRotation.eulerAngles;
        currRotation = initialRotation;
    }

    private void Update()
    {
        meshTransform.localPosition = Vector3.zero; 
        meshTransform.localRotation = Quaternion.Euler(meshRotation);
    }


    public void WallRun(float pDirection)
    {
        direction = pDirection;
        currRotation.x = -90;
        characterTransform.localRotation = Quaternion.Euler(currRotation);
        characterTransform.localPosition = Vector3.right * direction * 0.5f;
        animator.SetBool (wallRun, true);
        animator.SetBool(run, false);
    }

    public void OffWall(float pDirection)
    {
        direction = pDirection;
        currRotation.y = direction * initialRotation.y;
        currRotation.x = initialRotation.x;
        characterTransform.localRotation = Quaternion.Euler(currRotation);
        characterTransform.localPosition = Vector3.down;
        animator.SetBool(midAir, true);
        animator.SetBool(wallRun, false);
    }
    public void Jump()
    {
        animator.SetTrigger (jump);
        animator.SetBool(run, false);
        animator.SetBool(fall, false);
        characterTransform.localPosition = Vector3.down;
        jumpEffect.Play();
    }

    public void Land()
    {
        characterTransform.localPosition = Vector3.down;
        animator.SetBool(fall, true);
        animator.SetBool(midAir, false);
        animator.SetBool(run, true);
        animator.SetBool(jump, false);
    }

    public void MidAir()
    {
        animator.SetBool(midAir, true);
    }
}
