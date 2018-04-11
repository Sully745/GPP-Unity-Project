using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRemove : MonoBehaviour
{
    public BezierSpline spline;
    public GameObject Player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            //Player.GetComponent<PlayerController>().freeControl = false;
        }
    }
}
