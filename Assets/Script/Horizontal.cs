using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Horizontal : MonoBehaviour
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
        sphere.chainedSpheres.RemoveAll(x => x == null);
        sphere.chainCountHorizontal = sphere.chainedSpheres.Count;
        if (sphere.chainCountHorizontal >= 2)
        {
            Bullet bulletR = Instantiate(bullet, new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z), Quaternion.identity);
            bulletR.color = sphere.color;
            bulletR.X = 10;
            Bullet bulletL = Instantiate(bullet, new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z), Quaternion.identity);
            bulletL.color = sphere.color;
            bulletL.X = -10;
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
    public void OnTriggerExit(Collider other)
    {
    }
}
