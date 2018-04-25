using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDoor : MonoBehaviour
{
    public GameObject splinters;
    public GameObject door;
    public GameObject explosion_pos;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            door.SetActive(false);
            splinters.SetActive(true);
        }
    }
}
