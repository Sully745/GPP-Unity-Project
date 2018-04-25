using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Key : MonoBehaviour
{
    public GameObject GM;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GM.GetComponent<FadeScene>().level1_completed = true;
        }
    }
}

