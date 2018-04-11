using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath: MonoBehaviour
{
    [Header("Movement Settings")]
    public Movement_Style movement_style;
    public Movement_Type movement_type;
    public float delay = 0;
    
    private bool move_forwards = true;
    private int next_node = 0;
    private int prev_node = -1;
    private int target_node = 0;
    private float distance_to_node = 1.5f;

    [Header("Path")]
    public bool show_nodes = false;
    public GameObject[] nodes;
    public List<Vector3> _path;
    [HideInInspector]
    public Vector3 direction;
    

    // Use this for initialization
    void Start()
    {
        _path = new List<Vector3>();
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

    void FixedUpdate()
    {
        foreach (GameObject node in nodes)
        {
            node.GetComponent<MeshRenderer>().enabled = show_nodes;
        }

        SetNode();
        SetDirection();
    }

    private void SetDirection()
    {
        direction = (_path[target_node] - transform.position).normalized;
        direction.y = 0;
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
                StartCoroutine(EndPath());
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
                StartCoroutine(EndPath());
            }
        }
    }


    IEnumerator DelayMove()
    {
        yield return new WaitForSeconds(delay);
    }

    IEnumerator EndPath()
    {
        yield return new WaitForSeconds(delay);

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
                move_forwards = !move_forwards;
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

            for (int j = 0; j <= 10; j++)
            {
                float track = .1f * j;

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
            for (int j = 0; j <= 10; j++)
            {
                float track = .1f * j;
                track_pos = Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track);
                _path.Add(track_pos);
            }
            i++;
        }
    }
}