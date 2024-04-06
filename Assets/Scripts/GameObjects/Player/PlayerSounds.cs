using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] AudioClip run;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip land;
    [SerializeField] AudioClip spring;
    [SerializeField] AudioClip kunai;

    [SerializeField] AudioSource aS;
    public bool active;
    public bool runing;
    public bool landed;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        runing = false;
        landed = false;
        aS = GetComponent<AudioSource>();
        aS.clip = run;

    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            aS.clip = null;
        }
        if (runing)
        {
            if (aS.clip == null)
            {
                aS.clip = run;
                aS.Play();
            }
        }
        else
        {
            aS.clip = null;
        }
    }

    public void Jump()
    {
        aS.PlayOneShot(jump);
        landed = false;
        runing = false;
    }

    public void Land()
    {
        if (!landed)
        {
            //aS.PlayOneShot(land);
            runing = true;
            landed = true;
        }
    }

    public void Kunai()
    {
        aS.PlayOneShot(kunai);
        landed = false;
        runing = false;
    }

    public void Spring()
    {
        aS.PlayOneShot(spring);
        landed = false;
        runing = false;
    }
    public void OffWall()
    {
        if(!landed)
        {
            runing = false;
        }
    }
}
