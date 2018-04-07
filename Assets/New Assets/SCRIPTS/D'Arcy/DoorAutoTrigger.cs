using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutoTrigger : MonoBehaviour
{
    public GameObject _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _controller.GetComponent<DoorControllerAuto>()._door_open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _controller.GetComponent<DoorControllerAuto>()._door_open = false;
        }
    }
}
