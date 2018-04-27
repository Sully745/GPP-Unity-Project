using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCreation : MonoBehaviour
{
    private static bool created = false;
    public GameObject GM;

    private void Awake()
    {
        if (!created && GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            GM.SetActive(true);
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
}
