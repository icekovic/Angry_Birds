using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float launchForce;

    private bool inPlay;
    private Rigidbody rigidBody;
    private int lives = 5;
    private CanvasMessageManager canvasMessageManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        canvasMessageManager = FindObjectOfType<CanvasMessageManager>();
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

        if(!inPlay && lives == 0 && GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            canvasMessageManager.ShowGameOverMessage();
            canvasMessageManager.CloseGameCompletedMessage();
            canvasMessageManager.CloseLevelCompletedMessage();
            Debug.Log(inPlay);
            Destroy(this.gameObject);
        }

        if (GameObject.FindWithTag("Enemy") == null  && lives > 0 && 
            SceneManager.GetActiveScene().name.Equals("FirstLevel"))
        {
            canvasMessageManager.ShowLevelCompletedMessage();
            canvasMessageManager.CloseGameCompletedMessage();
            canvasMessageManager.CloseGameOverMessage();
        }

        if (GameObject.FindWithTag("Enemy") == null && lives > 0 &&
            SceneManager.GetActiveScene().name.Equals("Secondlevel"))
        {
            canvasMessageManager.ShowGameCompletedMessage();
            canvasMessageManager.CloseGameOverMessage();
            canvasMessageManager.CloseLevelCompletedMessage();
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
