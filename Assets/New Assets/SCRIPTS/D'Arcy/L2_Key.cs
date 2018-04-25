using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_Key : MonoBehaviour
{
    public GameObject GM;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            GM.GetComponent<FadeScene>().level2_completed = true;
        }
    }
}
