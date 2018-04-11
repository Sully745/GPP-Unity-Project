using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    public GameObject Player;
    public GameObject Platform;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Player.transform.parent = Platform.transform;
    }
    private void OnCollisionExit(Collision collision)
    {
        Player.transform.parent = null;
    }

}

