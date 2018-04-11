using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    public BezierSpline spline;
    public GameObject Player;
    public GameObject Trigger1;
    public GameObject Trigger2;

    bool controller;

    public float duration;

    public bool lookForward;



    public SplineWalkerMode mode;

    public float progress;
    private bool goingForward = true;
    private bool forward = false;
    private bool backward = false;

    public Vector3 position;

    private void Start()
    {
        controller = true;

    }
    private void Update()
    {
        if (progress < 0f)
        {
            progress = 0;
        }
        if (progress > 1f)
        {
            progress = 1;

        }

        //if (Player.GetComponent<PlayerController>().freeControl == false)
        //{
        //    if (lookForward)
        //    {

        //        float Move = Player.GetComponent<PlayerController>().MoveForward;

        //        progress += Move / 90;

        //         position = spline.GetPoint(progress);

        //        position = new Vector3(spline.GetPoint(progress).x, Player.transform.position.y, spline.GetPoint(progress).z);

        //        transform.localPosition = position;
        //        transform.LookAt(position + spline.GetDirection(progress));

            

        //    }
        //}

        //Debug.Log(progress);
    }
}













// if (goingForward && onSpline == true)
//        {
//            progress += Time.deltaTime / duration;
//            if (progress > 1f)
//            {
//                if (mode == SplineWalkerMode.Once)
//                {
//                    progress = 1f;
//                }
//                else if (mode == SplineWalkerMode.Loop)
//                {
//                    progress -= 1f;
//                }
//                else
//                {
//                    progress = 2f - progress;
//                    goingForward = false;
//                }
//            }
//        }
//        else
//        {
//            progress -= Time.deltaTime / duration;
//            if (progress< 0f)
//            {
//                progress = -progress;
//                goingForward = true;
//            }
//        }

//        Vector3 position = spline.GetPoint(progress);
//transform.localPosition = position;
//        if (lookForward)
//        {
//            transform.LookAt(position + spline.GetDirection(progress));
//        }
//    }