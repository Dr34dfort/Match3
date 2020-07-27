using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.WSA.Input;

public class CheckPillar : MonoBehaviour
{
    public Sphere sphere;
    public List<Sphere> spheres;
    public int count;
    public int row;
    public GameObject gp;
    private bool creation;
    public int score;
    public int state;
    public int a;
    public bool started;
    void Start()
    {
        score = 0;
        creation = false;
        spheres = new List<Sphere>();
    }

    // Update is called once per frame
    void Update()
    {
        spheres.RemoveAll(x => x == null);
        count = spheres.Count;
        if (count < 10 && creation == false && state == 1 && started == true)
        {
            Sphere basis = Instantiate(sphere, new Vector3(2.5f + row, 11 + (10-count), 0), Quaternion.identity) as Sphere;
            basis.color = (int)Mathf.Round(Random.Range(1, 6));
            creation = true;
        }
        if (count < 10)
        {
            a = 0;
        }
        else if (count == 10) a = 1;
        for (int i=0;i<count;i++)
        {
            spheres[i].state = state;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            if (state == 1) score += 10;
            creation = false;
            spheres.Add(other.GetComponent<Sphere>());
        }
    }
}
