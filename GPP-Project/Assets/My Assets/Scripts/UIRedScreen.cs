using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRedScreen : MonoBehaviour {

    public Color full_color;
    public float target_a = 0;
    public float increment = 0.008f;

    // Use this for initialization
    void Start () {
        full_color = GetComponent<Image>().color;
        full_color.a = 0;
    }
	
	// Update is called once per frame
	void Update () {
        full_color.a = Mathf.MoveTowards(full_color.a, target_a, increment);
        GetComponent<Image>().color = full_color;
    }
}
