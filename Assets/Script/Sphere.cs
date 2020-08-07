using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sphere : MonoBehaviour
{
    public int color;
    //public bool started;
    public bool grounded;
    public float vel;
    [SerializeField]
    public int PillarY;
    public int CoordY;
    public GameObject gp;
    public float StartX;
    public float StartY;
    public float EndX;
    public float EndY;
    public GameObject switcher;
    public Bullet bullet;
    public int state;
    public bool IsStacked;
    void Start()
    {
        this.IsStacked = false;
        grounded = false;
        StartX=0;
        StartY=0;
        EndX=0;
        EndY=0;
        switcher.SetActive(false);
        ColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        CoordY = (int)Mathf.Round(transform.position.y);
        if (IsStacked == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * vel * 8);
            vel = 1;
        }
        else if (IsStacked == true && transform.position.y > PillarY)
        {
            transform.Translate(Vector3.down * Time.deltaTime * vel * 8);
            vel = 1;
        }
        else if (IsStacked == true && transform.position.y == PillarY)
        {
            vel = 0;
        }
        else if (IsStacked == true && transform.position.y < PillarY)
        {
            transform.position = new Vector3(transform.position.x, PillarY, transform.position.z);
            vel = 0;
        }
        Material mat = Resources.Load("color" + color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
    private void OnMouseDown()
    {
        StartX = Input.mousePosition.x;
        StartY = Input.mousePosition.y;
    }
    private void OnMouseUp()
    {
        EndX = Input.mousePosition.x;
        EndY = Input.mousePosition.y;
        if ((StartX != EndX || StartY != EndY) && state == 0) Switch();
    }
    private void Switch()
    {
        switcher.GetComponent<Switcher>().color = color;
        if (Mathf.Abs(EndX - StartX) > Mathf.Abs(EndY - StartY))
        {
            if (EndX - StartX > 0)
            {
                switcher.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                switcher.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
            }
        }
        else
        {
            if (EndY - StartY > 0)
            {
                switcher.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            }
            else
            {
                switcher.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);
            }
        }
        switcher.SetActive(true);
        StartX = 0;
        StartY = 0;
        EndX = 0;
        EndY = 0;
        StartCoroutine(Wait3());
    }
    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(0.2f);
        switcher.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
    public void ColorChange()
    {
        Material mat = Resources.Load("color" + color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
    public void DestructHorizontal()
    {
        Bullet bulletR = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bulletR.color = color;
        bulletR.X = 10;
        Bullet bulletL = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bulletL.color = color;
        bulletL.X = -10;
    }
    public void DestructVertical()
    {
        Bullet bulletU = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bulletU.color = color;
        bulletU.Y = 10;
        Bullet bulletD = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bulletD.color = color;
        bulletD.Y = -10;
    }
}
