using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_l2 : MonoBehaviour
{
    private GameObject GM;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!GM.GetComponent<FadeScene>().l2)
            {
                StartCoroutine(_KillL2Cam());
            }
        }
    }

    IEnumerator _KillL2Cam()
    {
        yield return new WaitForSeconds(26.0f);
        GameObject l2camGO = GameObject.Find("L2Cam");
        Camera l2cam = l2camGO.GetComponent<Camera>();
        l2cam.enabled = false;
    }
}
