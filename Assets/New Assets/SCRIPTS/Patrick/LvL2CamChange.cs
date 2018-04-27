using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvL2CamChange : MonoBehaviour
{
    public Camera mainCamera;
    public Camera level2Camera;

    public bool exit_col;

    // Use this for initialization
    void Start()
    {
        mainCamera.enabled = true;
        level2Camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!exit_col)
            {
                //mainCamera.GetComponent<CameraFollow>().close_camera = true;
                mainCamera.enabled = false;
                level2Camera.enabled = true;
            }
        }

    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            if (exit_col)
            {
                mainCamera.enabled = true;
                level2Camera.enabled = false;
                //mainCamera.GetComponent<CameraFollow>().close_camera = false;
            }
        }
    }
}
