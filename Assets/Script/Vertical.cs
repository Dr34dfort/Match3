using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    public Sphere sphere;
    public Gameplay gp = new Gameplay();
    public Bullet bullet = new Bullet();
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
            Bullet bulletU = Instantiate(bullet, new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z), Quaternion.identity);
            bulletU.color = sphere.color;
            bulletU.Y = 10;
            Bullet bulletD = Instantiate(bullet, new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z), Quaternion.identity);
            bulletD.color = sphere.color;
            bulletD.Y = -10;
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
    public void OnTriggerExit(Collider other)
    {
    }
}