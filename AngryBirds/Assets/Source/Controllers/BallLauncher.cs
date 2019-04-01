﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private float launchForce;

    //[SerializeField]
    //private float maksimalnaPocetnaBrzina;

    [SerializeField]
    private Ball ball;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ball newBall = Instantiate(ball) as Ball;
            newBall.transform.parent = GameObject.Find("BallLauncher").transform;
            Vector3 brzinaLansiranja = new Vector3(1, Random.Range(0.2f, 0.8f), Random.Range(-0.2f, 0.3f)).normalized * launchForce;
            newBall.SetVektorBrzine(brzinaLansiranja);

            //if(noviProjektil != null && projektil != null)
            //{
            //    Destroy(noviProjektil, 1);
            //}            
        }
    }
}
