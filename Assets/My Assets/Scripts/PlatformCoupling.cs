using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCoupling : MonoBehaviour
{
    public GameObject parent;
    GameObject player;
    bool player_on = false;
    public Vector3 rotation;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player_on)
        {
            rotation = transform.InverseTransformPoint(player.transform.position);
            //rotation.x = rotation.x * 30;
            //rotation.z = rotation.z * 30;
        }
        else
        {
            rotation = new Vector3 (0, 0, 0);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == "Player")
        {
            if (GetComponent<ObjectPathFollow>().submerge)
            {
                GetComponent<ObjectPathFollow>().offset = new Vector3(0, -.5f, 0);
                StartCoroutine(reset());
            }
            player_on = true;

            //other.transform.parent = gameObject.transform;
            other.transform.parent = parent.transform;
            player = other.gameObject;
        }
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<ObjectPathFollow>().offset = new Vector3(0, 0, 0);

    }

    void OnTriggerExit(Collider other)
    {
        player_on = false;
        if (!other.isTrigger && other.tag == "Player")
        {
            GetComponent<ObjectPathFollow>().offset = new Vector3(0, 0f, 0);

            other.transform.parent = null;
        }
    }
}
