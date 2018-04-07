using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UILetterbox : MonoBehaviour {

    public float target_pos_y_top;
    public float target_pos_y_bottom;
    Vector2 new_pos_top;
    Vector2 new_pos_bottom;

    // Use this for initialization
    void Start () {
        new_pos_top = transform.GetChild(0).GetComponent<Image>().rectTransform.position;
        new_pos_bottom = transform.GetChild(1).GetComponent<Image>().rectTransform.position;
        target_pos_y_top = transform.GetChild(0).GetComponent<Image>().rectTransform.position.y;
        target_pos_y_bottom = transform.GetChild(1).GetComponent<Image>().rectTransform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        new_pos_top.y = Mathf.MoveTowards(new_pos_top.y, target_pos_y_top, 1);
        new_pos_bottom.y = Mathf.MoveTowards(new_pos_bottom.y, target_pos_y_bottom, 1);
        transform.GetChild(0).GetComponent<Image>().rectTransform.position = new_pos_top;
        transform.GetChild(1).GetComponent<Image>().rectTransform.position = new_pos_bottom;    }
}
