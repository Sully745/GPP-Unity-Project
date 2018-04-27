using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public GameObject boss_bar;
    public ParticleSystem[] fireworks;
    public Camera player_cam;
    public Camera fire_cam;
    float boss_health;
	// Use this for initialization
	void Start () {
        fire_cam.enabled = false;

        foreach (ParticleSystem fire in fireworks)
        {
            fire.Pause();
        }
	}
	
	// Update is called once per frame
	void Update () {
        boss_health = boss_bar.GetComponentInChildren<UIBoss>().health;
        Debug.Log(boss_health);

        if (boss_health <= 0)
        {
            StartCoroutine(end(3));
        }
    }

    IEnumerator end(int time)
    {
        yield return new WaitForSeconds(time);
        player_cam.enabled = false;

        fire_cam.enabled = true;
        Debug.Log("started");
        foreach (ParticleSystem fire in fireworks)
        {
            fire.Play();
        }
    }
}
