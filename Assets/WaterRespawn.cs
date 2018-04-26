using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    private GameObject Player;
    public GameObject PPos;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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

//Player.transform.position = new Vector3(Player.transform.position.x - (Player.transform.forward* 4).x, Player.transform.position.y* 1000, 
//                Player.transform.position.z - (Player.transform.forward* 4).z);