using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public int color;
    public Sphere sphere;
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
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
            color = sphere.color;
            sphere.Checker();
            sphere.Checker2();
            other.GetComponent<Sphere>().Checker();
            other.GetComponent<Sphere>().Checker2();
            StartCoroutine(Wait(other));
        }
    }
    IEnumerator Wait(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        if (other != null)
        {
            sphere.color = other.GetComponent<Sphere>().color;
            sphere.ColorChange();
            other.GetComponent<Sphere>().color = color;
            other.GetComponent<Sphere>().ColorChange();
        }
        this.transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z);
        this.gameObject.SetActive(false);
    }
}
