using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARControl : Control
{
    ARSessionOrigin arOrigin;
    Pose placementPose;
    bool placementPoseIsValid;

    private Ball ball;
    private CanvasMessageManager canvasMessageManager;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
    }

    void Start()
    {
        XRSettings.LoadDeviceByName("");
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        canvasMessageManager.DisplayShowMenuButton();
    }

    void Update()
    {
        updatePlacementPose();
        updatePlacementIndicator();
    }

    void updatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.position = placementPose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    void updatePlacementPose()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        bool success = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000);
        placementPoseIsValid = success;
        if (success)
        {
            placementPose.position = hit.point + hit.normal * 0.05f;
            indicator.transform.up = hit.normal;
            placementPose.rotation = Math.getBearing(Camera.main.transform.forward);

            //shoot
            if (!ball.GetInPlay())
            {
                if(Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                    Input.GetTouch(1).phase == TouchPhase.Began))
                {
                    ShootBall(hit);                  
                }               
            }
        }
    }

    public override PlacementPose getPlacementPose()
    {
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return new PlacementPose()
            {
                pose = placementPose
            };
        }

        return null;
    }

    private void ShootBall(RaycastHit hit)
    {
        canvasMessageManager.TakeOneLife();
        ball.SetInPlayTrue();

        if (ball.GetBallRigidBody() != null)
        {
            ball.GetBallRigidBody().velocity = (hit.point - ball.GetBallRigidBody().transform.position).normalized
            * ball.GetLaunchForce();

        }
    }
}
