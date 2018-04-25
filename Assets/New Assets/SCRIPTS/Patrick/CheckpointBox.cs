using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBox : MonoBehaviour
{
    public GameObject plane;
	public int check_point_value = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
			plane.GetComponent<Checkpoint> ().index = check_point_value;
        }

    }
}
