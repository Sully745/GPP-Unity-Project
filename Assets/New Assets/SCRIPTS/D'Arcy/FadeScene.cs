using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    public Texture2D fade;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    public int HP = 0;
    public static bool level1_completed = false;
    public static bool level2_completed = false;
    public bool l1;
    public bool l2;
    public GameObject[] spawn_index;

    public GameObject main_camera;
    public GameObject player;
    public GameObject UI;

    private static bool created = false;
    private void Awake()
    {
        if (!created)
        {
            player.SetActive(true);
            main_camera.SetActive(true);
            UI.SetActive(true);

            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    private void Update()
    {
        if(l1)
        {
            level1_completed = true;
        }
        if (l2)
        {
            level2_completed = true;
        }
    }

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fade);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }
}
