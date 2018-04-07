using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement_Style
{
    MACHINE,
    ORGANIC
};

public enum Movement_Type
{
    LOOP,
    RESTART,
    PING_PONG
}

public class ObjectPathFollow : MonoBehaviour
{
    public GameObject[] nodes;
    public bool show_path = false;
    bool following;
    public List<Vector3> _path;
    public float speed = 5;
    public int target_node = 0;
    public int next_node = 0;
    public int prev_node = -1;
    public Vector3 offset;
    public List<GameObject> draw_path; // this
    public float distance_to_node;
    public Movement_Style movement_style;
    public Movement_Type movement_type;
    public bool move_forwards = true;
    public Transform platform;
    public float delay;
    public bool delay_bool = false;
    public bool rotate = false;
    public bool rotate_with_movement = false;
    public bool submerge;
    float rotation_x;
    float rotation_y;

    // Use this for initialization

    void Start()
    {
        offset = new Vector3(0, 0, 0);
        _path = new List<Vector3>();
        draw_path = new List<GameObject>();
        //DrawPath();
        switch (movement_style)
        {
            case Movement_Style.ORGANIC:
                DrawPathOrganic();
                break;
            case Movement_Style.MACHINE:
                DrawPathMachine();
                break;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (rotate_with_movement)
        {
            LockedRotation();
        }
        if(rotate)
        {
            rotation();
        }
        if (!show_path)
        {
            foreach (GameObject node in nodes)
            {
                node.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        
    }

    void rotation()
    {
        //float step = 100 * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.right, _path[target_node], 100, 0.0F);
        ////transform.rotation = Quaternion.LookRotation(newDir);
        //transform.LookAt(_path[target_node]);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.forward) * transform.rotation;
    }

    void LockedRotation()
    {
        float x = GetComponent<PlatformCoupling>().rotation.x;
        rotation_x += x;
        rotation_x = Mathf.Clamp(rotation_x, -5.0f, 5.0f);

        float y = GetComponent<PlatformCoupling>().rotation.y;
        rotation_y += y;
        rotation_y = Mathf.Clamp(rotation_y, -5.0f, 5.0f);

        transform.localEulerAngles = new Vector3(-rotation_x, -rotation_y, transform.localEulerAngles.z);
    }

    void FixedUpdate()
    {
        //following = GetComponent<PlayerController>().following_path;
        //SetNode();
        switch (movement_style)
        {
            case Movement_Style.MACHINE:
                SetNode();
                Move();
                break;
            case Movement_Style.ORGANIC:
                SetNode();
                Move();
                break;
        }
    }


    private void Move()
    {

        //float distance = Vector3.Distance(nodes[next_node].transform.position, transform.position);
        //distance = Mathf.Abs(distance);
        //target_node = next_node;
        //Debug.Log(distance);
        //if (distance < distance_to_node && next_node < nodes.Length - 1)
        //{
        //    next_node++;
        //    prev_node = next_node - 1;

        //}
        transform.position = Vector3.MoveTowards(transform.position, _path[target_node] + offset, Time.deltaTime * speed) ;
        //Vector3 direction = (_path[target_node] - transform.position).normalized;
        //platform.GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.deltaTime);
    }


    private void SetNode()
    {
        if (move_forwards)
        {
            float distance = Vector3.Distance(_path[next_node], transform.position);
            distance = Mathf.Abs(distance);
            target_node = next_node;
            if (distance <= distance_to_node && next_node < _path.Count - 1)
            {
                next_node++;
                prev_node = next_node - 1;
                distance = distance_to_node + 1;
            }
            if (distance <= distance_to_node && next_node == _path.Count - 1)
            {
                EndPath();
            }
        }
        else
        {
            float distance = Vector3.Distance(_path[prev_node], transform.position);
            distance = Mathf.Abs(distance);
            target_node = prev_node;

            if (distance <= distance_to_node && prev_node > 0)
            {
                prev_node--;
                next_node = prev_node + 1;
                distance = distance_to_node + 1;
            }

            if (distance <= distance_to_node && prev_node == 0)
            {
                EndPath();

            }
        }
    }


    IEnumerator DelayMove(bool state)
    {
        if (delay_bool)
        {
            yield return new WaitForSeconds(delay);

        }
        move_forwards = state;

    }


    private void EndPath()
    {
        switch (movement_type)
        {
            case Movement_Type.LOOP:
                next_node = 0;
                break;
            case Movement_Type.RESTART:
                transform.position = nodes[0].transform.position;
                next_node = 1;
                break;
            case Movement_Type.PING_PONG:
                StartCoroutine(DelayMove(!move_forwards));
                break;
        }
    }

    private void DrawPathOrganic()
    {
        _path.Clear();
        Vector3 track_pos;
        int increment = 0;
        int i = 0;
        while (i < nodes.Length - 1)
        {

            for (int j = 0; j <= 100; j++)
            {
                float track = .01f * j;

                if (i + 3 <= nodes.Length - 1)
                {
                    track_pos = Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track),
                        Vector3.Lerp(nodes[i + 1].transform.position, nodes[i + 2].transform.position, track), track),
                        Vector3.Lerp(Vector3.Lerp(nodes[i + 1].transform.position, nodes[i + 2].transform.position, track),
                        Vector3.Lerp(nodes[i + 2].transform.position, nodes[i + 3].transform.position, track), track), track);
                    increment = 3;

                }
                else if (i + 2 <= nodes.Length - 1)
                {
                    track_pos = Vector3.Lerp(Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track),
                        Vector3.Lerp(nodes[i + 1].transform.position, nodes[i + 2].transform.position, track), track);
                    increment = 2;
                }
                else
                {
                    track_pos = Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track);
                    increment = 1;
                }
                _path.Add(track_pos);
            }
            i += increment;
        }
    }

    private void DrawPathMachine()
    {
        _path.Clear();
        Vector3 track_pos;
        int i = 0;
        while (i < nodes.Length - 1)
        {

            for (int j = 0; j <= 100; j++)
            {
                float track = .01f * j;
                track_pos = Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track);
                _path.Add(track_pos);
            }
            i++;
        }
    }
}