using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Events;
using XInputDotNetPure; // Required in C#


public class PlayerController : MonoBehaviour {

    //Components
    public Rigidbody rb;
    public Animator animator;
    public CapsuleCollider capsule;
    public Camera player_camera;
    public Camera target_camera;
    //public ParticleSystem[] jump_ring;
    //public ParticleSystem[] speed_trail;
    public ParticleSystem goo_hit;
    public Vector3 DirectionSlope;
    public float SlopeAngle;
    public Image red_screen;
    public Text death_text;

    private S_Flames flames;
    private S_Lightning lightning;
    private S_Smoke smoke;

    public int double_jump_forward_velocity;

    GameObject[] enemies;
    public GameObject closest_enemy;
    public bool targeting = false;

    //powerups
    public bool double_jump = false;
    public bool double_jump_used = false;
    public bool speed_up = false;

    //variables
    bool is_dead = false;
    bool grounded = true;
    bool is_falling = false;
    bool jumping = false;
    bool start_fall = false;
    public bool can_move = true;
    public bool can_action = true;
    public bool following_path = true;
    public int health = 100;
    public bool knocked = false;

    float falling_velocity = -4f;
    float move_speed = 0.0f;
    float walk_speed = 5.0f;
    float run_speed = 10.0f;
    float rotation_speed = 40;
    float path_rotation_speed = 10;
    float jump_force = 180;
    public float dist_to_ground;
    bool aoe_hit = false;
    Vector3 input_vector;
    Vector3 new_velocity;

    //inputs 
    float input_horizontal;
    float input_vertical;
    float input_run;
    bool input_attack_l;
    bool input_attack_r;
    bool input_target;

    bool input_attack_l_kick;
    bool input_attack_r_kick;


    //xinput 
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    // Use this for initialization

    private static bool created = false;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    void Start ()
    {
        death_text.enabled = false;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();

        flames = GetComponent<S_Flames>();
        lightning = GetComponent<S_Lightning>();
        smoke = GetComponent<S_Smoke>();

        target_camera.enabled = false;
        player_camera.enabled = true;
        animator.SetInteger("health", health);
    }

    // Update is called once per frame
    void Update ()
    {

        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        if (!is_dead)
        {
            Attacks();
            Jumping();
        }

        CameraBasedMovement();

        Inputs();
        IsGrounded();
        Gravity();
        //SpeedUpParticle();

        animator.SetInteger("health", health);
    }

    IEnumerator Attack(string attack, float time)
    {
        can_move = false;
        can_action = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        animator.applyRootMotion = true;
        animator.SetTrigger("Attack " + attack + " Trigger");
        yield return new WaitForSeconds(time);
        animator.applyRootMotion = false;
        can_action = true;
        can_move = true;
    }

    void Gravity()
    {
        if (move_speed <= .1 && IsGrounded() && SlopeAngle < 45 && !jumping)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
        }
        else
        {
            GetComponent<ConstantForce>().force = new Vector3(0, -360, 0);
        }
    }

    void Inputs()
    {
        input_horizontal = Input.GetAxis("Horizontal");
        input_vertical = Input.GetAxis("Vertical");
        input_run = Input.GetAxis("Run");
        input_attack_l = Input.GetButtonDown("AttackL");
        input_attack_r = Input.GetButtonDown("AttackR");
        input_attack_l_kick = Input.GetButtonDown("AttackLKick");
        input_attack_r_kick = Input.GetButtonDown("AttackRKick");
        input_target = Input.GetButtonDown("Target Camera");

        if (!closest_enemy)
        {
            targeting = false;
        }

        if (input_target)
        {
            switch (targeting)
            {
                case true:
                    targeting = false;
                    break;

                case false:
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    float temp_distance = 100;
                    foreach (GameObject enemy in enemies)
                    {
                        if (Vector3.Distance(transform.position, enemy.transform.position) < temp_distance)
                        {
                            temp_distance = Vector3.Distance(transform.position, enemy.transform.position);
                            closest_enemy = enemy;
                        }
                    }
                    targeting = true;
                    break;
            }
        }

        animator.SetBool("Targeting", targeting);
    }

    void Attacks()
    {
        if (can_action && grounded)
        {
            if (input_attack_l)
            {
                animator.SetBool("Moving", false);
                StartCoroutine(Attack("PL",0.8f));
            }
            if (input_attack_r)
            {

                animator.SetBool("Moving", false);
                StartCoroutine(Attack("PR",0.8f));
            }
            if (input_attack_l_kick)
            {
                animator.SetBool("Moving", false);
                StartCoroutine(Attack("KL",1f));
            }
            if (input_attack_r_kick)
            {
                animator.SetBool("Moving", false);
                StartCoroutine(Attack("KR",1f));
            }
        }        
    }

    //make input relative to camera
    void CameraBasedMovement()
    {
        Transform camera_transform;

        camera_transform = player_camera.transform;

        //forward vector based on camera
        Vector3 forward = camera_transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        //right vector relative to the camera, based on forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        if (!following_path)
        {
            input_vector = input_horizontal * right + input_vertical * forward;
        }
        else
        {
            input_horizontal = Input.GetAxisRaw("Horizontal");

            List<Vector3> _path = GetComponent<PathFollow>()._path;
            int target_node = GetComponent<PathFollow>().target_node;
            input_vector = _path[target_node] - transform.position;
            input_vector.y = 0;
            input_vector = input_vector * (input_horizontal*input_horizontal);
        }
    }

    void RotateTowardsMovement()
    {
        if (input_vector != Vector3.zero)
        {
            if (following_path)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(input_vector), Time.deltaTime * path_rotation_speed);
            }
            else if (targeting)
            {
                //rotate towards enemy
                Vector3 dir = closest_enemy.transform.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotation_speed);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(input_vector), Time.deltaTime * rotation_speed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(input_vector), Time.deltaTime * rotation_speed);

            }
        }
    }

    float UpdateMovement()
    {
        
        CameraBasedMovement();
        
        Vector3 motion = input_vector;
        if (IsGrounded())
        {
            //diagonal slow down
            if (motion.magnitude > 1)
            {
                motion.Normalize();
            }
            if (input_run > walk_speed / run_speed && !targeting)
            {
                new_velocity = motion * run_speed * input_run;
            }
            else
            {
                new_velocity = motion * walk_speed;
            }                    
        }
        else
        {
            //falling velocity
            new_velocity = rb.velocity;
        }   
        new_velocity.y = rb.velocity.y;
        if (knocked)
        {
            rb.velocity = rb.velocity;
        }
        else if (can_move)
        {
            RotateTowardsMovement();
            
            rb.velocity = new_velocity;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        //return a value to pass animator
        return input_vector.magnitude;
    }

    void FixedUpdate()
    {
        move_speed = UpdateMovement();

        if (rb.velocity.y < falling_velocity)
        {
            is_falling = true;
        }
        else
        {
            is_falling = false;
        }

    }

    //pass values to animator
    void LateUpdate()
    {
        if (move_speed > 0 && can_move)
        {
            animator.SetBool("Moving", true);
        }
        else if (move_speed == 0)
        {
            animator.SetBool("Moving", false);
        }

        //Get local velocity of charcter
        float velocityX = transform.InverseTransformDirection(rb.velocity).x;
        float velocityZ = transform.InverseTransformDirection(rb.velocity).z;
        //Update animator with movement values
        animator.SetFloat("Velocity X", velocityX / walk_speed);
        animator.SetFloat("Velocity Z", velocityZ / walk_speed);
    }

    void Jumping()
    {        
        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump") && !jumping && can_action && !is_falling)
            {
                StartCoroutine(PlayerJump());
            }
        }
        else if (Input.GetButtonDown("Jump") && !double_jump_used && double_jump)
        {
            StartCoroutine(PlayerJump());
            double_jump_used = true;
            smoke.SmokePlay(2.0f);
            
            //DoubleJumpParticle();
        }
        else
        {
            if (is_falling)
            {
                {
                    animator.SetInteger("Jumping Int", 2);
                    if (!start_fall)
                    {
                        animator.SetTrigger("Jumping Trigger");
                        start_fall = true;
                    }
                }                
            }           
        }
    }

    public IEnumerator PlayerJump()
    {
        jumping = true;
        can_action = false;        
        animator.SetInteger("Jumping Int", 1);
        animator.SetTrigger("Jumping Trigger");
        if(!double_jump_used && double_jump)
        {
            if(rb.velocity.x >= 0.1 || rb.velocity.z >= 0.1
                || rb.velocity.x <= -0.1 || rb.velocity.z <= -0.1)
            {
                rb.AddRelativeForce(Vector3.forward * double_jump_forward_velocity, ForceMode.Impulse);
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        rb.velocity = new Vector3(0, 0, 0);

        Vector3 new_jump = input_vector * 100 + new Vector3(0, jump_force, 0);
        //rb.AddForce(0, jump_force, 0, ForceMode.Impulse);
        rb.AddForce(new_jump, ForceMode.Impulse);

        yield return new WaitForSeconds(.4f);
        can_action = true;
        jumping = false;        
    }

    bool IsGrounded()
    {
        if (Physics.CheckCapsule(capsule.bounds.center,
            new Vector3(capsule.bounds.center.x, capsule.bounds.min.y -1f, capsule.bounds.center.z),
            .5f, 1, QueryTriggerInteraction.Ignore) && SlopeAngle < 45)
        {
            start_fall = false;
            if (!jumping)
            {
                animator.SetInteger("Jumping Int", 0);
            }
            if (double_jump)
            {
                double_jump_used = false;
            }
            grounded = true;
            return true;
        }
        else if (dist_to_ground < 3 && SlopeAngle <= 45)
        {
            start_fall = false;
            if (!jumping)
            {
                animator.SetInteger("Jumping Int", 0);
            }
            grounded = true;
            return true;
        }
        else
        {
            grounded = false;
            return false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "2.5D")
        {
            following_path = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "2.5D")
        {
            following_path = false;
        }
    }

    public void ActivatePowerup(PowerupType powerup_type, float duration)
    {
        switch (powerup_type)
        {
            case PowerupType.DOUBLEJUMP:
                lightning.LightningPlay(duration);
                StopCoroutine("DoubleJump");
                StartCoroutine("DoubleJump", duration);
                break;
            case PowerupType.DOUBLESPEED:
                flames.FlamePlay(duration);
                StopCoroutine("SpeedUp");
                StartCoroutine("SpeedUp", duration);
                break;
            case PowerupType.HEALTH:
                health += 10;
                if (health > 100)
                {
                    health = 100;
                }
                break;
        }
    }

    IEnumerator DoubleJump(float duration)
    {
        double_jump = true;
        //jump_ring[3].Play();
        yield return new WaitForSeconds(duration);
        //jump_ring[3].Stop();
        double_jump = false;
    }


    //void SpeedUpParticle()
    //{
    //    if (speed_up == true)
    //    {
    //        if (input_run > 0.8f && !speed_trail[0].isPlaying)
    //        {
    //            speed_trail[0].Play();
    //        }
    //        else if (input_run < 0.8f && speed_trail[0].isPlaying)
    //        {
    //            speed_trail[0].Stop();
    //        }

    //        if (!speed_trail[1].isPlaying)
    //        {
    //            speed_trail[1].Play();
    //        }
    //    }
    //}

    IEnumerator SpeedUp(float duration)
    {
        //speed_trail[0].Play();

        speed_up = true;
        run_speed = 17.0f;
        walk_speed = 8f;
        animator.speed = 1.5f;
        yield return new WaitForSeconds(duration);
        //foreach (ParticleSystem effect in speed_trail)
        //{
        //    effect.Stop();
        //}
        animator.speed = 1;
        run_speed = 10.0f;
        walk_speed = 5f;
        speed_up = false;
    }

    //void DoubleJumpParticle()
    //{
    //    foreach(ParticleSystem effect in jump_ring)
    //    {
    //        effect.Play();
    //    }
    //}

    private void OnParticleCollision()
    {
        if (!aoe_hit)
        {
            aoe_hit = true;
            StartCoroutine(AoeHit());
        }
    }

    public IEnumerator Knockback(Vector3 direction, float force)
    {
        if (!is_dead)
        {
            //INTERRUPT ATTACK
            StopCoroutine("Attack");
            animator.applyRootMotion = false;
            can_action = true;
            can_move = true;

            knocked = true;
            is_falling = true;
            direction.y = direction.y * 2;
            rb.AddForce(direction * force, ForceMode.VelocityChange);
            yield return new WaitForSeconds(.5f);
            Debug.Log("reset");
            knocked = false;

        }
    }

    public IEnumerator ControllerRumble(float duration, float intensity)
    {
        //xinput vibration
        GamePad.SetVibration(playerIndex, intensity, intensity);
        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(playerIndex, 0, 0);

    }

    IEnumerator AoeHit()
    {
        StartCoroutine(TakeHit(20, Vector3.zero, 0));
        //TakeHit(20, Vector3.zero, 0);
        yield return new WaitForSeconds(3);
        aoe_hit = false;
    }

    public IEnumerator TakeHit(int damage, Vector3 dir, float force)
    {
        if (!is_dead)
        {
            StartCoroutine(Knockback(dir, force));
            StartCoroutine(ControllerRumble(.15f, 1));
            StartCoroutine(player_camera.GetComponent<CameraFollow>().CameraShake(.15f, .6f));
            red_screen.GetComponent<UIRedScreen>().full_color.a = .4f;
            yield return new WaitForSeconds(.15f);
            health -= damage;
            die();
        }
    }

    void die()
    {
        if (health <= 0)
        {
            rb.velocity = Vector3.zero;
            death_text.enabled = true;
            is_dead = true;
            run_speed = 0;
            walk_speed = 0;
            rotation_speed = 0;
            red_screen.GetComponent<UIRedScreen>().increment = .0008f;
            red_screen.GetComponent<UIRedScreen>().target_a = .6f;
        }
    }

    //Animation Events
    void Jump()
    {
    }

    void Land()
    {
    }

    void FootL()
    {
    }

    void FootR()
    {
    }

    void Hit()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2.3f;

        if (Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), forward, out hit))
        {
            if (hit.distance <= 2.3f && !hit.collider.isTrigger)
            {
                StartCoroutine(ControllerRumble(0.15f, 1));
                StartCoroutine(player_camera.GetComponent<CameraFollow>().CameraShake(.15f, .6f));

                hit.transform.SendMessage("TakeDamage");
                forward.y = 1;
                hit.rigidbody.AddForce((forward.normalized * 3000) * 450/5);
                Instantiate(goo_hit, hit.point, Quaternion.identity);

            }
        }

        Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), forward, Color.green);

    }
}
