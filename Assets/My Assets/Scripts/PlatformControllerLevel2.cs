using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerLevel2 : MonoBehaviour {

    public GameObject[] timed_platforms;
    //public GameObject[] break_platforms;
    public GameObject[] alter_platforms;

    private int i = 0;

    public float seconds_for_timed = 2;

    // Use this for initialization
    void Start () {
        foreach (GameObject plat in timed_platforms)
        {
            plat.SetActive(false);
        }
        StartCoroutine("Timed");

        //foreach (GameObject plat in alter_platforms)
        //{
        //    plat.SetActive(false);
        //}
        StartCoroutine("Alternate");
    }

    // Update is called once per frame
    void Update () {

    }

    private IEnumerator Timed()
    {
        timed_platforms[i].SetActive(true);
        yield return new WaitForSeconds(seconds_for_timed);
        timed_platforms[i].SetActive(false);
        i++;
        if (i > timed_platforms.Length -1)
        {
            i = 0;
        }
        StartCoroutine("Timed");
    }

    private IEnumerator Alternate()
    {
        for (int k = 0; k < alter_platforms.Length; k++)
        {
            if (k % 2 == 0)
            {
                alter_platforms[k].SetActive(!alter_platforms[k].activeSelf);
            }
            else
            {
                alter_platforms[k].SetActive(!alter_platforms[k].activeSelf);
            }
        }

        yield return new WaitForSeconds(seconds_for_timed);
        StartCoroutine("Alternate");
    }
}
