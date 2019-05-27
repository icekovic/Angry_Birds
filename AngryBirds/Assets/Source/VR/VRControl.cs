using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRControl : Control
{
    bool canPlace = false;
    Vector3 currentHit;
    float placeCooldown = 0;

    private Ball ball;
    private CanvasMessageManager canvasMessageManager;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
    }

    public void Start()
    {
        StartCoroutine(LoadDevice("cardboard"));
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }

    public override PlacementPose getPlacementPose()
    {
        if (canPlace && placeCooldown <= 0 && currentHit != Vector3.zero)
        {
            placeCooldown = 2f;
            return new PlacementPose()
            {
                pose = new Pose(currentHit, Math.getBearing(Camera.main.transform.forward))
            };
        }
        return null;
    }

    public void Update()
    {
        var camera = Camera.main;
        
        if (Mathf.Abs(Mathf.DeltaAngle(camera.transform.eulerAngles.z, 0)) > 25)
        {
            canPlace = true;
        }
        else
        {
            canPlace = false;
        }

        if (placeCooldown > 0)
        {
            placeCooldown -= Time.deltaTime;
        }

        bool success = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000);
        if (success)
        {
            currentHit = hit.point;
            indicator.transform.position = currentHit + hit.normal * 0.05f;
            indicator.transform.up = hit.normal;
            indicator.transform.localScale = Vector3.one * 2;
            // Vector3 lookAt = Vector3.Cross(-hit.normal, Camera.main.transform.right);
            // lookAt = lookAt.y < 0 ? -lookAt : lookAt;
            // indicator.transform.rotation = Quaternion.LookRotation(hit.point + lookAt, hit.normal);
            // Debug.Log(hit.point);

            Debug.Log(camera.transform.eulerAngles.x);

            //shoot ball
            //if (Input.GetKeyDown(KeyCode.Space) && !ball.GetInPlay())
            if((camera.transform.eulerAngles.x > 5 && camera.transform.eulerAngles.x < 10) && !ball.GetInPlay())
            {
                ShootBall(hit);     
            }

            if(camera.transform.eulerAngles.x > 30 && camera.transform.eulerAngles.x < 60)
            {               
                canvasMessageManager.ShowMenu();
            }

            CheckWhichButtonIsLooked(hit);
        }
        else
        {
            currentHit = Vector3.zero;
        }
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

    private void CheckWhichButtonIsLooked(RaycastHit hit)
    {
        if (hit.collider.tag.Equals("RestartLevel"))
        {
            canvasMessageManager.RestartLevel();
            canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }

        if (hit.collider.tag.Equals("MainMenu"))
        {
            canvasMessageManager.MainMenu();
            canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }

        if (hit.collider.tag.Equals("PlayAgain") || hit.collider.tag.Equals("RestartGame"))
        {
            canvasMessageManager.PlayAgain();
            canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }
    }
}