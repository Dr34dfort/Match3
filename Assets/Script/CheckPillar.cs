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
    private bool creation;
    void Start()
    {
        creation = false;
        spheres = new List<Sphere>();
    }

    // Update is called once per frame
    void Update()
    {
        spheres.RemoveAll(x => x == null);
        count = spheres.Count;
        if (count < 10 && creation == false)
        {
            Sphere basis = Instantiate(sphere, new Vector3(2.5f + row, 11 + (10-count), 0), Quaternion.identity) as Sphere;
            gp.GetComponent<Gameplay>().spheres.Add(basis);
            basis.color = (int)Mathf.Round(Random.Range(1, 6));
            creation = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            creation = false;
            spheres.Add(other.GetComponent<Sphere>());
        }
    }
}
