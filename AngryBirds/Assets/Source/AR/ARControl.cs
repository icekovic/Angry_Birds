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
            //zbog jednostavnosti koristim lijevu i desnu tipku  miša
            //left mouse click
            if(Input.GetMouseButtonDown(0))
            {
                firstClick = Input.mousePosition;
                Debug.Log("first click: " + firstClick);
            }

            //right click
            if (Input.GetMouseButtonDown(1))
            {
                secondClick = Input.mousePosition;
                Debug.Log("second click: " + secondClick);
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
                    projectileSpeed = Mathf.Abs(Vector3.Distance(firstTouch.position, secondTouch.position));
                }
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //clicks[0] = Input.mousePosition;
            //firstClick = Input.mousePosition;
            //secondClick = Input.mousePosition;

            //Debug.Log("first click: " + firstClick);
            //Debug.Log("second click: " + secondClick);
            //Debug.Log("razlika klikova: " + Mathf.Abs(Vector3.Distance(firstClick, secondClick)));
            //}

            //shoot
            //if (!ball.GetInPlay())
            //{
            //if(Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
            //    Input.GetTouch(1).phase == TouchPhase.Began))
            //{
            //    Vector2 touchDifference = Input.GetTouch(1).position - Input.GetTouch(0).position;
            //    Debug.Log(touchDifference);
            //    ShootBall(hit);
            //}  

            //for(int i = 0; i < Input.touchCount; ++i)
            //{
            //    if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            //    {
            //        Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            //        if (Physics.Raycast(ray, out hit))
            //        {
            //            ShootBall(hit);
            //        }
            //            //hit.transform.gameObject.SendMessage("OnMouseDown");
            //        //ShootBall(hit);
            //    }
            //}

            //shotDelayTimer += Time.deltaTime;

            //if (!ball.GetInPlay())
            //{
            //    if (shotDelayTimer > 3.0f)
            //    {
            //        ShootBall(hit);
            //        shotDelayTimer = 0;
            //    }
            //}
            //}
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
