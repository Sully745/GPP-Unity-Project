using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{
    public GameObject pos;
    public GameObject player;

    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            player.transform.position = pos.transform.position;
        }
    }
}
