using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBridge : MonoBehaviour
{
    public GameObject bridge;
    public GameObject bridge_pos;
    private GameObject GM;

    public GameObject LVL_1_goal;
    public GameObject LVL_2_goal;
    public Material active;
    private bool check;

    public bool triggered_door;
    private static bool door;
    public GameObject explosive_door;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
        door = triggered_door;
        if(door)
        {
            explosive_door.SetActive(false);
        }
        if (GM.GetComponent<FadeScene>().l1)
        {
            LVL_1_goal.GetComponent<Renderer>().material = active;
        }
        if (GM.GetComponent<FadeScene>().l2)
        {
            LVL_2_goal.GetComponent<Renderer>().material = active;
        }
        if(check)
        {
            if (GM.GetComponent<FadeScene>().l1 && GM.GetComponent<FadeScene>().l2)
            {
                bridge.transform.position = Vector3.Lerp(bridge.transform.position, bridge_pos.transform.position, Time.deltaTime* 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            check = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            check = false;
        }
    }
    
}
