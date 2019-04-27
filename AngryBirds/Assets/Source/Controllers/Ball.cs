using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float launchForce;

    private bool inPlay;
    private Rigidbody rigidBody;

    private int lives = 5;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(!inPlay && lives > 0)
        {
            transform.position = GameObject.Find("SpawnPosition").transform.position;
        }

        if(lives == 0)
        {
            Debug.Log("Game over");
            Destroy(this.gameObject);
        }
    }

    public void SetInPlayTrue()
    {
        inPlay = true;
    }

    public bool GetInPlay()
    {
        return inPlay;
    }

    public Rigidbody GetBallRigidBody()
    {
        return rigidBody;
    }

    public float GetLaunchForce()
    {
        return launchForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Building")
            || collision.gameObject.tag.Equals("Enemy"))
        {
            inPlay = false;
            rigidBody.velocity = Vector3.zero;
        }
    }

    public void ReduceNumberOfLives()
    {
        lives--;
    }

    public int GetNumberOfLives()
    {
        return lives;
    }
}
