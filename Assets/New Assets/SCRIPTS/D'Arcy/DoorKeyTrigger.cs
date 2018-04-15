using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTrigger : MonoBehaviour
{
    public GameObject _controller;
    public GameObject _key;
    private bool has_access = false;

    private void Update()
    {
        if(!_key)
        {
            has_access = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && has_access)
        {
            _controller.GetComponent<DoorControllerKey>()._door_open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && has_access)
        {
            _controller.GetComponent<DoorControllerKey>()._door_open = false;
        }
    }
}
