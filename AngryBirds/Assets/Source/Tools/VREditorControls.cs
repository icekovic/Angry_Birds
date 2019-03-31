using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VREditorControls : MonoBehaviour
{
    public Transform target;

    Vector3 tiltTarget;
    float speed = 20;

    void Update()
    {
        tiltTarget.x -= Input.GetKey(KeyCode.W) ? Time.deltaTime * speed : 0;
        tiltTarget.x += Input.GetKey(KeyCode.S) ? Time.deltaTime * speed : 0;
        
        tiltTarget.y -= Input.GetKey(KeyCode.A) ? Time.deltaTime * speed: 0;
        tiltTarget.y += Input.GetKey(KeyCode.D) ? Time.deltaTime * speed: 0;


        tiltTarget.z = Input.GetKey(KeyCode.Q) ? 30: 0;

        target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.Euler(tiltTarget), Time.deltaTime * 45);
    }
}
