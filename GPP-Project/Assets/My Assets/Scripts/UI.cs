using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Letterbox
    //public float target_pos_y_top;
    //public float target_pos_y_bottom;
    //Vector2 new_pos_top;
    //Vector2 new_pos_bottom;

    //RedScreen
    //public Color full_color;

    //PlayerHealth
    
    //BossHealth

    // Use this for initialization
    void Start()
    {
        //Letterbox
        //new_pos_top = transform.GetChild(1).GetComponent<Image>().rectTransform.position;
        //new_pos_bottom = transform.GetChild(2).GetComponent<Image>().rectTransform.position;
        //target_pos_y_top = transform.GetChild(1).GetComponent<Image>().rectTransform.position.y;
        //target_pos_y_bottom = transform.GetChild(2).GetComponent<Image>().rectTransform.position.y;

        //RedScreen
        //full_color = transform.GetChild(0).GetComponent<Image>().color;
        //full_color.a = 0;
        
        //PlayerHealth

        //BossHealth
    }

    // Update is called once per frame
    void Update()
    {
        //LetterBox
        //new_pos_top.y = Mathf.MoveTowards(new_pos_top.y, target_pos_y_top, 1);
        //new_pos_bottom.y = Mathf.MoveTowards(new_pos_bottom.y, target_pos_y_bottom, 1);
        //transform.GetChild(1).GetComponent<Image>().rectTransform.position = new_pos_top;
        //transform.GetChild(2).GetComponent<Image>().rectTransform.position = new_pos_bottom;

        //RedScreen
        //full_color.a = Mathf.MoveTowards(full_color.a, 0, .008f);
        //transform.GetChild(0).GetComponent<Image>().color = full_color;

        //PlayerHealth

        //BossHealth
    }
}