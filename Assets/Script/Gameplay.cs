using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
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
    public int a;
    public ScoreCount scoreCount;
    public Turns turnCount;
    public int b;
    public List<Sphere> help;
    public GameObject aim1;
    public GameObject aim2;
    public GameObject aim3;
    public GameObject text;
    public GameObject text2;
    public int timer;
    public int state;
    public int turn;
    void Start()
    {
        text2.GetComponent<Text>().enabled = true;
        text.GetComponent<Text>().text = "";
        turn = 360;
        timer = 0;
        aim1.GetComponent<MeshRenderer>().enabled = false;
        aim2.GetComponent<MeshRenderer>().enabled = false;
        aim3.GetComponent<MeshRenderer>().enabled = false;
        spheres = new Sphere[10, 10];
        scoreHelper = 0;
        score = 0;
        started = false;
        help = new List<Sphere>();
        pillars = new List<CheckPillar>();
        //Здесь создаются столбы. Они жизненно необходимы для работы игры, благодаря ним создается матрица сфер и подсчитываются очки.
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
        StartCoroutine(Timer());
        StartCoroutine(Tutorial());
    }
    void Update()
    {
        //Условия победы и поражения
        if (score >= 4000 && timer>0)
        {
            text.GetComponent<Text>().text = "Поздравляю! Вы победили!";
            started = false;
        }
        else if (score < 4000 && turn <= 0)
        {
            text.GetComponent<Text>().text = "Вы проиграли.";
            started = false;
        }
        turnCount.turn = turn;
        
        for (int i=0;i<10;i++)
        {
            pillars[i].started = started;
        }
        if (started == true)
        {
            a = 1;
            scoreHelper = 0;
            for (int i = 0; i < 10; i++)
            {
                scoreHelper += pillars[i].score;
                for (int j = 0; j < pillars[i].spheres.Count; j++)
                {
                    for (int k=0;k < pillars[i].spheres.Count;k++)
                    {
                        if (pillars[i].spheres[k].coordY==j)
                        {
                            spheres[i, j] = pillars[i].spheres[k];
                        }
                    }
                }
            }
            //state - это состояния. 0 - готов, 1 - ожидание, 2 - обработка подсказки
            if (state < 2)
            {
                //эта функция отвечает за взаимодействия сфер. Их уничтожение.
                SphereChecker();
            }
            if (state == 0)
            {
                score = scoreHelper;
                StartCoroutine(WaitBeforeHelp());
            }
            scoreCount.score = score;
            for (int i = 0; i < 10; i++)
            {
                pillars[i].state = state;
                a *= pillars[i].a;
            }
            if (a == 1 && state == 1)
            {
                StartCoroutine(Wait());
            }
            if (a == 0)
            {
                state = 1;
                timer = 0;
                aim1.GetComponent<MeshRenderer>().enabled = false;
                aim2.GetComponent<MeshRenderer>().enabled = false;
                aim3.GetComponent<MeshRenderer>().enabled = false;
            }
            if (state == 2)
            {
                //Вычисление положения сфер, которые могут образовать линию.
                CheckAndRespawn();
            }
            //Вывод подсказки по истечении времени
            if (timer >= 5)
            {
                aim1.transform.position = new Vector3(help[0].transform.position.x, help[0].transform.position.y, help[0].transform.position.z - 1);
                aim1.GetComponent<MeshRenderer>().enabled = true;
                aim2.transform.position = new Vector3(help[1].transform.position.x, help[1].transform.position.y, help[1].transform.position.z - 1);
                aim2.GetComponent<MeshRenderer>().enabled = true;
                aim3.transform.position = new Vector3(help[2].transform.position.x, help[2].transform.position.y, help[2].transform.position.z - 1);
                aim3.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        state = 2;
    }
    IEnumerator Starter()
    {
        //Создание 100 сфер в самом начале
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
        state = 2;
    }
    public int SphereChecker()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < pillars[x].spheres.Count; y++)
            {
                for (int i = pillars[x].spheres.Count; i < 10; i++)
                {
                    if (i < 10) spheres[x, i] = null;
                }
                if ((x >= 1) && (x <= 8))
                {
                    if (spheres[x, y] != null && spheres[x+1, y] != null && spheres[x-1, y] != null)
                    {
                        if (spheres[x, y].color == spheres[x + 1, y].color && spheres[x, y].color == spheres[x - 1, y].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x + 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 1, y].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructHorizontal();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
                if ((y >= 1) && (y <= 8))
                {
                    if (spheres[x, y] != null && spheres[x, y + 1] != null && spheres[x, y - 1] != null)
                    {
                        if (spheres[x, y].color == spheres[x, y + 1].color && spheres[x, y].color == spheres[x, y - 1].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 1].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructVertical();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
                if (x == 0)
                {
                    if (spheres[x, y] != null && spheres[x + 1, y] != null && spheres[x + 2, y] != null)
                    {
                        if (spheres[x, y].color == spheres[x + 1, y].color && spheres[x, y].color == spheres[x + 2, y].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x + 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x + 2, y].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructHorizontal();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
                if (x == 9)
                {
                    if (spheres[x, y] != null && spheres[x - 1, y] != null && spheres[x - 2, y] != null)
                    {
                        if (spheres[x, y].color == spheres[x - 1, y].color && spheres[x, y].color == spheres[x - 2, y].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 1, y].vel) <= 0.1f && Mathf.Abs(spheres[x - 2, y].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructHorizontal();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
                if (y == 0)
                {
                    if (spheres[x, y] != null && spheres[x, y + 1] != null && spheres[x, y + 1] != null)
                    {
                        if (spheres[x, y].color == spheres[x, y + 1].color && spheres[x, y].color == spheres[x, y + 2].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y + 2].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructVertical();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
                if (y == 9)
                {
                    if (spheres[x, y] != null && spheres[x, y - 1] != null && spheres[x, y - 2] != null)
                    {
                        if (spheres[x, y].color == spheres[x, y - 1].color && spheres[x, y].color == spheres[x, y - 2].color)
                        {
                            if (Mathf.Abs(spheres[x, y].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 1].vel) <= 0.1f && Mathf.Abs(spheres[x, y - 2].vel) <= 0.1f)
                            {
                                spheres[x, y].DestructVertical();
                                for (int i = pillars[x].spheres.Count; i < 10; i++)
                                {
                                    if (i < 10) spheres[x, i] = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        return 0;
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
        if (b == 1) NewSpawn();
        state = 0;
        StartCoroutine(WaitBeforeHelp());
    }
    IEnumerator WaitBeforeHelp()
    {
        yield return new WaitForSeconds(1);
        timer++;
    }
    private void NewSpawn()
    {
        //Эта функция на случай, если следующий ход невозможен. Еще не разу не было такой ситуации.
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < pillars[i].spheres.Count; j++)
            {
                Destroy(spheres[i, j].gameObject);
            }
        }
        started = false;
        Spawn();
        StartCoroutine(Starter());
    }
    IEnumerator Timer()
    {
        while (turn > 0)
        {
            yield return new WaitForSeconds(1);
            turn--;
        }
    }
    IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(5);
        text2.GetComponent<Text>().enabled = false;
    }
}
