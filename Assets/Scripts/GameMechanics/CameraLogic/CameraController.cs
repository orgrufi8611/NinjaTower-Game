using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("ninja data")]
    [SerializeField] GameObject ninja;
    [SerializeField] Transform ninjaPosition;
    [SerializeField] float distance = 0;

    [Header("MidScreen")]
    [SerializeField] Transform midScreen;
    [SerializeField] float maxHeightBeforeActivation;
    
    [Header("Camera movement parameters")]
    public float speed;
    public float normalSpeed;
    public float speedFactor;


    
    public bool active = true;
    public bool follow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        maxHeightBeforeActivation = midScreen.position.y;
        speed = normalSpeed;
        ninjaPosition = ninja.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (follow)
            {
                if(ninjaPosition.position.y - transform.position.y > 0)
                {
                    transform.Translate(0, (ninjaPosition.position.y - transform.position.y) * speed * 15 * Time.deltaTime, 0);
                }
            }
            else
            {
                if (ninjaPosition.position.y < maxHeightBeforeActivation)
                {
                    return;
                }

                distance = ninjaPosition.position.y - midScreen.position.y;
                if (distance > 0)
                {
                    speed += speed * 2 * Time.deltaTime;
                }
                else if(distance < -maxHeightBeforeActivation/3)
                {
                    speed = normalSpeed;
                }
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
    }
}
