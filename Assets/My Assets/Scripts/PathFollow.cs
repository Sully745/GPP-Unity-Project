using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{

    public GameObject[] nodes;
    public bool show_path;
    bool following;
    public List<Vector3> _path;
    public int target_node = 0;
    public int next_node = 0;
    public int prev_node = -1;
    public List<GameObject> draw_path; // this
    public float distance_to_node;


    // Use this for initialization

    void Start()
    {
        _path = new List<Vector3>();
        draw_path = new List<GameObject>();

        DrawPath();


        //InvokeRepeating("DrawPath", 2.0f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        following = GetComponent<PlayerController>().following_path;
        SetNode();
        if (!show_path)
        {
            foreach (GameObject node in nodes)
            {
                node.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void SetNode()
    {
        if (following)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                float distance = Mathf.Abs(((_path[next_node].x - transform.position.x)
                    + (_path[next_node].z - transform.position.z)));

                target_node = next_node;
                if (distance < distance_to_node && next_node < _path.Count - 1)
                {
                    next_node++;
                    prev_node = next_node - 1;

                }
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && next_node >= 1)
            {
                float distance = Mathf.Abs((_path[prev_node].x - transform.position.x)
                    + (_path[prev_node].z - transform.position.z));

                target_node = prev_node;
                if (distance < distance_to_node && prev_node > 0)
                {
                    prev_node--;
                    next_node = prev_node + 1;
                }
            }
        }
    }

    private void DrawPath()
    {        
        _path.Clear();
        Vector3 track_pos;
        int increment = 0;
        int i = 0;
        while (i < nodes.Length - 1)
        {

            for (int j = 0; j < 10; j++)
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
}