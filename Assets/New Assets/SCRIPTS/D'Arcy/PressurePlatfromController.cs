using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatfromController : MonoBehaviour
{
    public GameObject _plate;
    public GameObject _platform;
    public GameObject _pos_0;
    public GameObject _pos_1;
    public bool move = false;
    private bool flip = false;
    public Material _mat_active;

    private Material _mat_inactive;

    // Use this for initialization
    void Start()
    {
        _mat_inactive = _plate.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            if(_platform.transform.position.x >= _pos_0.transform.position.x - 1)
            {
                flip = false;
            }
            if (_platform.transform.position.x <= _pos_1.transform.position.x + 1)
            {
                flip = true;
            }
            if(flip)
            {
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, _pos_0.transform.position, Time.deltaTime / 2);
            }
            else
            {
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, _pos_1.transform.position, Time.deltaTime / 2);
            }
            _plate.GetComponent<Renderer>().material = _mat_active;

        }
        else
        {
            _plate.GetComponent<Renderer>().material = _mat_inactive;
        }
    }
}
