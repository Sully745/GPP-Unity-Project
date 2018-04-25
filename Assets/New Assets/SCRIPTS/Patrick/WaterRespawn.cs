using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject PPos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.transform.position = new Vector3(PPos.transform.position.x, PPos.transform.position.y, PPos.transform.position.z);               
        }
    }
}
