using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTimedTrigger : MonoBehaviour
{
    public GameObject _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _controller.GetComponent<DoorControllerTimed>()._door_open = true;
        }
    }
}
