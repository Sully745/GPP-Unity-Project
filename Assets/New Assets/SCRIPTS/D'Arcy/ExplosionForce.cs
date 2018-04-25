using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 15.0f);
        foreach (Collider hit in collider)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(75.0f, Vector3.forward, 15.0f, -5.0f, ForceMode.Impulse);
            }
        }
    }
	
}
