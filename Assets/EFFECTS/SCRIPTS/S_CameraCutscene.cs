using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraCutscene : MonoBehaviour
{
    CameraFollow script;
    Animator animator;

    private bool play;
    [SerializeField]
    private float duration;

    public GameObject player;
    private PlayerController pc;

    [SerializeField]
    private Transform start;

    // Use this for initialization
    void Start ()
    {
        pc = player.GetComponent<PlayerController>();
        pc.can_action = false;
        pc.can_move = false;

        script = GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        play = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (play)
        {
            animator.enabled = true;
            script.enabled = false;
            StartCoroutine(_WaitForAnimation(duration));
            play = false;
        }
	}

    IEnumerator _WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.enabled = false;
        script.enabled = true;
        pc.can_action = true;
        pc.can_move = true; ;
    }
}
