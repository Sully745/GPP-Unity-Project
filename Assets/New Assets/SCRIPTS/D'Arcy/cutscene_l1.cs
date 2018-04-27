using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_l1 : MonoBehaviour
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
            if(!GM.GetComponent<FadeScene>().l2)
            { 
                StartCoroutine(_KillL1Cam());
            }
        }
    }

    IEnumerator _KillL1Cam()
    {
        yield return new WaitForSeconds(30.0f);
        GameObject l1camGO = GameObject.Find("L1Cam");
        Camera l1cam = l1camGO.GetComponent<Camera>();
        l1cam.enabled = false;
    }
}