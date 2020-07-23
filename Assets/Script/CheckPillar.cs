using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPillar : MonoBehaviour
{
    public Sphere sphere;
    public List<Sphere> spheres;
    public int count;
    public int row;
    public GameObject gp;
    void Start()
    {
        spheres = new List<Sphere>();
    }

    // Update is called once per frame
    void Update()
    {
        spheres.RemoveAll(x => x == null);
        count = spheres.Count;
        if (count < 10)
        {
            //gp.GetComponent<Gameplay>().CreateSpheres(row, count);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            spheres.Add(other.GetComponent<Sphere>());
        }
    }
}
