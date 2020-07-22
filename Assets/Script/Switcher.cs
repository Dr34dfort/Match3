using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public int color;
    public Sphere sphere;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            sphere.color = other.GetComponent<Sphere>().color;
            sphere.ColorChange();
            other.GetComponent<Sphere>().color = color;
            other.GetComponent<Sphere>().ColorChange();
            sphere.Checker();
            sphere.Checker2();
            other.GetComponent<Sphere>().Checker();
            other.GetComponent<Sphere>().Checker2();
            this.transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //this.transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z);
    }
}
