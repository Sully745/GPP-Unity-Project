using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Transform target_cam_pos;
    public Material transparent;
    public Material opaque;
    Vector3 target_position;
    Vector3 target_position_min;
    Vector3 target_position_max;
    GameObject see_through_object;
    Color see_through_object_color;

    public Transform side_cam_left;
    public Transform side_cam_right;


    public Camera_Side side_view;

    public GameObject player;

    bool targeting = false;
    public bool follow_path = false;

    public Vector3 offset;
    //level 2 in door offset = 0, 3.75, 4
    float rotate;
    public float rotateSpeed;
    public Vector3 h_offset;

    public float smooth_time;
    private Vector3 velocity = Vector3.zero;
    float camera_distance;

    Color new_color;

    GameObject closest_enemy;
    public GameObject UI;

    private static bool created = false;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    // Use this for initialization
    void Start() {
        new_color = transparent.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting)
        {
            UI.GetComponentInChildren<UILetterbox>().target_pos_y_top = Screen.height - 32;
            UI.GetComponentInChildren<UILetterbox>().target_pos_y_bottom = 32;
        }
        else
        {
            UI.GetComponentInChildren<UILetterbox>().target_pos_y_top = Screen.height + 45;
            UI.GetComponentInChildren<UILetterbox>().target_pos_y_bottom = -45;
        }
        //Debug.DrawRay(bloxor.transform.position, (target.position + new Vector3(0, 3, 0) - bloxor.transform.position) * 1.2f, new Color (255, 0, 0));
        //Debug.DrawRay(bloxor.transform.position + (target.position + new Vector3(0, 3, 0) - bloxor.transform.position) * 1.2f, Vector3.forward * 20, new Color(255, 0, 0));

        closest_enemy = player.GetComponent<PlayerController>().closest_enemy;

        camera_distance = (target.position - transform.position).magnitude;

        SetTransparentMaterialAlpha();
        ChangeMaterials();
        follow_path = player.GetComponent<PlayerController>().following_path;
        if (follow_path)
        {
            smooth_time = .5f;
        }
        else
        {
            smooth_time = .15f;
        }
    }

    void SetTransparentMaterialAlpha()
    {
        if (camera_distance < 3)
        {
            float range = 3f - camera_distance;
            //alpha starts at 40%
            float alpha_percentage = .5f - ((range / .7f) / 2f);
            //alpha from 100%
            //float alpha_percentage = 1 - (range / .9f);

            new_color.a = alpha_percentage;

            //player.GetComponentInChildren<SkinnedMeshRenderer>().material.Lerp(opaque, transparent, 1);

        }

    }

    void ChangeMaterials()
    {
        if (camera_distance < 3)
        {
            //lower player opacity
            player.GetComponentInChildren<SkinnedMeshRenderer>().material = transparent;
            transparent.color = new_color;
        }
        else
        {
            player.GetComponentInChildren<SkinnedMeshRenderer>().material = opaque;

        }
    }


    void LateUpdate() {
        Vector3 relative_pos = (target.position + h_offset) - transform.position;

        targeting = player.GetComponent<PlayerController>().targeting;

        // Make camera look at target
        /*Quaternion rotation = Quaternion.LookRotation(relative_pos);
        transform.rotation = rotation;*/
        target_position_max = target.transform.position + offset;
        target_position_min = target.transform.position;

        RaycastHit hit;

        Debug.DrawRay(target_position_min + h_offset, (target_position_max - target_position_min) * 8, Color.green);

        if (Physics.Raycast(target_position_max, transform.forward, out hit, 8, -1, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.tag == "Environment")
            {
                target_position = (hit.point - target.position) * 0.7f + target.position;
            }
            else if (hit.collider.tag == "See Through")
            {
                see_through_object = hit.collider.gameObject;
                target_position = target.transform.position + offset;
                see_through_object_color = see_through_object.GetComponent<MeshRenderer>().material.color;
                see_through_object_color.a = 0.3f;
                see_through_object.GetComponent<MeshRenderer>().material.color = see_through_object_color;
            }
            else
            {
                target_position = target.transform.position + offset;
            }
        }
        else
        {
            if (see_through_object)
            {
                see_through_object_color.a = 1f;
                see_through_object.GetComponent<MeshRenderer>().material.color = see_through_object_color;
            }
            target_position = target.transform.position + offset;

        }

        if(!follow_path)
        {
            if (Input.GetButtonDown("Camera Left"))
            {
                rotate = -45;
            }
            else if (Input.GetButtonDown("Camera Right"))
            {
                rotate = 45;
            }
            else
            {
                rotate = 0;
            }

            offset = Quaternion.AngleAxis(rotate * rotateSpeed, Vector3.up) * offset;
        }

        
        
        if (!targeting)
        {
            Quaternion rotation = Quaternion.LookRotation(relative_pos);
            transform.rotation = rotation;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(closest_enemy.transform.position - transform.position);
            transform.rotation = rotation;
        }

        if (follow_path)
        {
            switch (side_view)
            {
                case Camera_Side.LEFT:
                    if (Input.GetAxis("Horizontal") > 0.0f /*|| Input.GetAxis("Vertical") == 1*/)
                    {
                        transform.position = Vector3.SmoothDamp(transform.position, side_cam_left.position, ref velocity, smooth_time);
                        Debug.Log(Input.GetAxisRaw("Horizontal"));
                    }

                    if (Input.GetAxisRaw("Horizontal") < -0.1f)
                    {
                        transform.position = Vector3.SmoothDamp(transform.position, side_cam_right.position, ref velocity, smooth_time);
                    }
                    break;

                case Camera_Side.RIGHT:
                    if (Input.GetAxis("Horizontal") > 0.01f /*|| Input.GetAxis("Vertical") == 1*/)
                    {
                        transform.position = Vector3.SmoothDamp(transform.position, side_cam_right.position, ref velocity, smooth_time);
                        Debug.Log(Input.GetAxisRaw("Horizontal"));

                    }

                    if (Input.GetAxisRaw("Horizontal") < -0.1f)
                    {
                        transform.position = Vector3.SmoothDamp(transform.position, side_cam_left.position, ref velocity, smooth_time);
                    }

                    break;
            }
            
        }
        else if (targeting)
        {
            target_position = closest_enemy.transform.position + ((target.position + new Vector3(0, 3, 0) - closest_enemy.transform.position) * 1.4f) ;
            //transform.position = Vector3.SmoothDamp(transform.position, target_position, ref velocity, smooth_time);
            transform.position = Vector3.SmoothDamp(transform.position, target_cam_pos.transform.position, ref velocity, smooth_time);

        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, target_position, ref velocity, smooth_time);
        } 
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 original_pos = transform.position;

        float elapsed = 0;
        while (elapsed < duration)
        {
            Vector3 new_pos = original_pos;
            new_pos += new Vector3(Random.Range(-1, 1) * magnitude, Random.Range(-1, 1) * magnitude, 0);

            transform.position = Vector3.SmoothDamp(transform.position, new_pos, ref velocity, 0);

            //transform.position = new_pos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = original_pos;
    }
}
