using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Smoke : MonoBehaviour
{
    [SerializeField]
    private Transform[] Positions;
    [SerializeField]
    private ParticleSystem[] Smoke;

    private float DurationInSeconds = 10;
    [SerializeField]
    private bool SmokeTest = false;
    [SerializeField]
    private ParticleSystem SmokePrefab;

    private Quaternion up;

    void Start()
    {
        up = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        Smoke = new ParticleSystem[Positions.Length];

        int i = 0;
        foreach (Transform position in Positions)
        {
            Smoke[i] = Instantiate(SmokePrefab, position.position, up);
            Smoke[i].transform.SetParent(position);
            Smoke[i].transform.localRotation = up;
            Smoke[i].Pause();
            i++;
        }
    }

    private void Update()
    {
        if (SmokeTest)
        {
            StartCoroutine(_PlaySmoke(DurationInSeconds));
            SmokeTest = false;
        }
    }

    public IEnumerator _PlaySmoke(float duration)
    {
        duration = 1.5f;

        foreach (ParticleSystem s in Smoke)
        {
            if (!s.isPlaying)
            {
                s.Play(true);
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (ParticleSystem s in Smoke)
        {
            if (s.isPlaying)
            {
                s.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}