using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutscene_l1 : MonoBehaviour
{
    private GameObject GM;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if(!GM.GetComponent<FadeScene>().l1)
            {

            }
        }
    }
}
