using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int color;
    public int X = 0;
    public int Y = 0;
    private float startX = 0;
    private float startY = 0;
    private float endX = 0;
    private float endY = 0;
    public Sphere sphere = new Sphere();
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(X, Y, 0) * Time.deltaTime);
        endX = transform.position.x;
        endY = transform.position.y;
        if (Mathf.Sqrt(Mathf.Pow((endX - startX),2) + Mathf.Pow((endY - startY),2)) >= 2)
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
        }
    }
}
