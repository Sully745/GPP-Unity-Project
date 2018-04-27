using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTrigger : MonoBehaviour
{
    public GameObject _controller;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && player.GetComponent<PlayerController>().interact)
        {
            _controller.GetComponent<DoorControllerButton>()._door_open = true;
        }
    }
}
