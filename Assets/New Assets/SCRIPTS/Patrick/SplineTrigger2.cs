using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger2 : MonoBehaviour
{

    public BezierSpline spline;
    public GameObject Player;
    public Camera Camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        //if (collider.tag == "Player" && Player.GetComponent<NewCharacterController>().freeControl == false && Player.GetComponent<SplineWalker>().progress == 0)
        //{
        //    Player.GetComponent<NewCharacterController>().freeControl = true;
        //}

        if (collider.tag == "Player" && Player.GetComponent<SplineWalker>().spline == null)
        {
            Player.GetComponent<SplineWalker>().spline = spline;
            Player.GetComponent<SplineWalker>().progress = 1;

            Player.GetComponent<PlayerController>().freeControl = false;
             Camera.GetComponent<PlayerController>().camSpline = true;

                }
        else if (collider.tag == "Player" && Player.GetComponent<SplineWalker>().spline != null)
        {
            Player.GetComponent<SplineWalker>().spline = null;

            Player.GetComponent<PlayerController>().freeControl = true;

            Camera.GetComponent<PlayerController>().camSpline = false;
            //Assign the view to the back of the player
            //Camera.GetComponent<NewCameraController>().hasRot;
            Camera.GetComponent<PlayerController>().currentAngle = 2;
            Camera.GetComponent<PlayerController>().currentView = Camera.GetComponent<PlayerController>().views[Camera.GetComponent<PlayerController>().currentAngle];
        }
    }
}
