using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int index = -1;
	public GameObject[] check_points;

    void OnTriggerEnter(Collider col)
    {
		if (col.tag == ("Player"))
		{
			if (index >= 0) 
			{
				col.transform.position = check_points [index].transform.position;
			}
		}


       /* if (collision.tag == Player.tag
            && checkPoint1Bool)
        {
            //move to check point
            teleport1 = true;
        }
        else
        {
            teleport1 = false;
        }

        if (teleport1 == true)
        {
            Player.transform.position = checkPointCube1.transform.position;
        } */
    }
}

