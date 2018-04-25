using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public GameObject Player;

 /*   public GameObject checkPoint1;
    public bool checkPoint1Bool = false;
    public bool teleport1 = false;
    public GameObject checkPointCube1; */

	public int index = -1;
	public GameObject[] check_points;


    // Use this for initialization
    void Start()
    {
		Player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
		if (col.tag == ("Player"))
		{
			Debug.Log ("SOME SHIT HAPPENED");
			if (index >= 0) 
			{
				Player.transform.position = check_points [index].transform.position;
				Debug.Log ("SOME SHIT HAPPENED TWICE");
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

