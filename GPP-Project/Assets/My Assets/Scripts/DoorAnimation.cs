using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {

    public GameObject door_switch;

    public bool open_door = false;

    private float speed = 0.05f;
    public float step;

    public Transform end_position;
    private Transform start_position;

    // Use this for initialization
    void Start () {        
        start_position = transform;
    }
	
	// Update is called once per frame
	void Update () {

        step = speed * Time.deltaTime;

        if (open_door == true)
        {
            transform.position = Vector3.MoveTowards(start_position.position, end_position.position, speed);
        }
        
        if (transform.position == end_position.transform.position)
        {
            door_switch.GetComponent<DoorSwitch>().switchCamera();
        }
    }
}
