using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int color;
    public int X;
    public int Y;
    private float startX;
    private float startY;
    private float endX;
    private float endY;
    public Sphere sphere;
    public GameObject explosion;
    public GameObject gp;
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }
    void Update()
    {
        transform.Translate(new Vector3(X, Y, 0) * Time.deltaTime);
        endX = transform.position.x;
        endY = transform.position.y;
        if (Mathf.Sqrt(Mathf.Pow((endX - startX),2) + Mathf.Pow((endY - startY),2)) >= 1.4)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            startX = transform.position.x;
            startY = transform.position.y;
            if (other.GetComponent<Sphere>().color != color)
            {
                Destroy(this.gameObject);
            }
            else if (other.GetComponent<Sphere>().color == color)
            {

                GameObject expl = Instantiate(explosion, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity) as GameObject;
                Destroy(other.gameObject);
            }
        }
    }
}
