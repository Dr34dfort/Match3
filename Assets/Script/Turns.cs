using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turns : MonoBehaviour
{
    public int turn;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = Convert.ToString(turn);
    }
}
