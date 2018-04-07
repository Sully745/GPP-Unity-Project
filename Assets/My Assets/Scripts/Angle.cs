

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Angle : MonoBehaviour
{

    #region PARAMETER
    //[Range(1f, 15f)]
    //public float ShowLineDistance = 2f;
    #endregion

    #region DISPLAY
    public float DistanceToFace;
    public float SlopeAngle;
    
    public Vector3 DirectionSlope;
    public Vector3 HitFacePosition;
    public Vector3 HitFaceNormal;
    #endregion

    #region InternalVariable

    #endregion


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            GetGroundSliding();
        }
    }

    void FixedUpdate()
    {
        if (!Application.isEditor)
        {
            GetGroundSliding();
        }
        GetComponent<PlayerController>().DirectionSlope = DirectionSlope;
        GetComponent<PlayerController>().SlopeAngle = SlopeAngle;
        GetComponent<PlayerController>().dist_to_ground = DistanceToFace;

    }

    void GetGroundSliding()
    {
        // Get Physical from Up to Down
        Vector3 Origin = transform.position + Vector3.up;
        Vector3 Dir = Vector3.down;
        RaycastHit HitRay = new RaycastHit();
        
        SlopeAngle = 0;

        if (Physics.Raycast(Origin, Dir, out HitRay, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("SlopeDetector"))))
        {
            DistanceToFace = HitRay.distance;
            Debug.DrawLine(Origin, Origin + Dir * HitRay.distance, Color.green);
            //Debug.DrawLine(HitRay.point, HitRay.point + HitRay.normal * ShowLineDistance, Color.red);


            // Save Hit position For Display in OnSceneGUI
            HitFacePosition = HitRay.point;
            // Get the Normal of face
            HitFaceNormal = HitRay.normal;

            Dir = -HitRay.normal;


            // Compute Angle
            SlopeAngle = ((Vector3.Angle(Vector3.up, DirectionSlope.normalized)) - 90f);

            // Compute Slope first get Start point of the vector
            DirectionSlope = Vector3.Cross(HitFaceNormal, Vector3.Cross(HitFaceNormal, Vector3.up));
            DirectionSlope.Normalize();

            // If Angle is geater or equal to 180
            if (SlopeAngle >= 90f)
                SlopeAngle = 0;
        }
        // No Physcal Contact
        else
        {
            DistanceToFace = Mathf.Infinity;
            DirectionSlope = Vector3.zero;

        }


    }

}