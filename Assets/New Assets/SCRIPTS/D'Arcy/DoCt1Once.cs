using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoCt1Once : MonoBehaviour
{
    private GameObject GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
