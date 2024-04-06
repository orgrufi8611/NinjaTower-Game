using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteChange: MonoBehaviour
{
    [SerializeField] Sprite mute;
    [SerializeField] Sprite unmute;
    [SerializeField] Image toggle;
    [SerializeField] Toggle check;

    // Update is called once per frame
    void Update()
    {
        if (check.isOn)
        {
            toggle.sprite = unmute;
        }
        else
        {
            toggle.sprite = mute;
        }
    }
}
