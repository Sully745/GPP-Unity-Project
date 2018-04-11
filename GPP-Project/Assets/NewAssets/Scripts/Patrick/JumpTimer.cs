using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTimer : MonoBehaviour
{

    public float myCoolTimer = 10;
    private Text timerText;
    // Use this for initialization
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myCoolTimer -= Time.deltaTime;
        timerText.text = myCoolTimer.ToString("f0");
        print(myCoolTimer);
    }
}
