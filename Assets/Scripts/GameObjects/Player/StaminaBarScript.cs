using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    [SerializeField] Slider staminaBar;
    [SerializeField] PlayerController playerMovement;
    float max;
    float min;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        min = 0;
        max = playerMovement.maxStamina;
        staminaBar.maxValue = max;
        staminaBar.value = max;
        staminaBar.minValue = min;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = playerMovement.stamina;
    }
}
