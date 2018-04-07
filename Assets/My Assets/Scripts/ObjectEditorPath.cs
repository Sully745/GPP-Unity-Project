using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectEditorPath : MonoBehaviour {

    GameObject[] nodes;
    public GameObject node;
    public bool show_in_game;
    List<GameObject> draw_path;
    Movement_Style movement_style;
    public Material mat_organic;
    public Material mat_machine;

    // Use this for initialization
    void Start()
    {
        draw_path = new List<GameObject>();
        nodes = GetComponent<ObjectPathFollow>().nodes;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        movement_style = GetComponent<ObjectPathFollow>().movement_style;

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
        //foreach (GameObject clone in draw_path)
        //{
        //    DestroyImmediate(clone);
        //}
        //GameObject[] clones = GameObject.FindGameObjectsWithTag("Path Clone");
        //foreach (GameObject clone in clones)
        //{
        //    DestroyImmediate(clone);
        //}
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

                    GameObject clone = Instantiate(node, track_pos, Quaternion.identity);
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
        //foreach (GameObject clone in draw_path)
        //{
        //    DestroyImmediate(clone);
        //}
        //GameObject[] clones = GameObject.FindGameObjectsWithTag("Path Clone");
        //foreach (GameObject clone in clones)
        //{
        //    DestroyImmediate(clone);
        //}
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
                    GameObject clone = Instantiate(node, track_pos, Quaternion.identity);
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

