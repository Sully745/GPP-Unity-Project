using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollectableTrigger : MonoBehaviour
{
    public GameObject _controller;
    private int amount_of_active_keys = 0;
    private bool has_access = false;

    private void Update()
    {
        if (!has_access)
        {
            amount_of_active_keys = GameObject.FindGameObjectsWithTag("Collectable").Length;
            if(amount_of_active_keys <= 0)
            {
                has_access = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && has_access)
        {
            _controller.GetComponent<DoorControllerCollectable>()._door_open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && has_access)
        {
            _controller.GetComponent<DoorControllerCollectable>()._door_open = false;
        }
    }
}
