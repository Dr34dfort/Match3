using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.WSA.Input;

public class Gameplay : MonoBehaviour
{
    public Sphere sphere;
    private System.Random rnd;
    public int[,] colorMap;
    public CheckPillar checkPillar;
    public List<CheckPillar> pillars;
    public bool started;
    public int score;
    private int scoreHelper;
    public Sphere[,] spheres;
    public int waiting;
    public int a;
    public ScoreCount scoreCount;
    public int b;
    public List<Sphere> help;
    void Start()
    {
        waiting = 0;
        spheres = new Sphere[10, 10];
        scoreHelper = 0;
        score = 0;
        started = false;
        help = new List<Sphere>();
        pillars = new List<CheckPillar>();
        for (int i = 0; i < 10; i++)
        {
            CheckPillar pillar = Instantiate(checkPillar, new Vector3(2.5f+i, 4.5f, 0), Quaternion.identity) as CheckPillar;
            pillars.Add(pillar);
            pillars[i].row = i;
        }
        rnd = new System.Random();
        colorMap = new int[10, 10];
        Spawn();
        StartCoroutine(Starter());
    }
    void Update()
    {
        a = 1;
        scoreCount.score = score;
        if (Input.GetKey(KeyCode.Space))
        {
            CheckAndRespawn();
        }
        for (int i = 0; i < 10; i++)
        {
            scoreHelper += pillars[i].score;
            for (int j = 0; j < 10; j++)
            {
                spheres[i, j] = pillars[i].spheres[j];
            }
        }
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                SphereChecker(i, j);
            }
        }
        if (started == true)
        {
            score = scoreHelper;
            scoreHelper = 0;
        }
        else
        {
            waiting = 0;
        }
        for (int i = 0; i < 10; i++)
        {
            pillars[i].started = started;
            a *= pillars[i].a;
        }
        if (a == 0) waiting = 0;
    }
    IEnumerator Starter()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Sphere basis = Instantiate(sphere, new Vector3(2.5f+i, j+11, 0), Quaternion.identity) as Sphere;
                basis.color = colorMap[i,j];
            }
            
        }
        yield return new WaitForSeconds(2f);
        started = true;
    }
    public void SphereChecker(int x, int y)
    {
        if ((x >= 1) && (x <= 8))
        {
            if (spheres[x, y].color == spheres[x + 1, y].color && spheres[x, y].color == spheres[x - 1, y].color)
            {
                if (Mathf.Abs(spheres[x,y].vel)<=0.1f && Mathf.Abs(spheres[x + 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 1, y].vel) <= 0.1f) spheres[x, y].DestructHorizontal();
            }
        }
        if ((y >= 1) && (y <= 8))
        {
            if (spheres[x, y].color == spheres[x, y + 1].color && spheres[x, y].color == spheres[x, y - 1].color)
            {
                if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 1].vel) <= 0.1f) spheres[x, y].DestructVertical();
            }
        }
        if (x == 0)
        {
            if (spheres[x, y].color == spheres[x + 1, y].color && spheres[x, y].color == spheres[x + 2, y].color)
            {
                if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x + 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x + 2, y].vel) <= 0.1f) spheres[x, y].DestructHorizontal();
            }
        }
        if (x == 9)
        {
            if (spheres[x, y].color == spheres[x - 1, y].color && spheres[x, y].color == spheres[x - 2, y].color)
            {
                if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 2, y].vel) <= 0.1f) spheres[x, y].DestructHorizontal();
            }
        }
        if (y == 0)
        {
            if (spheres[x, y].color == spheres[x, y + 1].color && spheres[x, y].color == spheres[x, y + 2].color)
            {
                if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 2].vel) <= 0.1f) spheres[x, y].DestructVertical();
            }
        }
        if (y == 9)
        {
            if (spheres[x, y].color == spheres[x, y - 1].color && spheres[x, y].color == spheres[x, y - 2].color)
            {
                if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 2].vel) <= 0.1f) spheres[x, y].DestructVertical();
            }
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                if (j >= 2 && i<2)
                {
                    if (colorMap[i, j] == colorMap[i, j - 1] && colorMap[i, j] == colorMap[i, j - 2])
                    {
                        while (colorMap[i, j] == colorMap[i, j - 1])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                }
                else if (i >= 2 && j<2)
                {
                    if (colorMap[i, j] == colorMap[i - 1, j] && colorMap[i, j] == colorMap[i - 2, j])
                    {
                        while (colorMap[i, j] == colorMap[i - 1, j])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                }
                else if (i>=2 && j>=2)
                {
                    if (colorMap[i, j-1] == colorMap[i, j - 2] && colorMap[i-1, j] == colorMap[i-2, j])
                    {
                        while (colorMap[i, j] == colorMap[i, j - 1] || colorMap[i,j] == colorMap[i-1,j])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                    else if (colorMap[i, j] == colorMap[i, j - 1] && colorMap[i, j] == colorMap[i, j - 2])
                    {
                        while (colorMap[i, j] == colorMap[i, j - 1])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                    else if(colorMap[i, j] == colorMap[i - 1, j] && colorMap[i, j] == colorMap[i - 2, j])
                    {
                        while (colorMap[i, j] == colorMap[i - 1, j])
                        {
                            colorMap[i, j] = (int)Mathf.Round(Random.Range(1, 6));
                        }
                    }
                }
            }
        }
    }
    public void CheckAndRespawn()
    {
        b = 1;
        for (int i=0;i<10;i++)
        {
            for (int j=0;j<10;j++)
            {
                if (i>=2 && j>=2 && i<=7 && j <= 7)
                {
                    //проверка вертикальных и горизонтальных линий
                    if (j >= 3)
                    {
                        if (spheres[i, j].color == spheres[i, j - 3].color && spheres[i, j].color == spheres[i, j - 2].color)
                        {
                            b *= 0;
                            help.Clear();
                            help.RemoveAll(x => x == null);
                            help.Add(spheres[i, j]);
                            help.Add(spheres[i, j - 3]);
                            help.Add(spheres[i, j - 2]);
                        }
                    }
                    if (j <= 6)
                    {
                        if (spheres[i, j].color == spheres[i, j + 3].color && spheres[i, j].color == spheres[i, j + 2].color)
                        {
                            b *= 0;
                            help.Clear();
                            help.RemoveAll(x => x == null);
                            help.Add(spheres[i, j]);
                            help.Add(spheres[i, j + 3]);
                            help.Add(spheres[i, j + 2]);
                        }
                    }
                    if (i >= 3)
                    {
                        if (spheres[i, j].color == spheres[i - 3, j].color && spheres[i, j].color == spheres[i - 2, j].color)
                        {
                            b *= 0;
                            help.Clear();
                            help.RemoveAll(x => x == null);
                            help.Add(spheres[i, j]);
                            help.Add(spheres[i - 3, j]);
                            help.Add(spheres[i - 2, j]);
                        }
                    }
                    if (i <= 6)
                    {
                        if (spheres[i, j].color == spheres[i + 3, j].color && spheres[i, j].color == spheres[i + 2, j].color)
                        {
                            b *= 0;
                            help.Clear();
                            help.RemoveAll(x => x == null);
                            help.Add(spheres[i, j]);
                            help.Add(spheres[i + 3, j]);
                            help.Add(spheres[i + 2, j]);
                        }
                    }
                    //первые четыре проверки
                    if (spheres[i,j].color == spheres[i+1,j-1].color && spheres[i, j].color == spheres[i+1, j+1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j - 1]);
                        help.Add(spheres[i + 1, j + 1]);
                    }
                    if (spheres[i, j].color == spheres[i + 1, j + 1].color && spheres[i, j].color == spheres[i - 1, j + 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j + 1]);
                        help.Add(spheres[i - 1, j + 1]);
                    }
                    if (spheres[i, j].color == spheres[i - 1, j + 1].color && spheres[i, j].color == spheres[i - 1, j - 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j + 1]);
                        help.Add(spheres[i - 1, j - 1]);
                    }
                    if (spheres[i, j].color == spheres[i - 1, j - 1].color && spheres[i, j].color == spheres[i + 1, j - 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j - 1]);
                        help.Add(spheres[i + 1, j - 1]);
                    }
                    //лево-верхняя ветка
                    if (spheres[i, j].color == spheres[i + 1, j - 1].color && spheres[i, j].color == spheres[i + 1, j - 2].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j - 1]);
                        help.Add(spheres[i + 1, j - 2]);
                    }
                    if (spheres[i, j].color == spheres[i + 1, j - 1].color && spheres[i, j].color == spheres[i + 2, j - 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j - 1]);
                        help.Add(spheres[i + 2, j - 1]);
                    }
                    //право-верхняя ветка
                    if (spheres[i, j].color == spheres[i + 1, j + 1].color && spheres[i, j].color == spheres[i + 1, j + 2].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j + 1]);
                        help.Add(spheres[i + 1, j + 2]);
                    }
                    if (spheres[i, j].color == spheres[i + 1, j + 1].color && spheres[i, j].color == spheres[i + 2, j + 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i + 1, j + 1]);
                        help.Add(spheres[i + 2, j + 1]);
                    }
                    //право-нижняя ветка
                    if (spheres[i, j].color == spheres[i - 1, j + 1].color && spheres[i, j].color == spheres[i - 1, j + 2].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j + 1]);
                        help.Add(spheres[i - 1, j + 2]);
                    }
                    if (spheres[i, j].color == spheres[i - 1, j + 1].color && spheres[i, j].color == spheres[i - 2, j + 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j + 1]);
                        help.Add(spheres[i - 2, j + 1]);
                    }
                    //лево-нижняя ветка
                    if (spheres[i, j].color == spheres[i - 1, j - 1].color && spheres[i, j].color == spheres[i - 1, j - 2].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j - 1]);
                        help.Add(spheres[i - 1, j - 2]);
                    }
                    if (spheres[i, j].color == spheres[i - 1, j - 1].color && spheres[i, j].color == spheres[i - 2, j - 1].color)
                    {
                        b *= 0;
                        help.Clear();
                        help.RemoveAll(x => x == null);
                        help.Add(spheres[i, j]);
                        help.Add(spheres[i - 1, j - 1]);
                        help.Add(spheres[i - 2, j - 1]);
                    }
                }
            }
        }
        if (b == 0) Debug.Log("Есть возможность сделать ход");
        else if (b == 1) Debug.Log("Нет возможности сделать ход");
    }
}
