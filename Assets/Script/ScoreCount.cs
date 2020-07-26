using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public int score;
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = Convert.ToString(score);
    }
}
