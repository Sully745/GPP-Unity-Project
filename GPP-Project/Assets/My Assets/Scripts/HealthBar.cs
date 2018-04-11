using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {
    public GameObject player;

    int player_health;
    float current_health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        player_health = player.GetComponent<PlayerController>().health;
        current_health = Mathf.MoveTowards(current_health, player_health, 1);
        GetComponent<Slider>().value = current_health;
        if (current_health <= 0)
        {
            Color empty = new Color(0, 0, 0, 0);
            transform.GetChild(1).GetChild(0).GetComponent<Image>().color = empty;
        }
	}
}
