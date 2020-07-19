using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    public Sphere sphere;
    public bool sameCol;
    public Gameplay gp = new Gameplay();
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
            foreach (Sphere sp in sphere.chainedSpheres)
            {
                sp.chainCountHorizontal = sphere.chainCountHorizontal;
            }
            sphere.chained = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f && sphere.chained == false)
        {
            sameCol = true;
            sphere.chainedSpheres.Add(other.GetComponent<Sphere>());
            other.GetComponent<Sphere>().Checker();
        }
    }
    public void OnTriggerExit(Collider other)
    {

        //sphere.gotChecked = false;
    }
}
