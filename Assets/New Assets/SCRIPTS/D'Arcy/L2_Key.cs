﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_Key : MonoBehaviour
{
    private GameObject GM;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            GM.GetComponent<FadeScene>().l2 = true;
        }
    }
}
