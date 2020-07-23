using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    public Sphere sphere;
    public Gameplay gp;
    public Bullet bullet;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sphere.chainedSpheres2.RemoveAll(x => x == null);
        sphere.chainCountVertical = sphere.chainedSpheres2.Count;
        if (sphere.chainCountVertical >= 2)
        {
            sphere.Destruct();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f)
        {
            sphere.chainedSpheres2.Add(other.GetComponent<Sphere>());
            other.GetComponent<Sphere>().Checker2();
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f && sphere.chainCountHorizontal >= 2)
        {
            other.GetComponent<Sphere>().Checker2();
        }
    }
}