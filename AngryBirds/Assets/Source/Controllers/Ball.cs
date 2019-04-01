using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //[SerializeField]
    //private float masa;

    [SerializeField]
    private Vector3 vektorBrzine;

    //[SerializeField]
    //private Vector3 rezultantniVektorSile;

    //private List<Vector3> vektoriSila = new List<Vector3>();

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
        //inicijalizacija rezultantnog vektora
        //rezultantniVektorSile = Vector3.zero;

        //foreach (Vector3 vektorSile in vektoriSila)
        //{
        //    rezultantniVektorSile = rezultantniVektorSile + vektorSile;
        //}

        //vektoriSila = new List<Vector3>();

        //izračun trenutne pozicije ovisno o rezultantnoj sili
        //Vector3 vektorAkceleracije = rezultantniVektorSile / masa;
        vektorBrzine += vektorBrzine * Time.deltaTime;
        transform.position += vektorBrzine * Time.deltaTime;
    }

    //public void DodajSilu(Vector3 vektorSile)
    //{
    //    vektoriSila.Add(vektorSile);
    //}

    //public float GetMasa()
    //{
    //    return masa;
    //}

    public Vector3 GetVektorBrzine()
    {
        return vektorBrzine;
    }

    //public Vector3 GetRezultantniVektorSile()
    //{
    //    return rezultantniVektorSile;
    //}

    public void SetVektorBrzine(Vector3 vektorBrzine)
    {
        this.vektorBrzine = vektorBrzine;
    }
}
