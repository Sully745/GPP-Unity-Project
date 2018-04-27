using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Flames : MonoBehaviour
{
    [SerializeField]
    private Transform[] Positions;
    [SerializeField]
    private ParticleSystem[] Flames;
    [SerializeField]
    private float DurationInSeconds = 10;
    [SerializeField]
    private bool FlamesTest = false;
    [SerializeField]
    private ParticleSystem FlamesPrefab;
    
    private Quaternion up;

    void Start ()
    {
        up = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        Flames = new ParticleSystem[Positions.Length];

        int i = 0;
        foreach (Transform position in Positions)
        {
            Flames[i] = Instantiate(FlamesPrefab, position.position, up);
            Flames[i].transform.SetParent(position);
            Flames[i].transform.localRotation = up;
            Flames[i].Pause();
            i++;
        }
    }

    private void Update()
    {
        if (FlamesTest)
        {
            StartCoroutine(_PlayFlames(DurationInSeconds));
            FlamesTest = false;
        }
    }

    public void FlamePlay(float duration)
    {
        StartCoroutine(_PlayFlames(duration));
    }

    private IEnumerator _PlayFlames(float duration)
    {
        foreach (ParticleSystem Flame in Flames)
        {
            if (!Flame.isPlaying)
            {
                Flame.Play(true);
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (ParticleSystem Flame in Flames)
        {
            if (Flame.isPlaying)
            {
                Flame.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
