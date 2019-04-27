using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    private bool inPlay;
    private Rigidbody rigidBody;
    private RaycastHit hit;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(!inPlay)
        {
            transform.position = GameObject.Find("SpawnPosition").transform.position;
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    inPlay = true;
        //    LaunchBall();
        //}
    }

    private void FixedUpdate()
    {
        AzurirajKoordinate();
    }

    private void LaunchBall(RaycastHit hit)
    {
        rigidBody.velocity = (hit.point - rigidBody.transform.position).normalized * 30;
        AzurirajKoordinate();
    }

    private void AzurirajKoordinate()
    {
        direction += direction * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetInPlayTrue()
    {
        inPlay = true;
    }

    public Rigidbody GetBallRigidBody()
    {
        return rigidBody;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Building")
            || collision.gameObject.tag.Equals("Enemy"))
        {
            inPlay = false;
            rigidBody.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }
}
