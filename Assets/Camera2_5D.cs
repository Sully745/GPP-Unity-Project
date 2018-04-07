using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2_5D : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;
    public Transform target;
    float rotate;
    private Vector3 velocity = Vector3.zero;
    public float smooth_time = .125f;

    // Use this for initialization
    void Start()
    {
        //transform.parent = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown("i"))
        //{
        //    rotate = 90;
        //}
        //else
        //{
        //    rotate = 0;
        //}
       // rotate = player.transform.rotation.y;
    }

    void LateUpdate()
    {
        //offset = Quaternion.AngleAxis(rotate * 1, Vector3.up) * offset;
        //transform.position = player.transform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3 (0, 10, -10), ref velocity, smooth_time);
       // transform.rotation = player.transform.rotation;

        transform.LookAt(player.transform.position);
    }
}
