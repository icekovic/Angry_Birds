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
        if(!inPlay && canvasMessageManager.GetNumberOfLives() > 0)
        {
            transform.position = GameObject.Find("SpawnPosition").transform.position;
        }

        if(!inPlay && canvasMessageManager.GetNumberOfLives() == 0 && GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            canvasMessageManager.CloseHud();
            canvasMessageManager.ShowGameOverMessage();
            canvasMessageManager.CloseGameCompletedMessage();
            Destroy(this.gameObject);
        }

        if (GameObject.FindWithTag("Enemy") == null  && canvasMessageManager.GetNumberOfLives() > 0 && 
            SceneManager.GetActiveScene().name.Equals("FirstLevel"))
        {
            SceneManager.LoadScene("SecondLevel");
        }

        if (GameObject.FindWithTag("Enemy") == null && canvasMessageManager.GetNumberOfLives() > 0 &&
            SceneManager.GetActiveScene().name.Equals("SecondLevel"))
        {
            SceneManager.LoadScene("ThirdLevel");
        }

        if (GameObject.FindWithTag("Enemy") == null && canvasMessageManager.GetNumberOfLives() > 0 &&
            SceneManager.GetActiveScene().name.Equals("ThirdLevel"))
        {
            canvasMessageManager.ShowGameCompletedMessage();
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
}
