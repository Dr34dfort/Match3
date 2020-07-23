using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sphere : MonoBehaviour
{
    public bool chained;
    public bool chained2;
    public List<Sphere> chainedSpheres;
    public List<Sphere> chainedSpheres2;
    public int color;
    public bool started;
    public int chainCountHorizontal;
    public int chainCountVertical;
    public bool grounded;
    public Rigidbody rb;
    public float vel;
    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
    public bool gotChecked;
    public int coordX;
    public int coordY;
    public Gameplay gp;
    public float StartX;
    public float StartY;
    public float EndX;
    public float EndY;
    public GameObject switcher;
    public Bullet bullet;
    void Start()
    {
        vel = 0;
        chained = false;
        chained2 = false;
        started = false;
        grounded = false;
        gotChecked = false;
        coordX = 0;
        coordY = 0;
        chainCountHorizontal = 0;
        chainCountVertical = 0;
        StartX=0;
        StartY=0;
        EndX=0;
        EndY=0;
        switcher.SetActive(false);
        ColorChange();
        started = true;
    }

    // Update is called once per frame
    void Update()
    { 
        Material mat = Resources.Load("color" + color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        coordX = Mathf.RoundToInt(this.transform.position.x);
        coordY = Mathf.RoundToInt(this.transform.position.y);
        vel = Mathf.Round(rb.velocity.y);
        if (Mathf.Abs(vel) > 0.1f)
        {
            left.SetActive(false);
            right.SetActive(false);
            this.GetComponent<Sphere>().chainedSpheres.Clear();
            this.GetComponent<Sphere>().chainedSpheres.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountHorizontal = 0;
            up.SetActive(false);
            down.SetActive(false);
            this.GetComponent<Sphere>().chainedSpheres2.Clear();
            this.GetComponent<Sphere>().chainedSpheres2.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountVertical = 0;
        }
        else
        {
            left.SetActive(true);
            right.SetActive(true);
            up.SetActive(true);
            down.SetActive(true);
        }
    }
    public void Checker()
    {
        right.SetActive(false);
        left.SetActive(false);
        this.GetComponent<Sphere>().chainedSpheres.Clear();
        this.GetComponent<Sphere>().chainedSpheres.RemoveAll(x => x == null);
        this.GetComponent<Sphere>().chainCountHorizontal = 0;
        Wait();
    }
    IEnumerator Wait()
    {
        right.SetActive(true);
        left.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }
    public void Checker2()
    {
        up.SetActive(false);
        down.SetActive(false);
        this.GetComponent<Sphere>().chainedSpheres2.Clear();
        this.GetComponent<Sphere>().chainedSpheres2.RemoveAll(x => x == null);
        this.GetComponent<Sphere>().chainCountVertical = 0;
        Wait2();
    }
    IEnumerator Wait2()
    {
        up.SetActive(true);
        down.SetActive(true);
        yield return new WaitForSeconds(0.1f);
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
        if (StartX != EndX || StartY != EndY) Switch();
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
    public void Destruct()
    {
        if (chainCountHorizontal >= 2)
        {

            Bullet bulletR = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            bulletR.color = color;
            bulletR.X = 10;
            Bullet bulletL = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            bulletL.color = color;
            bulletL.X = -10;
        }
        if (chainCountVertical >= 2)
        {
            Bullet bulletU = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            bulletU.color = color;
            bulletU.Y = 10;
            Bullet bulletD = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            bulletD.color = color;
            bulletD.Y = -10;
        }
    }
}
