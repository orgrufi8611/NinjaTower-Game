using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPainter : MonoBehaviour
{
    public Material wallpaper;
    [SerializeField] MeshRenderer rendererBlender;
    [SerializeField] MeshRenderer rendererFDX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rendererBlender.material = wallpaper;
        rendererFDX.material = wallpaper;
    }
}
