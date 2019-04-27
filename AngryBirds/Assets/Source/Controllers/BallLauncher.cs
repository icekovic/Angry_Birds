using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private Ball ball;

    [SerializeField]
    private float launchForce;

    void Start()
    {
        //InstantiateBall();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //InstantiateBall();
            //    Ball newBall = Instantiate(ball) as Ball;
            //    newBall.transform.parent = GameObject.Find("Canon").transform;
            //    Vector3 direction = new Vector3(1, Random.Range(0.2f, 0.8f), Random.Range(9,10)).normalized * launchForce;
            //    newBall.SetDirection(direction);           
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    InstantiateAndLaunchBall(ball);
        //    Debug.Log(placementIndicatorCoordinates);
        //}
    }

    //private void InstantiateBall()
    //{
    //    Ball newBall = Instantiate(ball) as Ball;
    //    //newBall.transform.parent = GameObject.Find("Canon").transform;
    //    newBall.transform.position = GameObject.Find("SpawnPosition").transform.position;
    //}
}
