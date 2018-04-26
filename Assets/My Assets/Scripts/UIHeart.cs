using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour {
    public GameObject player;

    int player_health;
    float current_health;
    bool heart_beat = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        current_health = player.GetComponent<PlayerController>().health;
    }

    // Update is called once per frame
    void Update()
    {
        player_health = player.GetComponent<PlayerController>().health;
        current_health = Mathf.MoveTowards(current_health, player_health, 1f);
        GetComponent<Image>().fillAmount = current_health / 100;
        transform.parent.GetChild(1).GetComponent<Text>().text = current_health.ToString();
        if (current_health <= 20 && !heart_beat)
        {
            StartCoroutine(HeartBeat());
            heart_beat = true;
        }
    }

    private IEnumerator HeartBeat()
    {
        while (current_health <= 20 && current_health > 0)
        {
            transform.parent.localScale = new Vector3(3, 3, 1);
            StartCoroutine(player.GetComponent<PlayerController>().ControllerRumble(.1f, 1));
            yield return new WaitForSeconds(.1f);
            transform.parent.localScale = new Vector3(2, 2, 1);
            yield return new WaitForSeconds(.1f);
            transform.parent.localScale = new Vector3(3, 3, 1);
            StartCoroutine(player.GetComponent<PlayerController>().ControllerRumble(.1f, 1));
            yield return new WaitForSeconds(.1f);
            transform.parent.localScale = new Vector3(2, 2, 1);
            yield return new WaitForSeconds(1f);

        }
        transform.parent.localScale = new Vector3(2, 2, 1);
    }
}
