using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Flames : MonoBehaviour
{
    public Transform LeftFoot;
    public Transform RightFoot;
    public ParticleSystem Flames;

    private Quaternion up;
    private ParticleSystem left;
    private ParticleSystem right;

    // Use this for initialization
    void Start ()
    {
        up = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        StartCoroutine(_CreateFeetFlames(10.0f));
    }

    private void Update()
    {
        left.transform.localRotation = up;
        right.transform.localRotation = up;
    }

    public IEnumerator _CreateFeetFlames(float duration)
    {
        if (!left)
        {
            Debug.Log("Spawning Left Fire");
            left = Instantiate(Flames, LeftFoot.position, up);
            left.transform.SetParent(LeftFoot);
        }
        if (!right)
        {
            Debug.Log("Spawning Right Fire");
            right = Instantiate(Flames, RightFoot.position, up);
            right.transform.SetParent(RightFoot);
        }

        yield return new WaitForSeconds(duration);
        
        if (left)
        {
            Debug.Log("Killing Left Fire");
            Destroy(left.gameObject);
        }
        if (right)
        {
            Debug.Log("Killing Right Fire");
            Destroy(right.gameObject);
        }
    }
}
