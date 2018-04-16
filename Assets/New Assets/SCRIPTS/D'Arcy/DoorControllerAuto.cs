using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerAuto : MonoBehaviour
{
    public GameObject _door;
    private Vector3 _door_origin_position;
    public GameObject _door_open_pos;
    public bool _door_open = false;
    public GameObject _button;
    public Material _mat_active;
    private Material _mat_inactive;

	// Use this for initialization
	void Start ()
    {
        _door_origin_position = _door.transform.position;
        _mat_inactive = _button.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(_door_open)
        {
            _door.transform.position = Vector3.Lerp
                (_door.transform.position, _door_open_pos.transform.position, 5 * Time.deltaTime);
            _button.GetComponent<Renderer>().material = _mat_active;
            _door.GetComponent<Renderer>().material = _mat_active;
        }
        else
        {
            _door.transform.position = Vector3.Lerp
                (_door.transform.position, _door_origin_position, 5 * Time.deltaTime);
            _button.GetComponent<Renderer>().material = _mat_inactive;
            _door.GetComponent<Renderer>().material = _mat_inactive;
        }
	}

}
