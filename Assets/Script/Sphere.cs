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
    public Vector3 vect;
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
    void Start()
    {
        color = (int)Mathf.Round(Random.Range(1, 6));
        Material mat = Resources.Load("color"+color, typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        coordX = Mathf.RoundToInt(this.transform.position.x);
        coordY = Mathf.RoundToInt(this.transform.position.y);
        if (chained == false)
        {
            vel = Mathf.Round(rb.velocity.y);
            if (Mathf.Abs(vel) > 0.1f)
            {
                left.SetActive(false);
                right.SetActive(false);
                this.GetComponent<Sphere>().chainedSpheres.Clear();
                this.GetComponent<Sphere>().chainedSpheres.RemoveAll(x => x == null);
                this.GetComponent<Sphere>().chainCountHorizontal = 0;
                gp.spheres[coordX, coordY] = null;
            }
            else 
            { 
                left.SetActive(true); 
                right.SetActive(true);
                gp.spheres[coordX, coordY] = this;
            }
        }
        else
        {
            /*foreach (Sphere sp in chainedSpheres)
            {
                sp.chained = true;
            }*/
            gameObject.GetComponent<MeshRenderer>().material = Resources.Load("1", typeof(Material)) as Material;
            Destroy(this.gameObject);
        }
        if (chained2 == false)
        {
            vel = Mathf.Round(rb.velocity.y);
            if (Mathf.Abs(vel) > 0.1f)
            {
                up.SetActive(false);
                down.SetActive(false);
                this.GetComponent<Sphere>().chainedSpheres2.Clear();
                this.GetComponent<Sphere>().chainedSpheres2.RemoveAll(x => x == null);
                this.GetComponent<Sphere>().chainCountVertical = 0;
            }
            else
            {
                up.SetActive(true);
                down.SetActive(true);
            }
        }
        else
        {
            foreach (Sphere sp in chainedSpheres2)
            {
                sp.chained2 = true;
            }
            gameObject.GetComponent<MeshRenderer>().material = Resources.Load("1", typeof(Material)) as Material;
            //Destroy(this.gameObject);

        }
    }
    public void Checker()
    {
        if (chained == false)
        {
            right.SetActive(false);
            left.SetActive(false);
            up.SetActive(false);
            down.SetActive(false);
            this.GetComponent<Sphere>().chainedSpheres.Clear();
            this.GetComponent<Sphere>().chainedSpheres.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountHorizontal = 0;
            this.GetComponent<Sphere>().chainedSpheres2.Clear();
            this.GetComponent<Sphere>().chainedSpheres2.RemoveAll(x => x == null);
            this.GetComponent<Sphere>().chainCountVertical = 0;
            Wait();
        }
    }
    IEnumerator Wait()
    {
        right.SetActive(true);
        left.SetActive(true);
        up.SetActive(true);
        down.SetActive(true);
        gotChecked = true;
        yield return new WaitForSeconds(0.1f);
    }
}
