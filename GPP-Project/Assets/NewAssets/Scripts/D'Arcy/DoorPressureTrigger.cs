using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressureTrigger : MonoBehaviour
{
    public GameObject _controller;

    private void OnTriggerStay(Collider other)
    {
        _controller.GetComponent<DoorControllerPressure>()._door_open = true;
        if(other.tag != "Player")
        {
            _controller.GetComponent<DoorControllerPressure>()._switch_activator = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _controller.GetComponent<DoorControllerPressure>()._door_open = false;
        if (other.tag != "Player")
        {
            _controller.GetComponent<DoorControllerPressure>()._switch_activator = other.gameObject;
        }
    }
}
