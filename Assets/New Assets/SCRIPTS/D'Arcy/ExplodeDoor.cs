using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDoor : MonoBehaviour
{
    public GameObject splinters;
    public GameObject door;
    public GameObject exp;
    private TriggerBridge tb;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            door.SetActive(false);
            splinters.SetActive(true);
            exp.SetActive(true);
            tb.triggered_door = true;
        }
    }
}
