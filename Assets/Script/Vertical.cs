using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    public Sphere sphere;
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
            foreach (Sphere sp in sphere.chainedSpheres2)
            {
                sp.chainCountVertical = sphere.chainCountVertical;
            }
        }
        if (sphere.chainCountVertical >= 2)
        {
            sphere.chained2 = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f && other.GetComponent<Sphere>().gotChecked == false)
        {
            sphere.chainedSpheres2.Add(other.GetComponent<Sphere>());
            other.GetComponent<Sphere>().Checker();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        sphere.gotChecked = false;
    }
}
