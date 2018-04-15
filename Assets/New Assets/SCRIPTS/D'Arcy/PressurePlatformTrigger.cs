using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatformTrigger : MonoBehaviour
{
    public GameObject _controller;

    private void OnTriggerStay(Collider other)
    {
        _controller.GetComponent<PressurePlatfromController>().move = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _controller.GetComponent<PressurePlatfromController>().move = false;
    }
}
