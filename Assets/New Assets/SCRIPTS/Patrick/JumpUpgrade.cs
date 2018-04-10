using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpgrade : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem jump_parti;


    public GameObject JumpEffect;
    public GameObject SpeedEffect;

    private float jumpBoost = 0.5f;
    private float newJump;
    public Rigidbody rb;
    public Animator animator;
    private bool doubleJump;
    public bool landed;


    // Use this for initialization
    void Start()
    {
        JumpEffect.GetComponent<ParticleSystem>().Stop();
 
        //SpeedEffect.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") //speed completed
        {
            //Player.GetComponent<PlayerController>().jumpTransfer = true;
            StartCoroutine(jumpDuration());
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            jump_parti.Play(); //nani
            JumpEffect.GetComponent<ParticleSystem>().Play();
            //SpeedEffect.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }
    IEnumerator jumpDuration() //completed
    {
        yield return new WaitForSeconds(10);
        //Player.GetComponent<PlayerController>().doubleJump = false;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<ParticleSystem>().enableEmission = true;
       // Player.GetComponent<ParticleSystem>().Stop(); //nani
        JumpEffect.GetComponent<ParticleSystem>().Stop();
        //Player.GetComponent<PlayerController>().jumpTransfer = false;

    }
}
