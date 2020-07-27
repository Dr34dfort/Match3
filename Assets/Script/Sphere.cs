using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sphere : MonoBehaviour
{
    public int color;
    //public bool started;
    public bool grounded;
    public Rigidbody rb;
    public float vel;
    public int coordX;
    public int coordY;
    public GameObject gp;
    public float StartX;
    public float StartY;
    public float EndX;
    public float EndY;
    public GameObject switcher;
    public Bullet bullet;
    public int state;
    void Start()
    {
        vel = 0;
        //started = false;
        grounded = false;
        coordX = 0;
        coordY = 0;
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
        Material mat = Resources.Load("color" + color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        coordX = Mathf.RoundToInt(this.transform.position.x);
        coordY = Mathf.RoundToInt(this.transform.position.y);
        vel = Mathf.Round(rb.velocity.y);
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
        //gp.GetComponent<Gameplay>().state = 1;
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
