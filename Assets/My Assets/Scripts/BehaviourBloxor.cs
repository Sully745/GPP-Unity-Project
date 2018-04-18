using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentState
{
    ATTACKING,
    PATROLLINGRANDOM,
    PATROLLINGPATH
}

public class BehaviourBloxor : MonoBehaviour {
    //Stats
    [Header("Stats")]
    public int health;
    public int max_health = 3;
    public int damage = 20;

    [Header("Attack Stats")]
    public float range = 100;
    public float attack_length = .5f;
    public float aoe_trigger_distance = 15;
    public float aoe_cooldown = 10;
    private float distance_to_player;

    [Header("Movement Stats")]
    public float distance = 50;
    public float height = 20;
    public float repeat_rate = 1;
    public float rotation_speed = 10;
    Vector3 direction;
    Quaternion rotation;
    public CurrentState current_state;
    private CurrentState prev_state;
    

    //attack bools
    bool has_attacked = false;
    private bool player_in_range;
    private bool attacking;
    bool attack_aoe = false;
    bool aoe_ready = true;

    //state bools
    [HideInInspector]
    public bool in_area;
    private bool grounded;

    //components    
    Rigidbody rb;
    [Header("Components")]
    public GameObject target;
    public GameObject boss_bar;
    public GameObject patrol_area;
    public GameObject powerup;
    public ParticleSystem goo_split;
    public ParticleSystem particle_aoe;
    public Material mat_normal;
    public Material mat_hit;
    public Camera player_cam;

    // Use this for initialization
    void Start () {
        health = max_health;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Direction", 0, repeat_rate);
        prev_state = current_state;
        boss_bar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance_to_player = Vector3.Distance(transform.position, target.transform.position);
        //Debug.Log(distance_to_player);
        Die();

        IsGrounded();
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * rotation_speed);
        int player_health = target.GetComponent<PlayerController>().health;
        if (current_state == CurrentState.ATTACKING && distance_to_player > 30 && !in_area && max_health == 3 || player_health <= 0)
        {
            if (max_health < 3)
            {
                current_state = CurrentState.PATROLLINGRANDOM;
            }
            else
            {
                current_state = prev_state;
            }
        }
        if (distance_to_player < 20 && player_health > 0)
        {
            current_state = CurrentState.ATTACKING;            
        }
        
        if (current_state == CurrentState.ATTACKING && boss_bar.GetComponentInChildren<UIBoss>().health > 0)
        {
            boss_bar.SetActive(true);            
        }
        else
        {
            boss_bar.SetActive(false);
        }        
    }

    void IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance <= transform.localScale.x / 2 + .1f)
            {
                if (attack_aoe && hit.collider.tag == "Environment")
                {
                    Vector3 spawn_pos = transform.position;
                    spawn_pos.y = 1.5f;
                    Instantiate(particle_aoe, spawn_pos, Quaternion.Euler(90, 0, 0));
                    attack_aoe = false;
                    StartCoroutine(player_cam.GetComponent<CameraFollow>().CameraShake(.15f, .6f));
                    StartCoroutine(target.GetComponent<PlayerController>().ControllerRumble(.3f, 1));
                }
                grounded = true;
            }
            else
            {
                grounded = false;                
            }
        }
    }

    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }


    void Direction()
    {
        switch (current_state)
        {
            case CurrentState.ATTACKING:
                direction = target.transform.position - transform.position;
                direction.y = 0;
                StartCoroutine(Attack(.3f));
                break;
            case CurrentState.PATROLLINGRANDOM:
                Patrolling();
                Invoke("Movement", .3f);
                break;
            case CurrentState.PATROLLINGPATH:
                direction = GetComponent<EnemyPath>().direction;
                Invoke("Movement", .3f);
                break;
        }
    }

    void Patrolling()
    {
        switch (in_area)
        {
            case true:
                direction = new Vector3(Random.Range(-360, 360), 0, Random.Range(-360, 360)) - transform.position;
                direction.y = 0;
                break;
            case false:
                direction = patrol_area.transform.position - transform.position;
                direction.y = 0;
                break;
        }
    }

    IEnumerator Attack(float delay = 0.0f)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (grounded)
        {
            if (aoe_ready && !has_attacked && !player_in_range && distance_to_player >= aoe_trigger_distance)
            {
                rb.AddRelativeForce(0, height*2, distance*3, ForceMode.Impulse); //forward
                yield return new WaitForSeconds(.5f);
                attacking = true;
                attack_aoe = true;
                aoe_ready = false;
                has_attacked = true;

                yield return new WaitForSeconds(aoe_cooldown);
                aoe_ready = true;
            }
            else if (!has_attacked && player_in_range)
            {
                rb.AddRelativeForce(0, 0, range, ForceMode.Impulse); //forward
                has_attacked = true;
                attacking = true;
                yield return new WaitForSeconds(attack_length);
                attacking = false;
            }
            else if (has_attacked)
            {
                rb.AddRelativeForce(0, height, -distance/2, ForceMode.Impulse); //forward
                has_attacked = false;
            }
            else
            {
                rb.AddRelativeForce(0, height, distance, ForceMode.Impulse); //forward
            }
        }        
    }

    IEnumerator TakeDamage()
    {
        health -= 1;
        boss_bar.GetComponentInChildren<UIBoss>().health -= 1;
        if (health > 0)
        {
            GetComponent<MeshRenderer>().material = mat_hit;
            yield return new WaitForSeconds(.1f);
            GetComponent<MeshRenderer>().material = mat_normal;
        }
    }

    void Die()
    {
        if (health <= 0)
        {
            Instantiate(goo_split, transform.position, Quaternion.identity);
            GameObject health = Instantiate(powerup, transform.position - new Vector3(0, .5f, 0), Quaternion.identity);
            health.GetComponent<Powerup>().this_powerup = PowerupType.HEALTH;
            if (max_health > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector3 new_pos = transform.position + new Vector3(Random.Range(-2, 2), Random.value, Random.Range(-2, 2));
                    GameObject clone = Instantiate(gameObject, new_pos, Quaternion.identity);
                    clone.GetComponent<BehaviourBloxor>().max_health = max_health - 1;
                    clone.GetComponent<BehaviourBloxor>().has_attacked = true;
                    clone.transform.localScale += new Vector3(-1f, -1f, -1f);
                }
            }            
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        if (grounded)
        {
            rb.AddRelativeForce(0, 0, distance, ForceMode.Impulse); //forward
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && other.tag == "Player")
        {
            player_in_range = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.tag == "Player")
        {
            player_in_range = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.collider.isTrigger && other.collider.tag == "Player" && attacking)
        {
            attacking = false;
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 1;
            StartCoroutine(target.GetComponent<PlayerController>().TakeHit(damage, dir, 5));
        }
    }
}
