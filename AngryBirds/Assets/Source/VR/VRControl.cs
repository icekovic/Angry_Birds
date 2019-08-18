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
    private GameObject vrControl;
    private float projectileSpeed = 0;
    private float projectileChargeTimer = 0;
    //private float shotDelayTimer;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
    }

    public void Start()
    {
        if(!XRSettings.enabled)
        {
            StartCoroutine(LoadDevice("cardboard"));
        }      
        //shotDelayTimer = 0;
        //canvasMessageManager.HideShowMenuButton();
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
        vrControl = GameObject.FindGameObjectWithTag("VRControl");
        //Debug.Log(vrControl.transform.rotation.eulerAngles.y);

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

            //shoot ball
            //if (Input.GetKeyDown(KeyCode.Space) && !ball.GetInPlay())
            //if((camera.transform.eulerAngles.x > 5 && camera.transform.eulerAngles.x < 10) && !ball.GetInPlay())
            //{
            //    ShootBall(hit);     
            //}

            //shotDelayTimer += Time.deltaTime;

            //if (!ball.GetInPlay())
            //{
            //    if(shotDelayTimer > 4.0f)
            //    {
            //        ShootBall(hit);
            //        shotDelayTimer = 0;
            //    }               
            //}

            //if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            //{
            //    Debug.Log("camera: " + camera.transform.rotation.eulerAngles);
            //    Debug.Log("VR control: " + vrControl.transform.rotation.eulerAngles);
            //}

            //charging - E
            if((camera.transform.rotation.eulerAngles.z > 20))
            {
                //Debug.Log("camera: " + camera.transform.rotation.eulerAngles);
                projectileChargeTimer += Time.deltaTime;
                projectileSpeed += 1;
            }

            //shooting - Q
            if((camera.transform.rotation.eulerAngles.z > 300 && camera.transform.rotation.eulerAngles.z < 330 && !ball.GetInPlay()))
            {
                ShootBall(hit);
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
            * projectileSpeed;
        }
    }

    private void CheckWhichButtonIsLooked(RaycastHit hit)
    {
        if (hit.collider.tag.Equals("RestartLevel"))
        {
            canvasMessageManager.RestartLevel();
            //canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }

        if (hit.collider.tag.Equals("MainMenu"))
        {
            canvasMessageManager.MainMenu();
            //canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }

        if (hit.collider.tag.Equals("PlayAgain") || hit.collider.tag.Equals("RestartGame"))
        {
            canvasMessageManager.PlayAgain();
            //canvasMessageManager.CloseMenu();
            canvasMessageManager.CloseHud();
        }
    }
}