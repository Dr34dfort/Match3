using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject GameController;
    void Start()
    {
        AudioListener.volume = 1;
        GameObject gp = Instantiate(GameController, new Vector3(0, -4.5f, 0), Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AudioNull(bool toggle)
    {
        if (toggle == true) AudioListener.volume = 1;
        else AudioListener.volume = 0;
    }
}
