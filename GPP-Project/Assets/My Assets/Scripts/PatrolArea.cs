using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolArea : MonoBehaviour {

    private GameObject[] bloxors;
    private CurrentState prev_state;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnTriggerEnter(Collider other)
    //{
    //    prev_state = bloxor.GetComponent<BehaviourBloxor>().current_state;
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            other.GetComponent<BehaviourBloxor>().in_area = true;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            bloxors = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject bloxor in bloxors)
            {
                bloxor.GetComponent<BehaviourBloxor>().current_state = CurrentState.ATTACKING;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            other.GetComponent<BehaviourBloxor>().in_area = false;
        }
        //if (other.tag == "Player" && !other.isTrigger)
        //{
        //    bloxor.GetComponent<BehaviourBloxor>().current_state = prev_state;
        //}
    }
}
