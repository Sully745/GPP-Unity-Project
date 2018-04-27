using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBoss : MonoBehaviour {

    public int health = 11;
    float current_health;
    private Transform bar;
    // Use this for initialization
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);
        current_health = health;
        bar = transform.parent;
        foreach (Transform child in bar)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        current_health = Mathf.MoveTowards(current_health, health, .02f);
        if (health == 11)
        {
            GetComponent<Image>().fillAmount = 1;
        }
        else
        {
            GetComponent<Image>().fillAmount = (((100 / 11) * current_health) / 100);
        }
        if (current_health == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
