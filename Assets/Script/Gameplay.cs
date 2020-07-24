using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.WSA.Input;

public class Gameplay : MonoBehaviour
{
    public Sphere sphere;
    private System.Random rnd;
    public int[,] colorMap;
    public CheckPillar checkPillar;
    public List<Sphere> spheres;
    public List<CheckPillar> pillars;
    public bool started;
    void Start()
    {
        started = false;
        spheres = new List<Sphere>();
        pillars = new List<CheckPillar>();
        for (int i = 0; i < 10; i++)
        {
            CheckPillar pillar = Instantiate(checkPillar, new Vector3(2.5f+i, 4.5f, 0), Quaternion.identity) as CheckPillar;
            pillars.Add(pillar);
            pillars[i].row = i;
            pillars[i].gameObject.SetActive(false);
        }
        rnd = new System.Random();
        colorMap = new int[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                if (j >= 2)
                {
                    if (colorMap[i, j] == colorMap[i, j - 1] && colorMap[i, j] == colorMap[i, j - 2])
                    {
                        while (colorMap[i, j] == colorMap[i, j - 1])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                }
                if (i >= 2)
                {
                    if (colorMap[i, j] == colorMap[i - 1, j] && colorMap[i, j] == colorMap[i - 2, j])
                    {
                        while (colorMap[i, j] == colorMap[i - 1, j])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                }
            }
        }
        StartCoroutine(Starter());
    }
    void Update()
    {
        spheres.RemoveAll(x => x == null);
        if (spheres.Count < 100 && started == true)
        {
            for (int i=0; i < 10; i++)
            {
                pillars[i].gameObject.SetActive(true);
            }
        }
        else if (spheres.Count >= 100)
        {
            for (int i = 0; i < 10; i++)
            {
                pillars[i].spheres.Clear();
                pillars[i].spheres.RemoveAll(x => x == null);
                pillars[i].gameObject.SetActive(false);
            }
        }
    }
    IEnumerator Starter()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Sphere basis = Instantiate(sphere, new Vector3(2.5f+i, j+11, 0), Quaternion.identity) as Sphere;
                spheres.Add(basis);
                basis.color = colorMap[i,j];
            }
            
        }
        started = true;
    }
    public void CreateSpheres(int row, int count)
    {
        Sphere basis = Instantiate(sphere, new Vector3(2.5f + row, 11+count, 0), Quaternion.identity) as Sphere;
        spheres.Add(basis);
        basis.color = (int)Mathf.Round(Random.Range(1, 6));
    }
}
