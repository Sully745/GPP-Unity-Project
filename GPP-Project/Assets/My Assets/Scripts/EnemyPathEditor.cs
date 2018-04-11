using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyPathEditor : MonoBehaviour
{
    public bool show_in_game;
    public GameObject path_clone;
    public Material mat_organic;
    public Material mat_machine;

    private GameObject[] nodes;
    private List<GameObject> draw_path;
    private Movement_Style movement_style;

    // Use this for initialization
    void Start()
    {
        draw_path = new List<GameObject>();
        nodes = GetComponent<EnemyPath>().nodes;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        movement_style = GetComponent<EnemyPath>().movement_style;

        switch (movement_style)
        {
            case Movement_Style.MACHINE:
                DrawPathEditorMachine();
                break;
            case Movement_Style.ORGANIC:
                DrawPathEditorOrganic();
                break;
        }
    }

    private void DrawPathEditorMachine()
    {
        draw_path.Clear();

        if (!Application.isPlaying || show_in_game)
        {
            Vector3 track_pos;
            int i = 0;
            while (i < nodes.Length - 1)
            {

                for (int j = 0; j < 10; j++)
                {
                    float track = .1f * j;
                    track_pos = Vector3.Lerp(nodes[i].transform.position, nodes[i + 1].transform.position, track);

                    GameObject clone = Instantiate(path_clone, track_pos, Quaternion.identity);
                    clone.tag = "Path Clone";
                    clone.GetComponent<MeshRenderer>().material = mat_machine;
                    clone.hideFlags = HideFlags.HideInHierarchy;
                    draw_path.Add(clone);
                }
                i++;
            }
        }
    }

    private void DrawPathEditorOrganic()
    {
        draw_path.Clear();

        if (!Application.isPlaying || show_in_game)
        {
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
                    GameObject clone = Instantiate(path_clone, track_pos, Quaternion.identity);
                    clone.tag = "Path Clone";
                    clone.GetComponent<MeshRenderer>().material = mat_organic;
                    clone.hideFlags = HideFlags.HideInHierarchy;
                    draw_path.Add(clone);
                }
                i += increment;
            }
        }
    }
}

