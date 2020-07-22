using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public Sphere sphere;
    private System.Random rnd;
    public int[,] colorMap;
    private int s;
    void Start()
    {
        rnd = new System.Random();
        s = 0;
        colorMap = new int[16, 10];
        for (int i = 0; i < 16; i++)
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

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Starter()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Sphere basis = Instantiate(sphere, new Vector3(i, j+11+s, 0), Quaternion.identity) as Sphere;
                basis.color = colorMap[i,j];
                s++;
                //basis.color = (int)Mathf.Round(Random.Range(1, 6));
            }
            s = 0;
        }
    }
}
