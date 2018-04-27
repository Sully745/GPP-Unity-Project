using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public GameObject Player;
    private PowerupType type;
    public int duration = 5;
    public int respawn = 5;
    public bool once;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            //Player.GetComponent<PlayerController>().double_jump = true;
            type = PowerupType.HEALTH;
            Player.GetComponent<PlayerController>().ActivatePowerup(type, duration);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            if(!once)
            {
                StartCoroutine(Wait());
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(respawn);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}