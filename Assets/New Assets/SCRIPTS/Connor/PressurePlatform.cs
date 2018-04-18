using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatform : MonoBehaviour {

    public GameObject platform;
    public Material mat_off;
    public Material mat_on;
	// Use this for initialization
	void Start () {

        platform.GetComponent<ObjectPathFollow>().speed = 0;

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerStay (Collider coll)
    {
        platform.GetComponent<ObjectPathFollow>().speed = 5;
        GetComponent<MeshRenderer>().material = mat_on;
    }

    void OnTriggerExit(Collider coll)
    {
        platform.GetComponent<ObjectPathFollow>().speed = 0;
        GetComponent<MeshRenderer>().material = mat_off;
    }
}
