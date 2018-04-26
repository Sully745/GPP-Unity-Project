using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 25.0f);
        foreach (Collider hit in collider)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-Vector3.forward * 5f, ForceMode.Impulse);
            }
        }
    }
	
}
