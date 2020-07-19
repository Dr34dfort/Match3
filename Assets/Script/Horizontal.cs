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
            /*foreach (Sphere sp in sphere.chainedSpheres)
            {
                sp.chainCountHorizontal = sphere.chainCountHorizontal;
            }*/
            //gp.HorizontalCheck(sphere.coordX, sphere.coordY, sphere.color);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere" && sphere.color == other.GetComponent<Sphere>().color && Mathf.Abs(other.GetComponent<Sphere>().vel) <= 0.1f && other.GetComponent<Sphere>().gotChecked == false)
        {
            sameCol = true;
            sphere.chainedSpheres.Add(other.GetComponent<Sphere>());
            other.GetComponent<Sphere>().Checker();
        }
        if (other.tag == "Sphere" && sphere.color != other.GetComponent<Sphere>().color)
        {
            sameCol = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        sphere.gotChecked = false;
    }
}
