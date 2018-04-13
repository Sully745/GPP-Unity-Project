using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerLevel : MonoBehaviour
{
    public GameObject GM;
    public int level;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
            float fadeTime = GM.GetComponent<FadeScene>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime * 5);
            SceneManager.LoadScene(level);
    }
}
