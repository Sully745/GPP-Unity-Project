using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTimedTrigger : MonoBehaviour
{
    public GameObject _controller;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<PlayerController>().interact)
        {
            _controller.GetComponent<DoorControllerTimed>().StopAllCoroutines();
            _controller.GetComponent<DoorControllerTimed>()._door_open = true;
        }
    }
}
