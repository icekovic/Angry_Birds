using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Azurirajkoordinate();
    }

    private void Azurirajkoordinate()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Building")
            || collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
