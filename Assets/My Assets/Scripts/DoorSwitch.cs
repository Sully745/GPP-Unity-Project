using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    public GameObject player;
    public GameObject player_switch_pos;
    public GameObject door;
    public GameObject end_position;

    public Camera player_cam;
    public Camera door_cam;

    private bool button_move = false;
    private bool player_stay = false;
    private float speed = 0.1f;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
        door_cam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (button_move == true)
        {
           transform.position = Vector3.MoveTowards(transform.position, end_position.transform.position, speed);
        }

        if (transform.position == end_position.transform.position)
        {
            door.GetComponent<DoorAnimation>().open_door = true;
        }

        if (player_stay == true)
        {
            player.transform.position = player_switch_pos.transform.position;
            player.transform.rotation = player_switch_pos.transform.rotation;
        }

    }
    
    void OnTriggerStay(Collider collision)
    {
        if (!triggered && !collision.isTrigger)
        {
            if (collision.GetComponent<Animator>().GetAnimatorTransitionInfo(0).IsName("AnyState -> Attack Punch L"))
            {
                if (collision.transform.rotation.eulerAngles.y >= 45 &&
                collision.transform.rotation.eulerAngles.y <= 135)
                {
                    door_cam.enabled = true;
                    player_cam.enabled = false;

                    collision.transform.position = player_switch_pos.transform.position;
                    collision.transform.rotation = player_switch_pos.transform.rotation;

                    player.GetComponent<PlayerController>().enabled = false;
                    player_stay = true;


                    Invoke("setButtonMove", 0.35f);
                    triggered = true;
                }
            }
        }
                   
    }

    void setButtonMove()
    {
        button_move = true;
    }

    public void switchCamera()
    {
        player_stay = false;
        player.GetComponent<PlayerController>().enabled = true;
        player_cam.enabled = true;
        door_cam.enabled = false;
    }
}
