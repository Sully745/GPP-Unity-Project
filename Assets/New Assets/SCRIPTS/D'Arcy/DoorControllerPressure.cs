using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerPressure : MonoBehaviour
{
    public GameObject _door_open_pos;
    public GameObject _door;
    public GameObject[] _pipes;
    public Material _mat_active;
    public Material _mat_inactive;
    public bool _door_open = false;

    private Vector3 _door_origin_position;

    public GameObject _switch_activator;
    public GameObject _plate;
    // Use this for initialization
    void Start()
    {
        _door_origin_position = _door.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_door_open)
        {
            _door.transform.position = Vector3.Lerp
                (_door.transform.position, _door_open_pos.transform.position, 5 * Time.deltaTime);
            _plate.GetComponent<Renderer>().material = _mat_active;
            _switch_activator.GetComponent<Renderer>().material = _mat_active;
            _door.GetComponent<Renderer>().material = _mat_active;
            for (int iter = 0; iter < _pipes.Length; iter++)
            {
                _pipes[iter].GetComponent<Renderer>().material = _mat_active;
            }
        }
        else
        {
            _door.transform.position = Vector3.Lerp
                (_door.transform.position, _door_origin_position, 5 * Time.deltaTime);
            _plate.GetComponent<Renderer>().material = _mat_inactive;
            _switch_activator.GetComponent<Renderer>().material = _mat_inactive;
            _door.GetComponent<Renderer>().material = _mat_inactive;
            for (int iter = 0; iter < _pipes.Length; iter++)
            {
                _pipes[iter].GetComponent<Renderer>().material = _mat_inactive;
            }
        }
    }
}
