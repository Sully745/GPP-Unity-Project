using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Camera_Side
{
    LEFT,
    RIGHT
};

public class RotateCamera : MonoBehaviour {

    public Camera player_cam;
    private Vector3 right_offset = new Vector3(0, 6, 8);
    private Vector3 left_offset = new Vector3(0, 6, -8);

    public Camera_Side side_view;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (side_view)
            {
                case Camera_Side.LEFT:
                    player_cam.GetComponent<CameraFollow>().side_view = Camera_Side.LEFT;
                    player_cam.GetComponent<CameraFollow>().offset = left_offset;


                    break;
                case Camera_Side.RIGHT:
                    player_cam.GetComponent<CameraFollow>().side_view = Camera_Side.RIGHT;
                    player_cam.GetComponent<CameraFollow>().offset = right_offset;

                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger == false && other.tag == "Player")
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                player_cam.GetComponent<CameraFollow>().offset = right_offset;
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                player_cam.GetComponent<CameraFollow>().offset = left_offset;
            }
        }
    }

}


