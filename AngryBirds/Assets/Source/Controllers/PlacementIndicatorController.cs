using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicatorController : MonoBehaviour
{
    private CanvasMessageManager canvasMessageManager;

    private void Awake()
    {
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("RestartLevel"))
        {
            canvasMessageManager.RestartLevel();
        }
    }
}
