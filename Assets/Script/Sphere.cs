using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sphere : MonoBehaviour
{
    private Random rnd = new Random();
    public bool chained = false;
    public bool chained2 = false;
    public List<Sphere> chainedSpheres = new List<Sphere>();
    public List<Sphere> chainedSpheres2 = new List<Sphere>();
    public int color;
    public bool started = false;
    public int chainCountHorizontal = 0;
    public int chainCountVertical = 0;
    private int maxChainHorizontal = 0;
    private int maxChainVertical = 0;
    public bool grounded = false;
    public Rigidbody rb = new Rigidbody();
    public float vel = 0;
    public GameObject left = new GameObject();
    public GameObject right = new GameObject();
    public GameObject up = new GameObject();
    public GameObject down = new GameObject();
    public bool gotChecked = false;
    public int coordX = 0;
    public int coordY = 0;
    public Gameplay gp = new Gameplay();
    public float StartX=0;
    public float StartY=0;
    public float EndX=0;
    public float EndY=0;
    public GameObject switcher = new GameObject();
    void Start()
    {
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
        /*if (chained == false)
        {
            right.SetActive(false);
            left.SetActive(false);
            this.GetComponent<Sphere>().chainedSpheres.Clear();
            this.GetComponent<Sphere>().chainedSpheres.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountHorizontal = 0;
            Wait();
        }*/
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
        /*if (chained2 == false)
        {
            up.SetActive(false);
            down.SetActive(false);
            this.GetComponent<Sphere>().chainedSpheres2.Clear();
            this.GetComponent<Sphere>().chainedSpheres2.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountVertical = 0;
            Wait2();
        }*/
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
    public void OnTriggerEnter(Collider other)
    {
        /*if (other.tag=="Bullet" && (other.GetComponent<Bullet>().color == color))
        {
            Destroy(this.gameObject);
        }*/
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
    }
    public void ColorChange()
    {
        Material mat = Resources.Load("color" + color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
