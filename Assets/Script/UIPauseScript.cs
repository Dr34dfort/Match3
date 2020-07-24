using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseScript : MonoBehaviour
{
    public bool pause;
    public GameObject panel;
    public GameObject Gp;
    public GameObject camera;
    private bool toggle;
    void Start()
    {
        toggle = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = true;
            TimeStop();
        }
        panel.SetActive(pause);
    }
    public void Continue()
    {
        pause = false;
        TimeStop();
    }
    public void TimeStop()
    {
        if (pause == true) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void Music()
    {
        toggle = !toggle;
        camera.GetComponent<StartGame>().AudioNull(toggle);
    }
}
