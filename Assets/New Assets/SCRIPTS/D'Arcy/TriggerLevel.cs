using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerLevel : MonoBehaviour
{
    private GameObject GM;
    public int level;
    public int spawn_index = 0;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
    }

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
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        player.transform.position = GM.GetComponent<FadeScene>().spawn_index[spawn_index].transform.position;
        spawn_index = 0;
    }
}
