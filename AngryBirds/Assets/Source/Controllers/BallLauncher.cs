﻿using System.Collections;
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

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ball newBall = Instantiate(ball) as Ball;
            newBall.transform.parent = GameObject.Find("BallLauncher").transform;
            Vector3 direction = new Vector3(1, Random.Range(0.2f, 0.8f), Random.Range(9,10)).normalized * launchForce;
            newBall.SetDirection(direction);           
        }
    }
}
