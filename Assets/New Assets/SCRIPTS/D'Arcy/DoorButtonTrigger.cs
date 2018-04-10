using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTrigger : MonoBehaviour
{
    public GameObject _controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _controller.GetComponent<DoorControllerButton>()._door_open = true;
        }
    }
}
