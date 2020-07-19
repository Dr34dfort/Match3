using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public Sphere sphere = new Sphere();
    private System.Random rnd = new System.Random();
    void Start()
    {
        StartCoroutine(Starter());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Starter()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Sphere basis = Instantiate(sphere, new Vector3(j, i+11, 0), Quaternion.identity) as Sphere;
            }
        }
    }
}
