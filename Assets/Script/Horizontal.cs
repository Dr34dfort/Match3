using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Horizontal : MonoBehaviour
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
        sphere.chainedSpheres.RemoveAll(x => x == null);
        sphere.chainCountHorizontal = sphere.chainedSpheres.Count;
        if (sphere.chainCountHorizontal >= 2)
        {
            sphere.Destruct();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f)
        {
            sphere.chainedSpheres.Add(other.GetComponent<Sphere>());
            other.GetComponent<Sphere>().Checker();
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f && sphere.chainCountVertical>=2)
        {
            other.GetComponent<Sphere>().Checker();
        }
    }
}
