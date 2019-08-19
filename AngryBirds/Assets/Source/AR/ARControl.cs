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

    private Vector3 firstClick;
    private Vector3 secondClick;
    private float projectileSpeed = 0;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
    }

    void Start()
    {
        XRSettings.LoadDeviceByName("");
        arOrigin = FindObjectOfType<ARSessionOrigin>();
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

            //clicks -for Unity testing
            //left and right mouse buttons are used for clicks
            //left mouse click - first
            if(Input.GetMouseButtonDown(0))
            {
                firstClick = Input.mousePosition;
            }

            //right click - second
            //the math and launch is made here
            if (Input.GetMouseButtonDown(1))
            {
                secondClick = Input.mousePosition;
                projectileSpeed = Mathf.Abs(Vector3.Distance(firstClick, secondClick));

                //if the speed is high enough higher than 50, for example
                if(projectileSpeed > 50)
                {
                    //the ball is fired
                    ShootBall(hit);
                }

                //else
                if(projectileSpeed > 50)
                {
                    //the shot is cancelled
                    projectileSpeed = 0;
                }
                
            }

            //touches - for mobile
            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                //ako igrač dva puta dotakne ekran i drži 
                if(firstTouch.phase == TouchPhase.Stationary && secondTouch.phase == TouchPhase.Stationary)
                {
                    //računa se brzina
                    projectileSpeed = Mathf.Abs(Vector2.Distance(firstTouch.position, secondTouch.position));

                    //if the speed is high enough higher than 50, for example
                    if (projectileSpeed > 50)
                    {
                        //the ball is fired
                        ShootBall(hit);
                    }

                    //else
                    if (projectileSpeed > 50)
                    {
                        //the shot is cancelled
                        projectileSpeed = 0;
                    }
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
            * projectileSpeed;

        }
    }
}
