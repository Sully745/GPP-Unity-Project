using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerupType
{
    DOUBLEJUMP,
    DOUBLESPEED,
    HEALTH
};

public class Powerup : MonoBehaviour {

    Material this_mat;
    public PowerupType this_powerup;
    private float y_pos;
	// Use this for initialization
	void Start () {
        this_mat = GetComponent<Renderer>().material;
        y_pos = transform.position.y;

        switch (this_powerup)
        {
            case PowerupType.DOUBLEJUMP:
                this_mat.color = Color.red;
                break;
            case PowerupType.DOUBLESPEED:
                this_mat.color = Color.blue;
                break;
            case PowerupType.HEALTH:
                this_mat.color = Color.green;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, y_pos + (Mathf.Sin(Time.fixedTime) / 2) + .5f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            PlayerController player_controller = other.GetComponent<PlayerController>();
            //player_controller.ActivatePowerup(this_powerup);
            Destroy(gameObject);
        }
    }    
}
